using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordUnscrambler
{
    class Program
    {
        static void Main(string[] args)
        {
            var continueWordUnscramble = "Y";

            do
            {
                Console.WriteLine("Please enter the option F for File and M for Manual");

                string option = Console.ReadLine() ?? string.Empty;

                switch (option.ToUpper())
                {
                    case "F":
                        Console.Write("Enter Scrambled Words File Name: ");
                        ExecuteScrambledWordsInFileScenario();
                        break;
                    case "M":
                        ExecuteScrambledWordsManualEntryScenario();
                        Console.Write("Enter Scrambled Words Manually: ");

                        break;
                    default:
                        Console.WriteLine("Option was not recognized.");
                        break;
                }

                do
                {
                    Console.Write("Would you like to continue? Y/N ");
                    continueWordUnscramble = (Console.ReadLine() ?? string.Empty);

                } while (
                !continueWordUnscramble.Equals("Y", StringComparison.OrdinalIgnoreCase) &&
                !continueWordUnscramble.Equals("N", StringComparison.OrdinalIgnoreCase));

            } while (continueWordUnscramble.Equals("Y", StringComparison.OrdinalIgnoreCase));
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            throw new NotImplementedException();
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            throw new NotImplementedException();
        }
    }
}
