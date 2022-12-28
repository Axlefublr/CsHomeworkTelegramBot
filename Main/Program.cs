using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace Main;

public class Program
{
   public static async Task Main()
   {
      Console.OutputEncoding = Encoding.Unicode;

      var host = new HostBuilder()
         .ConfigureServices((hostContext, services) => ConfigureServices(services))
         .UseConsoleLifetime()
         .Build();

      Console.WriteLine("Service started");

      await host.RunAsync();
      Console.WriteLine("Service stopped");
   }

   static void ConfigureServices(IServiceCollection services) {
      services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5890705941:AAEwEqmToyagFgrngTiN7v7DL8l8LyYah0M"));
      services.AddHostedService<Bot>();
   }
}