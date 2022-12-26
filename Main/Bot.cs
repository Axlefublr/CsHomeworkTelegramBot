using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Main;

class Bot {
   private ITelegramBotClient _telegramClient;

   public Bot(ITelegramBotClient telegramBotClient) {
      _telegramClient = telegramBotClient;
   }
   
   async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
      if (update.Type == UpdateType.CallbackQuery) {
         await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "You pressed a button", cancellationToken: cancellationToken);
         return;
      }
      
      if (update.Type == UpdateType.Message) {
         await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "You sent a message", cancellationToken: cancellationToken);
         return;
      }
   }
}