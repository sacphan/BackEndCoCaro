
using CoCaro.Models;


using System;
using System.Collections.Generic;
using System.Text;

namespace CoCaro.Service.Token
{
    public interface ITokenService
    {
        //AuthToken CreateToken(UserProfile user);
        AuthToken RefreshToken(string RefreshToken);
    }
}
