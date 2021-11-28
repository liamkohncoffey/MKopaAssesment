using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmsService.Extensions;

namespace SmsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddMessaging();
                    services.AddSmsProviders();
                    services.AddLogging();
                    services.AddHostedService<SmsSender>();
                });
    }
}
