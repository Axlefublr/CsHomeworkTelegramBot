using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace CharacterCounterBot;

public class Program
{
   private static async Task Main()
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
      services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("BOT_TOKEN"));
      services.AddHostedService<Bot>();
   }
}