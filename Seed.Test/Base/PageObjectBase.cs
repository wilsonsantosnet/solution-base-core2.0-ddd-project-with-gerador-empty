using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace Seed.Test.Base
{
    public abstract class PageObjectBase
    {

        protected string GetSection(string section)
        {
            var config = LoadFile();
            return config.GetSection(section).Value;
        }

        protected IOptions<T> GetSection<T>(string section) where T : class, new()
        {
            var config = LoadFile();
            var newInstance = Activator.CreateInstance(typeof(T)) as T; 
            config.GetSection(section).Bind(newInstance);
            var result = Options.Create(newInstance);
            return result;
        }

        protected  Mock<ILoggerFactory> MakeLogFactory<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();
            mockLogger.Setup(
                m => m.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()));

            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(() => mockLogger.Object);
            return mockLoggerFactory;
        }

        private static IConfigurationRoot LoadFile()
        {
            return new ConfigurationBuilder()
                          .SetBasePath(AppContext.BaseDirectory)
                          .AddJsonFile("appsettings.json", false, true)
                          .Build();
        }
    }
}
