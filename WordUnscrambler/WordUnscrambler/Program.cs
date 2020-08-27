﻿using System;
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

        static void Main(string[] args)
        {
            var continueWordUnscramble = Constants.Yes;

            do
            {
                Console.WriteLine(Constants.OptionsOnHowToEnterScrabledWords);

                string option = Console.ReadLine() ?? string.Empty;

                switch (option.ToUpper())
                {
                    case Constants.File:
                        Console.Write(Constants.EnterScrambledWordsViaFile);
                        ExecuteScrambledWordsInFileScenario();
                        break;
                    case Constants.Manual:
                        ExecuteScrambledWordsManualEntryScenario();
                        Console.Write(Constants.EnterScrambledWordsManually);

                        break;
                    default:
                        Console.WriteLine(Constants.EnterScrambledWordsOptionNotRecognized);
                        break;
                }

                do
                {
                    Console.Write(Constants.OptionsOnContinuingTheProgram);
                    continueWordUnscramble = (Console.ReadLine() ?? string.Empty);

                } while (
                !continueWordUnscramble.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) &&
                !continueWordUnscramble.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));

            } while (continueWordUnscramble.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase));
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            try
            {
                var filename = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = _fileReader.Read(filename);
                DisplayMatchedUnscrambledWords(scrambledWords);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorScrambledWordsCannotBeLoaded + ex.Message);
            }
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(Constants.WordlListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if(matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine(Constants.MatchFound, matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine(Constants.MatchNotFound);
            }
        }
    }
}
