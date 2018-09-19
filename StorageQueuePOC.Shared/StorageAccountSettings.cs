using Microsoft.Extensions.Configuration;
using System.IO;

namespace StorageQueuePOC.Shared
{
    public class StorageAccountSettings
    {
        public static string ConnectionString => GetConnectionString();

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            return configuration.GetConnectionString("StorageAccountConnection");
        }
    }
}
