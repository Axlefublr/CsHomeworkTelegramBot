using Telegram.Bot;
using Telegram.Bot.Types;

namespace AxlefublrCounterBot.Controllers;

public class DefaultMessageController
{
   private readonly ITelegramBotClient _telegramClient;

   public DefaultMessageController(ITelegramBotClient telegramClient)
   {
      _telegramClient = telegramClient;
   }

   public async Task Handle(Message message, CancellationToken cancellationToken)
   {
      Console.WriteLine($"Controller {GetType().Name} got message");
      await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Got unsupported message");
   }
}
