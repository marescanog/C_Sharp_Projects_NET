using BackendForTranscriptionChecker.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BackendForTranscriptionChecker.Workers
{
    class SubsequenceValidator
    {

        public void ValidateListofSubsequences(List<Subsequence> listofSubsequences, string[] refArray, string[] evalArray)
        {
            MatchCollection matches = Regex.Matches(String.Join(Constants.space, refArray), listofSubsequences[0].GetString());

            try
            {
                string test = Regex.Replace(String.Join(Constants.space, evalArray), listofSubsequences[0].GetString(), "booty");

                Console.WriteLine(test);
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("Word Scramble operation timed out.");
                Console.WriteLine("Returned words:");
            }
        }
    }
}
