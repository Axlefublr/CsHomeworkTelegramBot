using AxlefublrCounterBot.Configuration;
using AxlefublrCounterBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AxlefublrCounterBot.Controllers;

public class VoiceMessageController
{
   private readonly AppSettings _appSettings;
   private readonly ITelegramBotClient _telegramClient;
   private readonly IStorage _memoryStorage;

   public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramClient, IStorage memoryStorage)
   {
      _appSettings = appSettings;
      _telegramClient = telegramClient;
      _memoryStorage = memoryStorage;
   }

   public async Task Handle(Message message, CancellationToken cancellationToken)
   {
      await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Only text messages are supported", cancellationToken: cancellationToken);
   }
}
