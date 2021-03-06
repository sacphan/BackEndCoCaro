

using CoCaro.Service.Board;
using CoCaro.Service.Caching;
using CoCaro.Service.Chat;
using CoCaro.Service.Game;
using CoCaro.Service.Token;
using CoCaro.Service.WorkContext;
using CoCaro.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

using System.Reflection;
using System.Text;


namespace CoCaro.Core
{
    public static class DependencyLoader
    {

        public static void LoadDependencies(this IServiceCollection serviceCollection)
        {
            try
            {
                serviceCollection.AddSingleton<ICacheService, MemoryCacheService>();
                serviceCollection.AddSingleton<ICacheKeyService, CacheKeyService>();
                serviceCollection.AddSingleton<Microsoft.Extensions.Caching.Memory.IMemoryCache, Microsoft.Extensions.Caching.Memory.MemoryCache>();
                serviceCollection.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

                switch (Assembly.GetEntryAssembly()?.GetName().Name)
                {
                  
                    case "AdminAPI":
                     
                        serviceCollection.AddSingleton<ITokenService, TokenService>();

                        serviceCollection.AddSingleton<IUserService, UserServices>();
                        serviceCollection.AddSingleton<IWorkContext, ApiWorkContext>();
                        serviceCollection.AddSingleton<IGameService, GameServices>();

                        break;
                    case "UserAPI":

                        serviceCollection.AddSingleton<ITokenService, TokenService>();
                        serviceCollection.AddSingleton<IUserService, UserServices>();
                        serviceCollection.AddSingleton<IBoardService, BoardService>();
                        serviceCollection.AddSingleton<IChatService, ChatService>();
                        serviceCollection.AddSingleton<IWorkContext, ApiWorkContext>();
                        serviceCollection.AddSingleton<IGameService, GameServices>();
                        
                        break;
                }
            }
            catch (ReflectionTypeLoadException typeLoadException)
            {
                var builder = new StringBuilder();
                foreach (Exception loaderException in typeLoadException.LoaderExceptions)
                {
                    builder.AppendFormat("{0}\n", loaderException.Message);
                }

                throw new TypeLoadException(builder.ToString(), typeLoadException);
            }
        }
    }
}
