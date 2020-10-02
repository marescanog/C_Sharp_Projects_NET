using BackendForTranscriptionChecker.Objects;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BackendForTranscriptionChecker.Workers
{
    class SubsequenceValidator
    {
        public List<Subsequence> ValidateListofSubsequences(List<Subsequence> listofSubsequences, string[] refArray, string[] evalArray)
        {
            List<Subsequence> validSubsequenceList = new List<Subsequence>();
            MatchCollection matches;
            MatchCollection matchesRef;

            string evalArrayText = String.Join(Constants.space, evalArray);
            string refArrayText = String.Join(Constants.space, evalArray);

            try
            {
                for (int i=0; i < listofSubsequences.Count;i++)
                {
                    matches = Regex.Matches(evalArrayText, listofSubsequences[i].GetString());
                    matchesRef = Regex.Matches(refArrayText, listofSubsequences[i].GetString());


                    if (matches.Count != 0)
                    {
                        validSubsequenceList.Add(listofSubsequences[i]);
                        evalArrayText = Regex.Replace(evalArrayText, listofSubsequences[i].GetString(), string.Empty, RegexOptions.None, TimeSpan.FromSeconds(.25));
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
