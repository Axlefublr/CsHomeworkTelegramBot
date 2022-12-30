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

   public async Task Handle(CallbackQuery? callbackQuery, CancellationToken cancellationToken) {
      Console.WriteLine($"Controller {GetType().Name} got message");
      await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"Got a button pressed");
   }

}
