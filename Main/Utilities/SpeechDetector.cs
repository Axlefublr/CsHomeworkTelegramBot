using System.Text;
using Main.Extensions;
using Newtonsoft.Json.Linq;
using Vosk;

namespace Main.Utilities;

public static class SpeechDetector {

   public static string DetectSpeech(string audioPath, float inputBitrate, string languageCode) {
      Vosk.Vosk.SetLogLevel(0);
      var modelPath = Path.Combine(DirectoryExtension.GetSolutionRoot(), "Speech-models", "vosk-model-small-" + languageCode.ToLower());
      Model model = new(modelPath);
      return GetWords(model, audioPath, inputBitrate);
   }

   private static string GetWords(Model model, string audioPath, float inputBitrate)
   {
      VoskRecognizer recording = new(model, inputBitrate);
      recording.SetMaxAlternatives(0);
      recording.SetWords(true);

      StringBuilder textBuffer = new();

      using (Stream source = File.OpenRead(audioPath)) {
         byte[] buffer = new byte[4096];
         int bytesRead;

         while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0) {
            if (recording.AcceptWaveform(buffer, bytesRead)) {
               var sentenceJson = recording.Result();
               JObject sentenceObj = JObject.Parse(sentenceJson);
               string sentence = (string)sentenceObj["text"];
               textBuffer.Append(StringExtension.UppercaseFirst(sentence) + ". ");
            }
         }
      }

      var finalSentence = recording.FinalResult();
      JObject finalSentenceObj = JObject.Parse(finalSentence);
      textBuffer.Append((string)finalSentenceObj["text"]);
      return textBuffer.ToString();
   }
}