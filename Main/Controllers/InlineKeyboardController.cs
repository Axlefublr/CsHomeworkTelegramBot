using Telegram.Bot;
using Telegram.Bot.Types;

namespace Main.Controllers;

public class InlineKeyboardController
{
   private readonly ITelegramBotClient _telegramClient;

   public InlineKeyboardController(ITelegramBotClient telegramClient)
   {
      _telegramClient = telegramClient;
   }
}
