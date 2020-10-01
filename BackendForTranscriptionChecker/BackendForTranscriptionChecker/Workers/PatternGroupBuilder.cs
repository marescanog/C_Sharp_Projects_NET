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

        public string[] GroupSuccessiveCorrectWords(string[] refArray, string[] evalArray)
        {
            string[] correctWords = _crossChecker.GetCorrectWords(refArray, evalArray);

            List<string> groupedWords = new List<string>();
            List<string> listOfGroupedWords = new List<string>();
            List<string> dynaRefArray = refArray.ToList();
            // B A F F A B
            // A A B
            //string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "H" };

            //string[] refArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
            //string[] evalArray = { "A", "B", "D", "D", "E", "G", "G", "H" };
            //Cross = A B D E G H


            //string[] refArray = { "C", "D", "E", "F", "G", "H" };
            //string[] evalArray = { "D", "D", "E", "G", "G", "H" };
            //Cross = D E G H
            //int keyIndex = Array.IndexOf(refArray, correctWords[i]);
            //if (keyIndex > 0)
            //{
            //start new list
            //}

            //string[] refArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
            //string[] evalArray = { "A", "B", "D", "D", "E", "G", "G", "H" };
            //Cross = A B D E G H

            //string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "H" }; // add another letter between B and D, next to C 
            //string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "H" }; add another G to eval array
            //Cross = A B D E G H

            //string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "H" };
            //string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "G", "H" };

            for (int i = 0, k = 0; i <= correctWords.Length;)
            {
                if (i == correctWords.Length)
                {
                    ResetList(groupedWords, listOfGroupedWords);
                    i++;
                }
                else if (correctWords[i] == evalArray[k])
                {
                    int keyIndex = dynaRefArray.IndexOf(correctWords[i]);

                    if (keyIndex > 0 && groupedWords.Count !=0)
                    {
                        int repCount = 0;

                        ResetList(groupedWords, listOfGroupedWords);

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

                            //If there are repeititions, removes them
                            //if (i != 0) //change check later??
                            
                            dynaRefArray.RemoveRange(0, keyIndex);
                            

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

                        ResetList(groupedWords, listOfGroupedWords);
                        k++;
                    }
                }
            }


            //AnalysisTempTransferBackhereIfthisisBetter(evalArray, correctWords, groupedWords, listOfGroupedWords);

            return listOfGroupedWords.ToArray();
        }

        private static void ResetList(List<string> groupedWords, List<string> listOfGroupedWords)
        {
            listOfGroupedWords.Add(String.Join(Constants.space, groupedWords));
            groupedWords.Clear();
        }

    }
}
