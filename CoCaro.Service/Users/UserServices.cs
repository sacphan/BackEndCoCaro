using System.Linq;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

using System.IO;
using CoCaro.Models;
using CoCaro.Data.Models;
using System.Collections.Generic;

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
                var user = db.Users.FirstOrDefault(x => x.Username.ToLower().Equals(Username.ToLower()) && x.Password.Equals(Password) && x.IsBlock != true);
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
        public ErrorObject LoginFacebook(User user)
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var user1 = db.Users.FirstOrDefault(x => x.FacebookId == user.FacebookId);
                if (user1 == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return error.SetData(user);
                }
                else 
                {
                    return error.SetData(user1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ErrorObject LoginGoogle(User user)
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var user1 = db.Users.FirstOrDefault(x => x.GoogleId == user.GoogleId);
                if (user1 == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return error.SetData(user);
                }
                else
                {
                    return error.SetData(user1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ErrorObject LoginAdmin(string Username, string Password)
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var user = db.Users.FirstOrDefault(x =>x.RoleId == 1 && x.Username.ToLower().Equals(Username.ToLower()) && x.Password.Equals(Password));
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
                var u = db.Users.FirstOrDefault(x => x.Username.ToLower() == user.Username.ToLower() || x.Email == user.Email);
                if (u != null)
                {
                    return error.Failed("Tài khoản đã tồn tại");
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
      
        public UserInfo GetInfoByUserName(string username)
        {
            UserInfo userinfo = null;
            try
            {
                using var db = new CoCaroContext();
                var user = db.Users.FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()));
                userinfo = new UserInfo()
                {
                    Username = user.Username,
                    Cup = user.Cup,
                    RateWin = user.RateWin,
                    TotalGame = user.TotalGame,
                    CreateDate = user.CreateDated == null ? string.Empty : user.CreateDated.Value.ToString("dd/MM/yyyy")

                };
                userinfo.Rank = db.Users.Where(u=>u.Cup!=null).OrderBy(u => u.Cup).ToList().IndexOf(user) + 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return userinfo;
        }
        public ErrorObject GetListUser()
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var users = db.Users.ToList();
                return error.SetData(users);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return error;
        }
        public ErrorObject BlockUser(int userId)
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var users = db.Users.FirstOrDefault(x => x.Id == userId);
                if (users.IsBlock != null)
                {
                    users.IsBlock = !users.IsBlock;
                }
                else
                {
                    users.IsBlock = true;
                }
                db.SaveChanges();
                return error.SetData(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ErrorObject SeachUserByEmailOrName(string Keyword)
        {
            var error = Error.Success();
            try
            {
                using var db = new CoCaroContext();
                var users = db.Users.Where(x => x.Email.Contains(Keyword) || x.FullName.Contains(Keyword) || x.Username.Contains(Keyword)).ToList();
                return error.SetData(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return error;
        }

    }
}