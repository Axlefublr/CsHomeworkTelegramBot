using Main.Configuration;
using Main.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Main.Controllers;

public class VoiceMessageController
{
   private readonly AppSettings _appSettings;
   private readonly ITelegramBotClient _telegramClient;
   private readonly IFileHandler _audioFileHandler;
   private readonly IStorage _memoryStorage;

   public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramClient, IFileHandler audioFileHandler, IStorage memoryStorage)
   {
      _appSettings = appSettings;
      _telegramClient = telegramClient;
      _audioFileHandler = audioFileHandler;
      _memoryStorage = memoryStorage;
   }

   public async Task Handle(Message message, CancellationToken cancellationToken)
   {
      var fileId = message.Voice?.FileId;
      if (fileId == null) {
         return;
      }

      await _audioFileHandler.Download(fileId, cancellationToken);

      string userLanguageCode = _memoryStorage.GetSession(message.Chat.Id).LanguageCode;

      var result = _audioFileHandler.Process(userLanguageCode);

      await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: cancellationToken);
   }
}
