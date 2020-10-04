using BackendForTranscriptionChecker.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BackendForTranscriptionChecker
{
    class SubsequenceProcessor
    {
        public void GetListOfAllPossibleSubsequences(string[] refArray, string[] evalArray)
        {
            List<string> listOfAllPossibleSubsequences = new List<string>();
            List<string> refList = refArray.ToList();
            string sEvalString = String.Join(Constants.space, evalArray);

            while (refList.Count > 0)
            {
                List<string> refListCopy = new List<string>(refList);
                string refListString = String.Join(Constants.space, refListCopy);


                while (refListCopy.Count > 0)
                {
                    string sequence = String.Join(Constants.space, refListCopy);

                    if (!listOfAllPossibleSubsequences.Contains(sequence, StringComparer.OrdinalIgnoreCase))
                    {
                        Match match = Regex.Match(sEvalString, sequence);

                        if (match.Success)
                        {
                            listOfAllPossibleSubsequences.Add(sequence);
                        }
                    }

                    refListCopy.RemoveAt(refListCopy.Count - 1);

                }

                refList.Remove(refList[0]);
            }

            Console.WriteLine("Check");
        }

    }
}
