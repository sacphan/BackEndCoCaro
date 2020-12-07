

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
                //serviceCollection.AddSingleton<ICacheService, MemoryCacheService>();
                //serviceCollection.AddSingleton<ICacheKeyService, CacheKeyService>();
                //serviceCollection.AddSingleton<Microsoft.Extensions.Caching.Memory.IMemoryCache, Microsoft.Extensions.Caching.Memory.MemoryCache>();
               

                switch (Assembly.GetEntryAssembly()?.GetName().Name)
                {
                  
                    case "BoardManager_BackEnd":
                     
                        //serviceCollection.AddSingleton<ITokenService, TokenService>();
                        //serviceCollection.AddSingleton<IWorkContext, ApiWorkContext>();
                        //serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                                         
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
