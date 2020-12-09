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

namespace UserAPI.Controllers
{
    public class UserController : Controller
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
        [Route("api/register")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {

            //IActionResult response = Unauthorized();
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                error = _IUserService.CreateUser(user);
                if (error.Code == Error.SUCCESS.Code)
                {

                    var token = _TokenService.CreateToken(error.GetData<User>());
                    return Ok(error.SetData(token));
                }
                return Ok(error);
            }
            catch (Exception ex)
            {
                return Ok(error.System(ex));
            }
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
        [Route("api/LoginFacebook")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginFacebook([FromBody] LoginFacebook login)
        {

            //IActionResult response = Unauthorized();
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                //var user = new User { FacebookId= login.FacebookId, Username = l}
                //error = _IUserService.Login(user);
                if (error.Code == Error.SUCCESS.Code)
                {

                    var token = _TokenService.CreateToken(error.GetData<User>());
                    return Ok(error.SetData(token));
                }
                return Ok(error);
            }
            catch (Exception ex)
            {
                return Ok(error.System(ex));
            }
        }
        [Route("api/LoginGoogle")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginGoogle([FromBody] LoginGoogle login)
        {

            //IActionResult response = Unauthorized();
            var error = new ErrorObject(Error.SUCCESS);
            try
            {
                //error = _IUserService.CreateUser(user);
                //if (error.Code == Error.SUCCESS.Code)
                {

                    var token = _TokenService.CreateToken(error.GetData<User>());
                    return Ok(error.SetData(token));
                }
                return Ok(error);
            }
            catch (Exception ex)
            {
                return Ok(error.System(ex));
            }
        }
        [Route("api/GetUserOnline")]
        [Authorize]
        [HttpGet]
        public IActionResult GetUserOnline()
        {
            return Ok(_IUserService.GetListUserOnline());
        }
    }
}

