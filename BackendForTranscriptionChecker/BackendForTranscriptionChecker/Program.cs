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
                //FileReader fileReader = new FileReader();

                //List<string> referenceText = fileReader.Read("Reference.txt");
                //List<string> evaluatedText = fileReader.Read("Evaluated.txt");

                //EvaluatorEngine evaluatorEngine = new EvaluatorEngine(new RegExPatternCreator());
                //evaluatorEngine.EvaluateText(referenceText, evaluatedText);


                RegExPatternCreator _regExPatternCreator = new RegExPatternCreator();
                //PatternGroupBuilder _patternGroupBuilder = new PatternGroupBuilder();
                CrossChecker _crossChecker = new CrossChecker();

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

                string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
                string[] evalArray = { "A", "B", "B", "B", "B", "I", "I", "I", "I", "F" };

                string[] correctWords = _crossChecker.GetCorrectWords(refArray, evalArray);

                Console.WriteLine(String.Join(" ", correctWords));


                Console.WriteLine("End");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}


