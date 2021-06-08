using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Mp3TagReader.Models;
using Mp3TagReader.Tasks;

namespace Mp3TagReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = InitConfig();

            //new SaveToJson(config);
            new SaveToDb(config);
        }

        private static Settings InitConfig()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
            ; 
 
            IConfigurationRoot configuration = builder.Build();
            var configSettings = new Settings();
            
            configuration.GetSection("Settings").Bind(configSettings);
            configSettings.DefaultConnectionString = configuration.GetConnectionString("Mp3TagReaderConnection");

            return configSettings;
        }
    }
}