using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendForTranscriptionChecker.Objects;

namespace BackendForTranscriptionChecker.Workers
{
    class CorrectWordGroupListGenerator
    {
        private SubsequenceProcessor _subsequenceProcessor = new SubsequenceProcessor();
        private SubsequenceValidator _subsequenceValidator = new SubsequenceValidator();

        public string[] GetGroupOfSuccessiveCorrectWords(string[] refArray, string[] evalArray)
        {
            _subsequenceProcessor.ProcessMatch(refArray, evalArray);
            List<Subsequence> subsequenceList = _subsequenceProcessor.GetListOfSubsequences();
            List<Subsequence> validSubSeqEvalArray = _subsequenceValidator.ValidateListofSubsequences(subsequenceList, refArray, evalArray);
            List<string> correctWords = new List<string>();

            foreach (var sub in validSubSeqEvalArray)
            {
                correctWords.Add(sub.GetString());
            }

            return correctWords.ToArray();
        }
    }
}
