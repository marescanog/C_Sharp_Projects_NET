using BackendForTranscriptionChecker.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace BackendForTranscriptionChecker.Workers
{
    class EvaluatorEngine
    {
        private readonly SubsequenceProcessor _subsequenceProcessor = new SubsequenceProcessor();
        private readonly MatchHandler _matchHandler = new MatchHandler();

        public List<string> Evaluate(string[] refArray, string[] evalArray)
        {
            List<string> possibleSubsequence = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            string evalString = String.Join(Constants.space, evalArray);
            string refstring = String.Join(Constants.space, refArray);

            _matchHandler.HandleMatches(possibleSubsequence, evalString, refstring);


            /*
            foreach (var sequence in possibleSubsequence)
            {
                string evalStpattern = String.Join(Constants.space, sequence);
                Regex regex = new Regex(string.Concat(Constants.regexPatternBuildStart, evalStpattern, Constants.regexPatternBuildEnd), RegexOptions.IgnoreCase);
                MatchCollection matchesInEvalstring = regex.Matches(evalString);
                MatchCollection matchesInRefstring = regex.Matches(refstring);
                List<int> listPosEvalstring = GenerateListOfPositions_forthisSubSqs(matchesInEvalstring);
                List<int> listPosRefstring = GenerateListOfPositions_forthisSubSqs(matchesInRefstring);

                evalString = Regex.Replace(evalString, evalStpattern, "/");
                refstring = Regex.Replace(refstring, evalStpattern, "/");
            }

            evalString = String.Concat("/", evalString, "/");
            refstring = String.Concat("/", refstring, "/");

            string[] array = evalString.Split('/');
            string[] arrayRef = refstring.Split('/');
            */

            return possibleSubsequence;
        }
    }
}
