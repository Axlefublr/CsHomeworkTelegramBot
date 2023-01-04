namespace AxlefublrCounterBot.Services;

public interface INumSummer {
   int[] FormatMessage(string message);
   int GetNumSum(params int[] numbers);
}