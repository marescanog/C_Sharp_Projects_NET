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
            string[] totalSubsequences = possibleSubsequence.ToArray();


            /*

            //for each sequence in possible subsequence, create a Correction Pattern
            while (possibleSubsequence.Count!=0)
            {
                
                
                
                
                
                
                possibleSubsequence.Remove(possibleSubsequence[0]);
            }
            */

            return possibleSubsequence;
        }







    }
}
