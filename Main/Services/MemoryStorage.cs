
using System.Collections.Concurrent;
using Main.Models;

namespace Main.Services;

public class MemoryStorage : IStorage
{

   private readonly ConcurrentDictionary<long, Session> _sessions;
   
   public MemoryStorage() {
      _sessions = new ConcurrentDictionary<long, Session>();
   }

   public Session GetSession(long chatId)
   {
      if (_sessions.ContainsKey(chatId)) {
         return _sessions[chatId];
      }

      Session newSession = new Session() { LanguageCode = "en" };
      _sessions.TryAdd(chatId, newSession);
      return newSession;
   }
}