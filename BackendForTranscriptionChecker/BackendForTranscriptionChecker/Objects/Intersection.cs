using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker.Objects
{
    class Intersection
    {
        private string _stringInterX;
        private int _startPos;
        private int _endPos;

        public Intersection(string segment, int startPos, int endPos)
        {
            _stringInterX = segment;
            _startPos = startPos;
            _endPos = endPos;
        }

        public string GetString()
        {
            return _stringInterX;
        }

        public int GetStartPos()
        {
            return _startPos;
        }
        public int GetEndPos()
        {
            return _endPos;
        }
    }
}
