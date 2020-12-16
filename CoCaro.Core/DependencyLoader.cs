

using CoCaro.Service.Board;
using CoCaro.Service.Caching;
using CoCaro.Service.Chat;
using CoCaro.Service.Playing;
using CoCaro.Service.Token;
using CoCaro.Services.Users;
using Microsoft.Extensions.DependencyInjection;

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


                switch (Assembly.GetEntryAssembly()?.GetName().Name)
                {
                  
                    case "AdminAPI":
                     
                        serviceCollection.AddSingleton<ITokenService, TokenService>();

                        serviceCollection.AddSingleton<IUserService, UserServices>();
                        //serviceCollection.AddSingleton<IWorkContext, ApiWorkContext>();
                        break;
                    case "UserAPI":

                        serviceCollection.AddSingleton<ITokenService, TokenService>();
                        serviceCollection.AddSingleton<IUserService, UserServices>();
                        serviceCollection.AddSingleton<IBoardService, BoardService>();
                        serviceCollection.AddSingleton<IChatService, ChatService>();
                        serviceCollection.AddSingleton<IPlayingService, PlayingService>();
                        //serviceCollection.AddSingleton<IWorkContext, ApiWorkContext>();
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
