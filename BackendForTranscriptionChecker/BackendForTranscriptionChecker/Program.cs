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
        private static readonly FileReader _fileReader = new FileReader();
        static void Main(string[] args)
        {
            try
            {
                List<string> referenceText = _fileReader.Read("Reference.txt");
                List<string> evaluatedText = _fileReader.Read("Evaluated.txt");

                Console.WriteLine("Check");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}
