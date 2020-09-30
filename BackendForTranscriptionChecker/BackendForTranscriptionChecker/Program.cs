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
                PatternGroupBuilder _patternGroupBuilder = new PatternGroupBuilder();

                //string[] refArray = { "A", "A", "A", "B", "A", "A", "A", "A", "B" };
                //string[] evalArray = { "B", "A", "F", "F", "A", "B" };

                string[] refArray = { "A", "B", "C", "D", "E" };
                string[] evalArray = { "A", "B", "C", "D", "E" };

                //string expectedOutCome = "(.*) A A B (.*) (.*) (.*) (.*) (.*)";
                string expectedOutComeCrossCheck = "\nA\nA B\n";

                string[] arrayOFCorrectWords = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);
                //string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);
                
                Console.WriteLine("ExpctPattern: {0}", expectedOutComeCrossCheck);
                Console.WriteLine("ActualPattern: ");

                foreach(var word in arrayOFCorrectWords)
                {
                    Console.WriteLine(word);
                }

                Console.WriteLine("Check");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}


