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
            string original = "The quick brown fox jumps over the lazy dog.";
            string transcript = "The brown fox jumps over the lazy dog.";

            string[] listOriginal = ChangeToUpperCaseAndSplitByWord(original);
            string[] listTranscript = ChangeToUpperCaseAndSplitByWord(transcript);


            //First Check if String is less than the original or more than the original
            //less than - gauranteed missing words
            //more than - added words
            //same - less than, missing or added

            
            for(int i = 0; i < listOriginal.Length -1 ; i++)
            {
                Console.WriteLine($"{listOriginal[i]} and {listTranscript[i]}");
            }
            
        }

        static string[] ChangeToUpperCaseAndSplitByWord(string phrase)
        {
            return phrase.ToUpper().Split(' ');
        }

        static bool IsMissingWord(int index, string wordtocheck, string nextWord, string[] transcript)
        {


            return true;
        }
    }
}
