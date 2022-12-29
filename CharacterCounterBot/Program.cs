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
      services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5909859455:AAEijjGD2jcxQPJ8H5nUdMJqeHlvWzOYx0M"));
      services.AddHostedService<Bot>();
   }
}