using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class Helper
    {
        private static readonly Regex OnlyLettersRegex = new Regex("^[a-zA-Z ]+$");
        private static readonly Regex OnlyDigitsRegex = new Regex("^[0-9]+$");
        private static readonly Regex AlphaNumericWithSpaces = new Regex("^[a-zA-Z0-9 ]+$");

        public static int GetValidatedInteger(string message)
        {
            while (true)
            {
                Console.Write(message);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && OnlyDigitsRegex.IsMatch(input))
                    return int.Parse(input);
                Console.WriteLine("Xəta: Yalnız rəqəmlər daxil edin.");
            }
        }

        public static string GetValidatedStringLettersOnly(string message)
        {
            while (true)
            {
                Console.Write(message);
                var input = Console.ReadLine() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(input) && OnlyLettersRegex.IsMatch(input))
                    return input;
                Console.WriteLine(" Xəta: Yalnız ingilis hərfləri və boşluq icazəlidir.");
            }
        }

        public static string GetValidatedStringAlphaNumeric(string message)
        {
            while (true)
            {
                Console.Write(message);
                var input = Console.ReadLine() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(input) && AlphaNumericWithSpaces.IsMatch(input))
                    return input;
                Console.WriteLine(" Xəta: Yalnız ingilis hərfləri, rəqəmlər və boşluq icazəlidir.");
            }
        }
    }
}
