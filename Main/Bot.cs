using Main.Controllers;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Main;

internal class Bot : BackgroundService {
   private readonly ITelegramBotClient _telegramClient;
   private readonly InlineKeyboardController _inlineKeyboardController;
   private readonly TextMessageController _textMessageController;
   private readonly VoiceMessageController _voiceMessageController;
   private readonly DefaultMessageController _defaultMessageController;

   public Bot(
      ITelegramBotClient telegramClient,
      InlineKeyboardController inlineKeyboardController,
      TextMessageController textMessageController,
      VoiceMessageController voiceMessageController,
      DefaultMessageController defaultMessageController
   ) {
      _telegramClient = telegramClient;
      _inlineKeyboardController = inlineKeyboardController;
      _textMessageController = textMessageController;
      _voiceMessageController = voiceMessageController;
      _defaultMessageController = defaultMessageController;
   }

   protected override Task ExecuteAsync(CancellationToken stoppingToken)
   {
      _telegramClient.StartReceiving(
         HandleUpdateAsync,
         HandleErrorAsync,
         new ReceiverOptions() { AllowedUpdates = { } },
         cancellationToken: stoppingToken
      );

      Console.WriteLine("Бот запущен");
      return Task.CompletedTask;
   }

   async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
      if (update.Type == UpdateType.CallbackQuery) {
         await _inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
         return;
      }

      if (update.Type == UpdateType.Message) {
         switch (update.Message!.Type) {
            case MessageType.Voice:
               await _voiceMessageController.Handle(update.Message, cancellationToken);
               return;
            case MessageType.Text:
               await _textMessageController.Handle(update.Message, cancellationToken);
               return;
            default:
               await _defaultMessageController.Handle(update.Message, cancellationToken);
               return;
         }
      }
   }

   private static string GenerateResponse(string text)
   {
      return text;
   }

   Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
   {
      var errorMessage = exception switch
      {
         ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
         _ => exception.ToString()
      };

      Console.WriteLine(errorMessage);

      Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
      Thread.Sleep(10000);

      return Task.CompletedTask;
   }
}