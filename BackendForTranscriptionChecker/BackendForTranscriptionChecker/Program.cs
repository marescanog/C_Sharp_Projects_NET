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
                FileReader fileReader = new FileReader();

                List<string> referenceText = fileReader.Read("Reference.txt");
                List<string> evaluatedText = fileReader.Read("Evaluated.txt");

                EvaluatorEngine evaluatorEngine = new EvaluatorEngine(new RegExPatternCreator());

                evaluatorEngine.EvaluateText(referenceText, evaluatedText);

             

                Console.WriteLine("Check");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}
