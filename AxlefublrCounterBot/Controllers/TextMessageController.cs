using AxlefublrCounterBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AxlefublrCounterBot.Controllers;

public class TextMessageController
{
   private readonly ITelegramBotClient _telegramClient;
   private readonly IStorage _memoryStorage;

   public TextMessageController(ITelegramBotClient telegramClient, IStorage memoryStorage)
   {
      _telegramClient = telegramClient;
      _memoryStorage = memoryStorage;
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
            DoMessageAction(message, cancellationToken);
            break;
      }
   }

   private void DoMessageAction(Message message, CancellationToken cancellationToken)
   {
      switch (_memoryStorage.GetSession(message.Chat.Id).Action) {
         case "characterCount":
            _telegramClient.SendTextMessageAsync(message.Chat.Id, "The number of characters in your message is: " + message.Text.Length);
            break;
         case "numSum":
            string displayText;
            try
            {
               displayText = new NumSummer().GetNumSum(message.Text).ToString();
            } catch (FormatException) {
               displayText = "not counted because not all symbols in your message are numbers!";
            }
            _telegramClient.SendTextMessageAsync(message.Chat.Id, "The sum of all numbers in your message is " + displayText, cancellationToken: cancellationToken);
            break;
         default:
            _telegramClient.SendTextMessageAsync(message.Chat.Id, "Select your action first!", cancellationToken: cancellationToken);
            break;
      }
   }
}