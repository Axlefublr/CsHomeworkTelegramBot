using Main.Configuration;
using Main.Utilities;
using Telegram.Bot;

namespace Main.Services;

public class AudioFileHandler : IFileHandler {

   private readonly AppSettings _appSettings;
   private readonly ITelegramBotClient _telegramBotClient;

   public AudioFileHandler(AppSettings appSettings, ITelegramBotClient telegramBotClient) {
      _appSettings       = appSettings;
      _telegramBotClient = telegramBotClient;
   }

   public async Task Download(string fileId, CancellationToken cancellationToken) {
      string inputAudioFilePath = Path.Combine(_appSettings.DownloadFolder, $"{_appSettings.AudioFileName}.{_appSettings.InputAudioFormat}");
      
      using (FileStream destinationStream = File.Create(inputAudioFilePath)) {
         var file = await _telegramBotClient.GetFileAsync(fileId, cancellationToken);
         if (file.FilePath == null)
            return;

         await _telegramBotClient.DownloadFileAsync(file.FilePath, destinationStream, cancellationToken);
      }
   }

   public string Process(string languageCode) {
      string inputAudioPath = Path.Combine(_appSettings.DownloadFolder, _appSettings.AudioFileName + "." + _appSettings.InputAudioFormat);
      string outputAudioPath = Path.Combine(_appSettings.DownloadFolder, _appSettings.AudioFileName + "." + _appSettings.OutputAudioFormat);

      AudioConverter.TryConvert(inputAudioPath, outputAudioPath);

      return "Conversion successful";
   }
}