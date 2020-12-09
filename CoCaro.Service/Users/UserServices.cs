
using System.Linq;
using System;

using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

using System.IO;
using CoCaro.Models;
using CoCaro.Data.Models;

namespace CoCaro.Services.Users
{
   
    public class UserServices : IUserService
    {

      
        public ErrorObject Login(string Username, string Password)
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var user = db.Users.FirstOrDefault(x => x.Username.ToLower().Equals(Username.ToLower()) && x.Password.Equals(Password));
                if (user != null)
                {
                    return error.SetData(user);
                }
                else
                {
                    return Error.USER_INVALID;
                }
      
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ErrorObject Login(string Username, string Password)
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var user = db.Users.FirstOrDefault(x => x.Username.ToLower().Equals(Username.ToLower()) && x.Password.Equals(Password));
                if (user != null)
                {
                    return error.SetData(user);
                }
                else
                {
                    return Error.USER_INVALID;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ErrorObject CreateUser(User user)
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var users = db.Users.FirstOrDefault(x => x.Username.ToLower().Equals(user.Username.ToLower()));
                if (users != null)
                {
                    return error.Failed("Username đã tồn tại");
                }
                else
                {

                    db.Users.Add(user);
                    db.SaveChanges();
                    return error.SetData(user);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}