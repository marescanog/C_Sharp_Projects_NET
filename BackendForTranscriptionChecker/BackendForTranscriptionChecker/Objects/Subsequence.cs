using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BackendForTranscriptionChecker.Objects
{
    class Subsequence
    {
        private string _sequenceString;
        private int _totalMatches = 0; //include
        private List<int> _listOfPos; //index is match number, value is position
        private int _length = 0;

        public Subsequence(string sequence, int length, List<int> pos, int totalMatches)
        {
            _sequenceString = sequence;
            _totalMatches = totalMatches;
            _length = length;
            _listOfPos = new List<int>(pos);
        }

        public string GetString()
        {
            return _sequenceString;
        }

        public int GetTotalMatches()
        {
            return _totalMatches;
        }

        public int GetStartPosIndex(int matchNum)
        {
            return _listOfPos != null && matchNum != 0 && matchNum < _listOfPos.Count + 1 ? _listOfPos[matchNum-1] : -1 ;
        }

        public int GetEndPosIndex(int matchNum)
        {
            return _listOfPos != null && matchNum != 0 && matchNum < _listOfPos.Count + 1 ? _listOfPos[matchNum - 1] + _length -1 : -1;
        }

    }
}
