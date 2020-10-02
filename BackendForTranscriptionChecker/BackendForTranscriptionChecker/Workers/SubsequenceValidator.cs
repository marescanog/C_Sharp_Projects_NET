using BackendForTranscriptionChecker.Objects;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BackendForTranscriptionChecker.Workers
{
    class SubsequenceValidator
    {
        public List<Subsequence> ValidateListofSubsequences(List<Subsequence> listofSubsequences, string[] array)
        {
            List<Subsequence> validSubsequenceList = new List<Subsequence>();
            MatchCollection matches;
            string test = String.Join(Constants.space, array);
            try
            {
                for (int i=0; i < listofSubsequences.Count;i++)
                {
                    matches = Regex.Matches(test, listofSubsequences[i].GetString());

                    if (matches.Count != 0)
                    {
                        validSubsequenceList.Add(listofSubsequences[i]);
                        test = Regex.Replace(test, listofSubsequences[i].GetString(), string.Empty, RegexOptions.None, TimeSpan.FromSeconds(.25));
                    }
                }
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("Subsequence Validator operation timed out.");
                Console.WriteLine("Returned words:");
            }
            return validSubsequenceList;
        }
    }
}
