using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System.IO;

namespace NcrTestSub.App
{
    class Program
    {
        public static IConfiguration configuration;

        public static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            IServiceCollection services = ConfigureServices();


            ServiceProvider serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run();

            
        }
        /// <summary>
        /// Initial configuration, set appsettings to IConfiguration object
        /// </summary>
        private static void ConfigureApp()
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();

        }

        /// <summary>
        /// Services configuration add Iconfiguration object to singleton repository
        /// </summary>
        /// <returns>IServiceCollection instance</returns>
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureApp();

            services.AddSingleton(configuration);


            services.AddTransient<App>();

            return services;
            
        }

        
    }
}
