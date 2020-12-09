
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Http;
using CoCaro.Models;
using CoCaro.Data.Models;

namespace  CoCaro.Services.Users
{
    public interface IUserService
    {

        ErrorObject CreateUser(User user);
        ErrorObject Login(string Username, string Password);
    
    }
}