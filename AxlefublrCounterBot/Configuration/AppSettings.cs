namespace AxlefublrCounterBot.Configuration;

public class AppSettings {
   public string BotToken { get; set; }
   public AppSettings(string botToken) => BotToken = botToken;
}