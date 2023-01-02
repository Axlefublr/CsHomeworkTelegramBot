using Main.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Main.Controllers;

public class InlineKeyboardController
{
   private readonly ITelegramBotClient _telegramClient;
   private readonly IStorage _memoryStorage;

   public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
   {
      _telegramClient = telegramClient;
      _memoryStorage = memoryStorage;
   }

   public async Task Handle(CallbackQuery? callbackQuery, CancellationToken cancellationToken) {
      
      if (callbackQuery?.Data == null) {
         return;
      }

      _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

      string languageText = callbackQuery.Data switch
      {
         "ru" => "Русский",
         "en" => "English",
         _ => string.Empty
      };

      await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, 
         $"<b>Audio language is {languageText}.{Environment.NewLine}</b>"
         + $"{Environment.NewLine}You can change it in the main menu.",
         cancellationToken: cancellationToken,
         parseMode: ParseMode.Html
      );
   }

}
