using System;
using System.Collections.Generic;

namespace BackendForTranscriptionChecker.Objects
{
    class Subsequence
    {
        private List<string> _sequenceList = new List<string>();
        private string _sequenceString;
        private int _value;

        public Subsequence(List<string> stringList)
        {
            foreach(string word in stringList)
            {
                _sequenceList.Add(word);
            }
            _sequenceString = String.Join(Constants.space, _sequenceList);
            _value = _sequenceList.Count;
        }

        public Subsequence(string emptyString)
        {
            _sequenceString = string.Empty;
            _value = 0;
        }

            public int GetValue()
        {
            return _value;
        }

        public string GetString()
        {
            return _sequenceString;
        }

        public List<string> GetList()
        {
            return _sequenceList;
        }
    }
}
