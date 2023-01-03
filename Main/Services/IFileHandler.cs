namespace Main.Services;

public interface IFileHandler {
   Task Download(string fileId, CancellationToken cancellationToken);

   string Process(string param);
}