namespace CoCaro.Service.Caching
{
    public static class CacheKeyName
    {



        #region Api

        /// <summary>
        /// Key for Refresh Token
        /// </summary>
        /// /// <remarks>
        /// {0} : Refresh Token
        /// </remarks>
        public static CacheKey RefreshToken => new CacheKey("Api.RefreshToken-{0}");
        public static CacheKey User => new CacheKey("Api.User-{0}-{1}","Api","User");

        #endregion


    }
}