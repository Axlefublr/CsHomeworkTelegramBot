using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Main.Controllers;

public class TextMessageController
{
   private readonly ITelegramBotClient _telegramClient;

   public TextMessageController(ITelegramBotClient telegramClient)
   {
      _telegramClient = telegramClient;
   }

   public async Task Handle(Message message, CancellationToken cancellationToken) {
      switch (message.Text) {
         case "/start":
            var buttons = new List<InlineKeyboardButton[]>
            {
               new[] {
                  InlineKeyboardButton.WithCallbackData("Русский", "ru"),
                  InlineKeyboardButton.WithCallbackData("English", "en")
               }
            };

            string htmlMessage = $"<b> This bot transforms audio into text.</b>{Environment.NewLine}{Environment.NewLine}You can record a message if you don't want to type it out, then send it to whoever you want :){Environment.NewLine}";

            await _telegramClient.SendTextMessageAsync(message.Chat.Id, htmlMessage, cancellationToken: cancellationToken, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
            break;
         default:
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Record a voice message to be converted into text", cancellationToken: cancellationToken);
            break;
      }
   }
}