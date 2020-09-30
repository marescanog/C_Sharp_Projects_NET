using System.Collections.Generic;

namespace BackendForTranscriptionChecker.Workers
{
    class CrossChecker
    {
        public string[] GetCorrectWords(string[] refArray, string[] evalArray)
        {
            List<string> correctWords = new List<string>();
            int maxRef = refArray.Length -1 , maxEval = evalArray.Length -1;

            for (int i = 0, k = 0; i <= maxRef || k <= maxEval;)
            {

                if (refArray[i].Equals(Constants.delimiter))
                {
                    if((i == maxRef)&& (k == maxEval)) 
                    {
                        i++;k++;
                    }
                    else if(i < maxRef) i++;
                }
                else if (refArray[i].Equals(evalArray[k]))
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
                        if ((i == maxRef) && (k == maxEval))
                        {
                            i++; k++;
                        }
                        else if (i < maxRef)
                        {
                            i++;
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


    }
}
