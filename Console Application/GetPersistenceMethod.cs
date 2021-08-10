using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ConsoleApplication.Persistence
{
    public static class DataLayer 
    {
        public static Type GetClass(){
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            IConfigurationSection configurationSection = configuration.GetSection("PersistenceMethod").GetSection("Method");
            return Type.GetType(configurationSection.Value);
        }
    }
}