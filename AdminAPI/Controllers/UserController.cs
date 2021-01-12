using CoCaro.Data.Models;
using CoCaro.Models;
using CoCaro.Service.Token;
using CoCaro.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPI.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _IUserService;
        private ITokenService _TokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _IUserService = userService;
            _TokenService = tokenService;
        }
        [Route("api/login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {

            //IActionResult response = Unauthorized();
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
               

                var result = _IUserService.Login(user.Username, user.Password);
                if (result.Code == Error.SUCCESS.Code)
                {
                    var token = _TokenService.CreateToken(result.GetData<User>());
                    return Ok(error.SetData(token));
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return Ok(error.System(ex));
            }
        }
        [Route("api/isLogin")]
        [Authorize]
        [HttpGet]
        public IActionResult CheckLogin()
        {
            return Ok(true);
        }
       
        [Route("api/refreshtoken")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult RefreshToken(JObject RefreshToken)
        {
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                JToken jToken;
                if (!RefreshToken.TryGetValue("refreshToken", out jToken))
                {
                    return Ok(error.Failed("refreshToken invalid."));
                }
                var refreshToken = jToken.Value<string>();
                var token = _TokenService.RefreshToken(refreshToken);
                if (token != null)
                {
                    return Ok(error.SetData(token));
                }
                else
                {
                    return Ok(error.Failed("refreshToken invalid"));
                }
            }
            catch (Exception ex)
            {
                error.Failed(ex.Message);   
            }
            return Ok(error);
        }
        [Route("api/GetListUser")]
        [HttpGet]
        [Authorize]
        public IActionResult GetListUser()
        {
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                error=  _IUserService.GetListUser();
                return Ok(error);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/BlockUser")]
        [HttpPost]
        [Authorize]
        public IActionResult BlockUser([FromBody]int id)
        {
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                error = _IUserService.BlockUser(id);
                return Ok(error);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
