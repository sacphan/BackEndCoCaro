//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;

//using Microsoft.AspNetCore.Http;


//namespace CoCaro.Service.WorkContext
//{
//    public class ApiWorkContext : IWorkContext
//    {
//        private IHttpContextAccessor _HttpContextAccessor;
//        private IUserService _UserService;
      
//        //public ApiWorkContext(IHttpContextAccessor httpContextAccessor,IUserService userService,ICacheManager cacheManager) 
//        public ApiWorkContext(IHttpContextAccessor httpContextAccessor, IUserService userService)
//        {
//            _HttpContextAccessor = httpContextAccessor;
//            _UserService = userService;
          
//        }
//        public UserProfile CurrentUser
//        {
//            get
//            {
//                var user = _HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type.Equals("User")).Value;
//                return user.JsonToObject<UserProfile>();
//            }
//            set
//            {
               
//            }
//        }

//        public UsersAccount UserCookie
//        {
//            get
//            {
//                var user = _HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("User")).Value;
//                return user.JsonToObject<UsersAccount>();
//            }
//            set
//            {

//            }
//        }

       
//        public UsersAccount GetUserCookie()
//        {
//            throw new System.NotImplementedException();
//        }

      
//        public void SetUserCookie()
//        {
//            throw new NotImplementedException();
//        }

      

      
//        }

      

       
    
//}