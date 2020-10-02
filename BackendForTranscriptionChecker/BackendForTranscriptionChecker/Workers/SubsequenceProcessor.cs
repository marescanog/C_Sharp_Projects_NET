using BackendForTranscriptionChecker.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackendForTranscriptionChecker
{
    class SubsequenceProcessor
    {
        List<Subsequence> listOfSubSequences = new List<Subsequence>();
        List<string> subsequence = new List<string>();

        public void ProcessMatch(string[] refArray, string[]evalArray)
        {
            
            listOfSubSequences.Clear();
            subsequence.Clear();

            Subsequence temp = new Subsequence(subsequence);
            int icounter = 0;

            for (int i=0; i<refArray.Length; i++)
            {
                for (int j=0; j<evalArray.Length; j++)
                {
                    if(i + icounter < refArray.Length && refArray[i + icounter].Equals(evalArray[j], StringComparison.OrdinalIgnoreCase))
                    {
                        if (i + icounter < refArray.Length )
                        {
                            subsequence.Add(refArray[i + icounter]);
                            icounter++;
                        }
                    }
                    else if(subsequence.Count != 0)
                    {
                        AddSubsequenceInList(temp);
                        subsequence.Clear();
                        icounter = 0;
                    }
                }

                if (subsequence.Count != 0)
                {
                    AddSubsequenceInList(temp);
                    subsequence.Clear();
                }

                icounter = 0;
            }

            //OutSideForLoop
            SortListOfSubSequences();
        }

        private void AddSubsequenceInList(Subsequence temp)
        {
            if ((!HasSubsequenceInList(temp) || listOfSubSequences.Count == 0) && subsequence.Count != 0)
            {
                listOfSubSequences.Add(new Subsequence(subsequence));
            }
        }

        private bool HasSubsequenceInList(Subsequence temp)
        {
            bool hasSubsequence = false;

            for (int subIn = 0; subIn < listOfSubSequences.Count; subIn++)
            {
                if (temp.GetString().Equals(listOfSubSequences[subIn].GetString(), StringComparison.OrdinalIgnoreCase))
                {
                    hasSubsequence = true;
                    break;
                }
            }

            return hasSubsequence;
        }

        private Subsequence GetLongestSubsequence(List<Subsequence> subsequenceList)
        {
            Subsequence longestSubsequence = new Subsequence(string.Empty);
            int maxCount = int.MinValue;
            foreach (var sequence in subsequenceList)
            {
                if(maxCount<sequence.GetValue())
                {
                    maxCount = sequence.GetValue();
                    longestSubsequence = sequence;
                }
            }

            return longestSubsequence;
        }

        private void SortListOfSubSequences()
        {
            Dictionary<Subsequence, int> tempList = new Dictionary<Subsequence, int>();
            if (listOfSubSequences.Count != 0)
            {
                foreach(var sub in listOfSubSequences)
                {
                    tempList.Add(sub, sub.GetValue());
                }

                var sortedDict2 = from entry in tempList orderby entry.Value descending select entry;

                listOfSubSequences.Clear();

                foreach(var sub in sortedDict2)
                {
                    listOfSubSequences.Add(sub.Key);
                }
            }

        }

        public List<Subsequence> GetListOfSubsequences()
        {
            return listOfSubSequences;
        }
    }
}
