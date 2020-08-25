using System;
using System.Collections.Generic;
using System.Linq;
using WordUnscrambler.Data;
using WordUnscrambler.Workers;

namespace WordUnscrambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        private const string wordlListFileName = "wordList.txt";

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
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            var filename = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = _fileReader.Read(filename);
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(wordlListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if(matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine("Match Found for {0}: {1}", matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine("No matches have been found.");
            }
        }
    }
}
