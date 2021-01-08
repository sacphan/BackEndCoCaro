using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CoCaro.Data.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CoCaro.Service.WorkContext
{
    public class ApiWorkContext : IWorkContext
    {
        private IHttpContextAccessor _HttpContextAccessor;
       

        //public ApiWorkContext(IHttpContextAccessor httpContextAccessor,IUserService userService,ICacheManager cacheManager) 
        public ApiWorkContext(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
           

        }
        public User CurrentUser
        {
            get
            {
                var user = _HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("User")).Value;
                return JsonConvert.DeserializeObject<User>(user);
            }
            set
            {

            }
        }

        public User UserCookie
        {
            get
            {
                var user = _HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("User")).Value;
                return JsonConvert.DeserializeObject<User>(user);
            }
            set
            {

            }
        }


        public User GetUserCookie()
        {
            throw new System.NotImplementedException();
        }


        public void SetUserCookie()
        {
            throw new NotImplementedException();
        }




    }





}