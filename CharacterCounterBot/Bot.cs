using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CharacterCounterBot;

internal class Bot : BackgroundService {

   private readonly ITelegramBotClient _telegramClient;

   public Bot(ITelegramBotClient telegramBotClient) {
      _telegramClient = telegramBotClient;
   }

   protected override Task ExecuteAsync(CancellationToken stoppingToken) {
      _telegramClient.StartReceiving(
         HandleUpdateAsync,
         HandleErrorAsync,
         new ReceiverOptions()
         {
            AllowedUpdates = { }
         },
         cancellationToken: stoppingToken
      );

      Console.WriteLine("Service started");
      return Task.CompletedTask;
   }

   async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
   {
      if (update.Type == UpdateType.CallbackQuery) {
         await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "You pressed a button", cancellationToken: cancellationToken);
         return;
      }

      if (update.Type == UpdateType.Message) {
         string message = GenerateResponse(update.Message.Text);
         await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, message, cancellationToken: cancellationToken);
         return;
      }
   }

   private string GenerateResponse(string text)
   {
      throw new NotImplementedException();
   }

   Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
   {
      var errorMessage = exception switch
      {
         ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
         _ => exception.ToString()
      };

      Console.WriteLine(errorMessage);

      Console.WriteLine("Waiting 10 seconds before retrying");
      Thread.Sleep(10000);

      return Task.CompletedTask;
   }
}