﻿using BackendForTranscriptionChecker.Objects;
using BackendForTranscriptionChecker.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*
                FileReader fileReader = new FileReader();

                List<string> referenceText = fileReader.Read("Reference.txt");
                List<string> evaluatedText = fileReader.Read("Evaluated.txt");

                EvaluatorEngine evaluatorEngine = new EvaluatorEngine(new RegExPatternCreator());
                evaluatorEngine.EvaluateText(referenceText, evaluatedText);
                */


                //string[] refArray = { "A", "A", "A", "B", "A", "A", "A", "A", "B" };
                //string[] evalArray = { "B", "A", "F", "F", "A", "B" };

                //string[] refArray = { "A", "B", "C", "D", "E" };
                //string[] evalArray = { "A", "B", "C", "D", "E" };

                //string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "H" };
                //string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "G", "H" };

                ////string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
                ////string[] evalArray = { "A", "B", "O", "G", "H", "I", "J", "K", "L", "F" };

                //string expectedOutCome = "(.*) A A B (.*) (.*) (.*) (.*) (.*)";
                //string expectedOutComeCrossCheck = "\nA\nA B\n";

                //string[] arrayOFCorrectWords = _crossChecker.GetCorrectWords(refArray, evalArray);
                //string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

                //Console.WriteLine("ExpctPattern: {0}", expectedOutComeCrossCheck);
                //Console.WriteLine("ActualPattern: ");

                //string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
                //string[] evalArray = { "A", "B", "B", "B", "B", "I", "I", "I", "I", "F" };

                /* Test this string
                string[] refArray = { "G", "H", "I", "J", "K", "L", "O"};
                string[] evalArray = { "E", "O", "G", "H", "I", "J", "K", "L", "F" };
                */

                //string[] refArray = { "G", "H", "I", "J", "K", "L", "O", "P", "Q" };
                //string[] evalArray = { "E", "O", "G", "H", "I", "J", "K", "L", "F" };
                //string[] refArray = { "G", "H", "I", "J", "K", "L", "O", "P", "Q", "E", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
                // string[] evalArray = { "E", "O", "G", "H", "I", "J", "K", "L", "F", "G", "Q", "P", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };

                //string[] refArray = {"The","quick","brown","fox","jumped","over","the","lazy","dog", "The", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog" };
                //string[] evalArray = { "The", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "fox", "jumped", "over", "the", "lazy", "dog" };

                SubsequenceProcessor _subsequenceProcessor = new SubsequenceProcessor();

                string[] refArray = { "A", "A", "A", "A", "A" };
                string[] evalArray = { "A", "A", "A", "A", "A" };

                Console.WriteLine("The original text");
                Console.WriteLine("{0} \n", String.Join(" ", refArray));
                Console.WriteLine("The modified text");
                Console.WriteLine("{0} \n", String.Join(" ", evalArray));


                Console.WriteLine("\nThe pattern of correct words as a single string");
                _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

                Console.WriteLine("\n\nThe pattern of correct words:");

                
                Console.WriteLine("End");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}


