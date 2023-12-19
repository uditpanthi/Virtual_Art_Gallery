using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    /*public static class DBConnection
    {
        public static string GetConnectionString()
        {
            // Read connection string from property file or configuration
            string connectionString = "Server=DESKTOP-JFQUU93;Database=VirtualGallery;Trusted_connection=true";

            return connectionString;
        }
    }*/
}
