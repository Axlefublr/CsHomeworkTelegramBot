namespace Main.Extensions;

public static class StringExtension {
   public static string UppercaseFirst(string str) {
      if (string.IsNullOrEmpty(str)) {
         return string.Empty;
      }

      return char.ToUpper(str[0]) + str[1..];
   }
}