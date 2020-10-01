using System.Collections.Generic;
using System;
using System.Linq;

namespace BackendForTranscriptionChecker.Workers
{
    class CrossChecker
    {
        List<string> correctWords = new List<string>();

        public string[] GetCorrectWords(string[] refArray, string[] evalArray)
        {
            int maxRef = refArray.Length -1 , maxEval = evalArray.Length -1;

            string[] missingwords = GetMissingWordsFromArray(refArray, evalArray);
            string[] processedRefArray = (missingwords.Length != 0) ? ReplaceMissingWordsWithDelimter(refArray, missingwords): refArray;
            /*
            string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "B", "B", "B", "I", "I", "I", "I", "F" };

                string[] Expected = {"A B"}
            */

            for (int i = 0, k = 0; i <= maxRef || k <= maxEval;)
            {

                if (processedRefArray[i].Equals(Constants.delimiter))
                {
                    if ((i == maxRef) && (k == maxEval))
                    {
                        i++; k++;
                    }
                    else if (i < maxRef) i++;
                    else k++;
                }
                else if (processedRefArray[i].Equals(evalArray[k], StringComparison.OrdinalIgnoreCase))
                {
                    correctWords.Add(refArray[i]);
                    if ((i == maxRef) && (k == maxEval))
                    {
                        i++; k++;
                    }
                    else
                    {
                        if (i < maxRef) i++;
                        if (k < maxEval) k++;
                    }
                }
                else
                {
                    if (i <= k)
                    {
                        int repetitions = SeekForwardRepetitions(i, processedRefArray);
                        if(repetitions>0)
                        {
                            repetitions = CheckForwardForAnyMatches(evalArray, repetitions, i, processedRefArray[i]);
                        }

                        if ((i == maxRef) && (k == maxEval))
                        {
                            i=i+1+ repetitions;
                            k++;
                        }
                        else if (i < maxRef)
                        {
                            i = i + 1 + repetitions;
                        }
                        else
                        {
                            if (k < maxEval) k++;
                        }
                    }
                    else
                    {
                        if ((i == maxRef) && (k == maxEval))
                        {
                            i++; k++;
                        }
                        else if (k < maxEval)
                        {
                            k++;
                        }
                        else
                        {
                            if (i < maxRef) i++;
                        }
                    }
                }
            }
            return correctWords.ToArray();
        }

        private static int CheckForwardForAnyMatches(string[] evalArray, int repetitions, int currentIndex, string word)
        {
            for (int i = currentIndex; i< currentIndex + repetitions; i++ )
            {
                if(evalArray[i].Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    repetitions--;
                }
            }
            return repetitions;
        }

        private int SeekForwardRepetitions(int currentIndex, string[] array)
        {
            int count = 0;
            for (int i= currentIndex; i<array.Length; i++)
            {
                if(!array[i].Equals(Constants.delimiter) && i+1!=array.Length && array[i].Equals(array[i+1], StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

        private static string[] ReplaceMissingWordsWithDelimter(string[] refArray, string[] missingWords)
        {
            foreach(var word in missingWords)
            {
                refArray = refArray.Select(x => x.Replace(word, Constants.delimiter)).ToArray();
            }

            return refArray;
        }

        private static string[] GetMissingWordsFromArray(string[] refArray, string[] evalArray)
        {
            string[] intersection = refArray.Intersect(evalArray).ToArray();

            if(intersection.Length!=0)
            {
                foreach (string word in intersection)
                {
                    refArray = refArray.Where(val => val != word).ToArray();
                }

                return refArray.Intersect(refArray).ToArray();
            }

            return intersection;
        }

    }
}
