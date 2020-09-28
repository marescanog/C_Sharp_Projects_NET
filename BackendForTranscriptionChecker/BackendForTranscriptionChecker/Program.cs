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
            string original = "The quick brown fox jumps over the lazy dog";
            string transcript = "The brown quick fox jumps over the lazy dog";

            List<string> listOriginal = SplitbyWordToUpper(original);
            List<string> listTranscript = SplitbyWordToUpper(transcript);

            //var listIntersection = listOriginal.Intersect(listTranscript);
            //Console.WriteLine("test");

            //First Check if String is less than the original or more than the original
            //less than - gauranteed missing words
            //more than - added words
            //same - less than, missing or added
            
            for(int i = 0, j=0; i < listOriginal.Count -1 ; i++, j++)
            {
                Console.WriteLine($"{listOriginal[i]} and {listTranscript[j]}");

                if(!listOriginal[i].Equals(listTranscript[j]))
                {
                    if(IsMissingWord(i, listOriginal, listTranscript))
                    {
                        Console.WriteLine($"Missing Word {listOriginal[i]}");
                        j--;
                    }
                }
            }
            

        }

        static List<string> SplitbyWordToUpper(string phrase)
        {
            return phrase.ToUpper().Split(' ').ToList();
        }



        //string original = "The quick brown fox jumps over the lazy dog.";
        //string transcript = "The brown fox jumps quick over the lazy dog.";

        static bool IsMissingWord(int index, List<string> original , List<string> transcript)
        {
            string wordtocheck = original[index];

            if (!transcript.Contains(wordtocheck))
            {
                return true;
            }

            //Check if the next words are missing
            //transcript.IndexOf(wordtocheck)
            //index = 1
            //indexWhereWordIsFoundInTranscript = 4

            int indexWhereWordIsFoundInTranscript = transcript.IndexOf(wordtocheck);

            return true;
        }
    }
}
