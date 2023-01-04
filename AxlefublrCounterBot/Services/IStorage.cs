using AxlefublrCounterBot.Models;

namespace AxlefublrCounterBot.Services;

public interface IStorage
{
   Session GetSession(long chatId);
}