using Microsoft.Extensions.Configuration;
using System.IO;

namespace Common.Gen.Utils
{

    public class ConnectionStringResult
    {

        public ConnectionStringResult(string cns)
        {
            this.ConnectionString = cns;

        }

        public string ConnectionString { get; private set; }


    }

    public class AppSettings
    {
        private readonly IConfigurationRoot _configuration;
        public AppSettings()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this._configuration = builder.Build();

        }

        public string this[string nome]
        {
            get
            {
                return this._configuration.GetSection($"AppSettings:{nome}").Value;
            }
        }
    }

   
    public class ConnectionStrings
    {
        private readonly IConfigurationRoot _configuration;
        public ConnectionStrings()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this._configuration = builder.Build();

        }

        public ConnectionStringResult this[string nome]
        {
            get
            {
                return new ConnectionStringResult(this._configuration.GetSection($"ConnectionStrings:{nome}").Value);
            }
        }
    }


    public static class ConfigurationManager
    {
        static ConfigurationManager()
        {
            AppSettings = new AppSettings();
            ConnectionStrings = new ConnectionStrings();
        }

        public static AppSettings AppSettings { get; set; }

        public static ConnectionStrings ConnectionStrings { get; set; }
    }
}
