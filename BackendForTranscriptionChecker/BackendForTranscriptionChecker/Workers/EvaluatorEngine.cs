using BackendForTranscriptionChecker.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace BackendForTranscriptionChecker.Workers
{
    class EvaluatorEngine
    {
        private readonly SubsequenceProcessor _subsequenceProcessor = new SubsequenceProcessor();

        public List<string> Evaluate(string[] refArray, string[] evalArray)
        {
            List<string> possibleSubsequence = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            Dictionary<string, Subsequence> _subSqsDictionary = new Dictionary<string, Subsequence>(_subsequenceProcessor.RetreiveDisctionaryOfSubsequences());

            /* Notes

            //for each sequence  that intersects with one another in possible subsequence, create a Correction Pattern
            while (possibleSubsequence.Count!=0)
            {               
                possibleSubsequence.Remove(possibleSubsequence[0]);
            }
            */









            return possibleSubsequence;
        }







    }
}
