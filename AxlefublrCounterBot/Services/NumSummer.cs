namespace AxlefublrCounterBot.Services;

public class NumSummer : INumSummer
{
   private int[] FormatMessage(string message)
   {
      string[] wrongTypeNumbers = message.Split(' ');
      int[] correctTypeNumbers = new int[wrongTypeNumbers.Length];
      for (int i = 0; i < wrongTypeNumbers.Length; i++)
      {
         try
         {
            correctTypeNumbers[i] = int.Parse(wrongTypeNumbers[i]);
         }
         catch (FormatException)
         {
            throw;
         }
      }
      return correctTypeNumbers;
   }

   public int GetNumSum(string message)
   {
      int sum = 0;

      try
      {
         int[] numbers = FormatMessage(message);
         foreach (int value in numbers)
         {
            sum += value;
         }
      }
      catch (FormatException)
      {
         throw;
      }

      return sum;
   }
}