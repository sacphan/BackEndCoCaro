namespace CoCaro.Service.Caching
{
    /// <summary>
    /// Represents default values related to caching
    /// </summary>
    public static partial class CachingDefaults
    {
        /// <summary>
        /// Gets the default cache time in minutes
        /// </summary>
        public static int CacheTime => int.MaxValue;
        
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Entity type name
        /// {1} : Entity id
        /// </remarks>
        public static string EntityCacheKey => "{0}.id-{1}";
    }
}