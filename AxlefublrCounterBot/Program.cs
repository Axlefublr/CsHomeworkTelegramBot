using System.Text;
using AxlefublrCounterBot.Configuration;
using AxlefublrCounterBot.Services;
using AxlefublrCounterBot.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace AxlefublrCounterBot;

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

      AppSettings appSettings = new("5904191681:AAGDbxApDBbAPSGvOfuqV9lmPc1F5s8_3ok");

      services.AddTransient<DefaultMessageController>();
      services.AddTransient<VoiceMessageController>();
      services.AddTransient<TextMessageController>();
      services.AddTransient<InlineKeyboardController>();

      services.AddSingleton<IStorage, MemoryStorage>();

      services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
      services.AddHostedService<Bot>();
   }
}