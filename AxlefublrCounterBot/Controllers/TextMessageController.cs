using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AxlefublrCounterBot.Controllers;

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
                  InlineKeyboardButton.WithCallbackData("Count characters", "characterCount"),
                  InlineKeyboardButton.WithCallbackData("Sum numbers", "numSum")
               }
            };

            string htmlMessage = $"<b> This bot counts your message characters or sums up your numbers for you.</b>{Environment.NewLine}{Environment.NewLine}Pick what you want to bot to do by pressing one of the buttons!{Environment.NewLine}";

            await _telegramClient.SendTextMessageAsync(message.Chat.Id, htmlMessage, cancellationToken: cancellationToken, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
            break;
         default:
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Record a voice message to be converted into text", cancellationToken: cancellationToken);
            break;
      }
   }
}