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

   public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramClient, IFileHandler audioFileHandler)
   {
      _appSettings = appSettings;
      _telegramClient = telegramClient;
      _audioFileHandler = audioFileHandler;
   }

   public async Task Handle(Message message, CancellationToken cancellationToken)
   {
      var fileId = message?.Voice.FileId;
      if (fileId == null) {
         return;
      }

      await _audioFileHandler.Download(fileId, cancellationToken);

      await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Voice message downloaded", cancellationToken: cancellationToken);
   }
}
