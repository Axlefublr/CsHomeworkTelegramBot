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
         await _telegramClient.SendTextMessageAsync(update.CallbackQuery.From.Id, "Sorry, I'm not programmed to operate on this, please send a normal message instead", cancellationToken: cancellationToken);
         return;
      }

      if (update.Type == UpdateType.Message) {
         if (update.Message.Text == "bro that's crazy") {
            await _telegramClient.SendTextMessageAsync(update.Message.From.Id, "fr on god no cap", cancellationToken: cancellationToken);
            return;
         }
         await _telegramClient.SendTextMessageAsync(update.Message.From.Id, $"Your message consists of {update.Message.Text.Length} symbols", cancellationToken: cancellationToken);
         return;
      }
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