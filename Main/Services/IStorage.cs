using Main.Models;

namespace Main.Services;

public interface IStorage {
   Session GetSession(long chatId);
}