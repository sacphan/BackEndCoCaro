//using System;
//using System.Collections.Generic;
//using System.Linq;
//using BoardManager_Data.BoardManagerContext.Models;
//using BoardManager_Model;
//using BoardManager_Service.Users;
//using BoardManager_Utilities;
//using Microsoft.AspNetCore.Http;


//namespace BoardManager_Service.WorkContext
//{
//    /// <summary>
//    /// Work context for web application
//    /// </summary>
//    public partial class WebWorkContext : IWorkContext
//    {
//        private const string UserSessionName = "Session.User";
//        private const string UserCookieName = "Cookie.User";
//        private const string LanguageCookieName = "Language";
//        private const string CustomerSessionName = "Session.Customer";
//        private const string CustomerCookieName = "Cookie.Customer";
//        private readonly IHttpContextAccessor _HttpContextAccessor;
//        private IUserService _UserService;
      
//        public WebWorkContext(IHttpContextAccessor httpContextAccessor, IUserService userService)
//        {
//            _HttpContextAccessor = httpContextAccessor;
//            _UserService = userService;
           
//        }
//        public UsersAccount CurrentUser
//        {
//            get => _HttpContextAccessor.HttpContext.Session.GetObject<UsersAccount>(UserSessionName);
//            set
//            {
//                if (value != null)
//                {
//                    _HttpContextAccessor.HttpContext.Session.SetObject(UserSessionName, value);
//                    //SetUserCookie();
//                    //SetLanguageCookie(value.Id);
//                }
//                else
//                {
//                    _HttpContextAccessor.HttpContext.Session.Remove(UserSessionName);
//                    RemoveCookie(UserCookieName);
//                }
//            }
//        }

       

//        public UsersAccount UserCookie
//        {
//            get => GetUserCookie();
//            set
//            {
//                SetUserCookie();
//            }
//        }

//        public UsersAccount GetUserCookie()
//        {
//            var cookieValue = GetCookie(UserCookieName);
//            if (string.IsNullOrEmpty(cookieValue))
//            {
//                return null;
//            }
//            /*if (cookie.Expires < DateTime.UtcNow) //khóa lại vì browser ko gửi expire lên server
//            {
//                RemoveCookie(UserCookieName);
//                return null;
//            }*/
//            var tmp = cookieValue.Split('|');
//            var result = _UserService.Login(tmp.First(), tmp.Last());
//            if (result.Code == Error.SUCCESS.Code)
//            {
//                CurrentUser = result.GetData<UsersAccount>();
//            }
//            return CurrentUser;

//        }

      

//        public void SetUserCookie()
//        {
//            SetCookie(UserCookieName, CurrentUser.UserName + "|" + CurrentUser.PassWord);
//        }

      

//        public void SetCookie(string key, string value)
//        {
//            var option = new CookieOptions
//            {
//                Expires = string.IsNullOrEmpty(value) ? DateTime.Now.AddMonths(-1) : DateTime.Now.AddHours(24 * 365)
//            };
//            _HttpContextAccessor.HttpContext.Response.Cookies.Delete(key);
//            _HttpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
//        }

//        public string GetCookie(string key)
//        {
//            return _HttpContextAccessor.HttpContext.Request.Cookies[key];
//        }

//        public void RemoveCookie(string Name)
//        {
//            if (_HttpContextAccessor.HttpContext == null) return;
//            SetCookie(Name, string.Empty);
//        }

     
//    }
//}