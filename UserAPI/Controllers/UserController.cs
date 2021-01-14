using CoCaro.Data.Models;
using CoCaro.Models;
using CoCaro.Service.Token;
using CoCaro.Services.Users;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _IUserService;
        private ITokenService _TokenService;
        IHostingEnvironment env = null;

        public UserController(IUserService userService, ITokenService tokenService, IHostingEnvironment env)
        {
            _IUserService = userService;
            _TokenService = tokenService;
            this.env = env;
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
                    sendMail(user.Email, error.GetData<User>().Id);

                    //var token = _TokenService.CreateToken(error.GetData<User>());
                    return Ok(error.SetData("Truy cập mail để kích hoạt tài khoản"));
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
                var user = new User { FacebookId = login.FacebookId, Username = login.Email ?? login.Phone, FullName = login.Name, Email = login.Email };
                error = _IUserService.LoginFacebook(user);
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
                var user = new User { GoogleId = login.GoogleId, Username = login.Email ,Email=login.Email, FullName = login.Name };
                error = _IUserService.LoginGoogle(user);
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
       
        
        [Route("api/isLogin")]
        [Authorize]
        [HttpGet]
        public IActionResult CheckLogin() 
        {
            return Ok(_User);
        }
        [Route("api/GetInfoByUserName")]
        [Authorize]
        [HttpPost]
        public IActionResult GetInfoByUserName([FromBody] string username)
        {
            return Ok(_IUserService.GetInfoByUserName(username));
        }
        public IActionResult sendMail(string email, int userId)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("tranphihung312@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(email);
            message.To.Add(to);

            message.Subject = "Confirm accounts in Co Caro";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "http://localhost:3000/confirmAccount/"+userId.ToString();
            bodyBuilder.TextBody = "http://localhost:3000/confirmAccount/" + userId.ToString();
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("tranphihung312@gmail.com", "Hungpro312@@");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
            return Ok();
        }
        [Route("api/ConfirmAccount")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ConfirmAccount([FromBody] int id)
        {
            return Ok(_IUserService.UnlockUser(id));
        }
    }

}

