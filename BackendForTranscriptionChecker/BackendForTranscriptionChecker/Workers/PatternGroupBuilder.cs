using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker.Workers
{
    class PatternGroupBuilder
    {
        private readonly CrossChecker _crossChecker = new CrossChecker();
        private List<string> groupedWords = new List<string>();
        private List<string> listOfGroupedWords = new List<string>();

        public string[] GroupSuccessiveCorrectWords(string[] refArray, string[] evalArray)
        {
            string[] correctWords = _crossChecker.GetCorrectWords(refArray, evalArray);
            List<string> dynaRefArray = refArray.ToList();

            for (int i = 0, k = 0; i <= correctWords.Length;)
            {
                if (i == correctWords.Length)
                {
                    ResetList();
                    i++;
                }
                else if (correctWords[i] == evalArray[k])
                {
                    int keyIndex = dynaRefArray.IndexOf(correctWords[i]);

                    if (keyIndex > 0 && groupedWords.Count !=0)
                    {
                        int repCount = 0;

                        ResetList();
                        dynaRefArray.RemoveRange(0, keyIndex);

                        if (k + 1 != evalArray.Length && evalArray[k].Equals(evalArray[k + 1], StringComparison.OrdinalIgnoreCase)) // Detects if there are repetitions
                        {
                            //Counts number of  back to back repeititons forwards
                            for (int rep = k; rep < evalArray.Length; rep++)
                            {
                                if (rep + 1 != evalArray.Length)
                                {
                                    if(evalArray[rep].Equals(evalArray[rep + 1], StringComparison.OrdinalIgnoreCase))
                                    {
                                        repCount++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }


                            k = k + repCount;

                        }
                    }

                    dynaRefArray.Remove(correctWords[i]);
                    groupedWords.Add(correctWords[i]);
                    i++;
                    k++;
                }
                else
                {
                    if (groupedWords.Count == 0)
                    {
                        k++;
                    }
                    else
                    {
                        int keyIndex = dynaRefArray.IndexOf(correctWords[i]);

                        if (keyIndex > 0 && i!=0)
                        {
                            dynaRefArray.RemoveRange(0, keyIndex);
                        }

                        ResetList();
                        k++;
                    }
                }
            }

            return listOfGroupedWords.ToArray();
        }

        private void ResetList()
        {
            listOfGroupedWords.Add(String.Join(Constants.space, groupedWords));
            groupedWords.Clear();
        }

    }
}
