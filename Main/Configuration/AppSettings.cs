namespace Main.Configuration;

public class AppSettings {
   public string BotToken { get; set; }
   public string DownloadFolder { get; set; }
   public string AudioFileName { get; set; }
   public string InputAudioFormat { get; set; }
   public string OutputAudioFormat { get; set; }
   public float InputAudioBitrate { get; set; }
}