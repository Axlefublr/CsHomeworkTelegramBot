namespace AxlefublrCounterBot.Services;

public class NumSummer : INumSummer
{
   public int[] FormatMessage(string message)
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

   public int GetNumSum(int[] numbers)
   {
      throw new NotImplementedException();
   }
}