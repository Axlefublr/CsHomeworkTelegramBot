using AxlefublrCounterBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AxlefublrCounterBot.Controllers;

public class InlineKeyboardController
{
   private readonly ITelegramBotClient _telegramClient;
   private readonly IStorage _memoryStorage;

   public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
   {
      _telegramClient = telegramClient;
      _memoryStorage = memoryStorage;
   }

   public async Task Handle(CallbackQuery? callbackQuery, CancellationToken cancellationToken)
   {

      if (callbackQuery?.Data == null)
      {
         return;
      }

      _memoryStorage.GetSession(callbackQuery.From.Id).Action = callbackQuery.Data;

      string currentAction = callbackQuery.Data switch
      {
         "characterCount" => "counting message characters",
         "numSum" => "summing up message numbers",
         _ => string.Empty
      };

      await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
         $"<b>Current bot action is {currentAction}.{Environment.NewLine}</b>"
         + $"{Environment.NewLine}You can change it in the main menu.",
         cancellationToken: cancellationToken,
         parseMode: ParseMode.Html
      );
   }

}
