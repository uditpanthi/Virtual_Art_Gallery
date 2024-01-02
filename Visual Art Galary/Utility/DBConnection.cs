using Microsoft.Extensions.Configuration;

namespace Visual_Art_Galary.Utility
{

    internal static class DBConnection
    {
        static IConfiguration _iconfiguration;

        static DBConnection()
        {
            GetAppSetting();
        }
        private static void GetAppSetting()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSetting.json");
            _iconfiguration = builder.Build();
        }
        public static string GetConnectionString()
        {
            return _iconfiguration.GetConnectionString("localConnectionString");
        }

    }
}
