using System.Text;
using Main.Configuration;
using Main.Controllers;
using Main.Services;
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

   static AppSettings BuildAppSettings() {
      return new AppSettings()
      {
         DownloadFolder = @"C:\Users\serge\Downloads",
         BotToken = "5890705941:AAEwEqmToyagFgrngTiN7v7DL8l8LyYah0M",
         AudioFileName = "audio",
         InputAudioFormat = "ogg",
         OutputAudioFormat = "wav"
      };
   }

   static void ConfigureServices(IServiceCollection services) {

      AppSettings appSettings = BuildAppSettings();

      services.AddTransient<DefaultMessageController>();
      services.AddTransient<VoiceMessageController>();
      services.AddTransient<TextMessageController>();
      services.AddTransient<InlineKeyboardController>();

      services.AddSingleton<IStorage, MemoryStorage>();

      services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
      services.AddHostedService<Bot>();
   }
}