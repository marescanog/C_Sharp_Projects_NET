using System;
using System.Collections.Generic;

namespace BackendForTranscriptionChecker.Objects
{
    class Subsequence
    {
        private string _sequenceString;
        private bool isLocked = false;
        private int numberOfElements;
        private int indexOfFirstElement;
        private string[] evalArray;

        public Subsequence(string sequence, string[] evalArray)
        {
            _sequenceString = sequence;
            this.evalArray = evalArray;
        }

        public Subsequence()
        {
            _sequenceString = string.Empty;
        }


        public string GetString()
        {
            return _sequenceString;
        }

    }
}
