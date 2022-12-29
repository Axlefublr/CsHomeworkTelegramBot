using Telegram.Bot;
using Telegram.Bot.Types;

namespace Main.Controllers;

public class TextMessageController
{
   private readonly ITelegramBotClient _telegramClient;

   public TextMessageController(ITelegramBotClient telegramClient)
   {
      _telegramClient = telegramClient;
   }
   
   public async Task Handle(Message message, CancellationToken cancellationToken) {
      Console.WriteLine($"Controller {GetType().Name} got message");
      await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Got text message");
   }
}