

using CoCaro.Models;
using CoCaro.Service.Caching;
using Microsoft.Extensions.Configuration;
using System;


using System.Linq;
using System.Security.Claims;
using System.Text;
using CoCaro.Data.Models;
using Microsoft.IdentityModel.Tokens;
using CoCaro.Utilities;
using System.IdentityModel.Tokens.Jwt;

namespace CoCaro.Service.Token
{
    public class TokenService :ITokenService
    {
        private IConfiguration _Configuration;

        private ICacheService _CacheService;
        public TokenService(IConfiguration configuration, ICacheService cacheService)
        {
            _Configuration = configuration;
            _CacheService = cacheService;
        }
        public AuthToken CreateToken(User user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
             
                var claims = new[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("User", user.ToJson())
            

                };
                var token = new JwtSecurityToken(_Configuration["Jwt:Issuer"],
                    _Configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(int.Parse(_Configuration["Jwt:Expire"])),
                    signingCredentials: credentials);
                var bearerToken = new JwtSecurityTokenHandler().WriteToken(token);
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
                var exp = start.AddSeconds(int.Parse(token.Claims.FirstOrDefault(c => c.Type == "exp").Value)).AddHours(7);// VN: utc + 7
                var tokenResult = new AuthToken
                {
                    Token = bearerToken,
                    RefreshToken = Guid.NewGuid().ToString(),
                    Exprire = exp
                };
                var cacheKey = _CacheService.CreateCacheKey(CacheKeyName.RefreshToken, tokenResult.RefreshToken);
                _CacheService.Set(cacheKey, user);
                return tokenResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AuthToken RefreshToken(string RefreshToken)
        {
            try
            {
                //so sánh RefeshToken với cache
                var cacheKey = _CacheService.CreateCacheKey(CacheKeyName.RefreshToken, RefreshToken);
                //var user = _CacheService.Get<UsersAccount>(cacheKey);
                //if (user != null)
                //{
                //    _CacheService.Remove(cacheKey);
                //    return CreateToken(user.UserProfile);
                //}
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
