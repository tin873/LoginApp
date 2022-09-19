namespace Helper
{
    public static class AppConstants
    {
        public const string AppName = "Demo";

        /// <summary>
        /// Tạo ra một key cache theo chuẩn chung của app
        /// </summary>
        /// <param name="keyCache">Key cache cần lưu</param>
        /// <returns>Key cache theo chuẩn</returns>
        public static string CreateKeyCache(string keyCache) => $"{AppName}:{keyCache}";

        /// <summary>
        /// Thời gian sống mặc định của cache (đơn vị: seconds)
        /// </summary>

        public const int SlidingExpirationDefaultCacheDefault = 60*60*1;
    }
}
