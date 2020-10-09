using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker.Objects
{
    class MatchObj
    {
        private readonly string _stringContent;
        private readonly int _startPos;
        private readonly int _endPos;
        private bool _isLocked = false;
        private int _nudge = 0;

        public MatchObj (string content, int startPos, int endPos)
        {
            _stringContent = content;
            _startPos = startPos;
            _endPos = endPos;
        }

        public string GetString()
        {
            return _stringContent;
        }

        public bool NudgeToLeft()
        {
            if(!_isLocked)
            {
                _nudge--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool NudgeToRight()
        {
            if (!_isLocked)
            {
                _nudge++;
                return true;
            }
            else
            {
                return true;
            }
        }

        public int GetStartPos()
        {
            return _startPos + _nudge;
        }
    }


}
