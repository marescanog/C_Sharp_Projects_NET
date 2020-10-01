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

            /* Input Values here to check for reference
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "B", "C", "D", "E" };
            string[] correctWords = {"B" "C" "D" "E"}
            string[] expected = { "B C D E" };
            */

            for (int i = 0, k = 0; i <= correctWords.Length;)
            {
                if (i == correctWords.Length)
                {
                    ResetList();
                    i++;
                }
                else if (correctWords[i] == evalArray[k])
                {
                    // Detects if there are leading strings in Reference Array
                    int keyIndex = dynaRefArray.IndexOf(correctWords[i]);
                    if (keyIndex > 0 && groupedWords.Count !=0)
                    {
                        ResetList();

                        dynaRefArray.RemoveRange(0, keyIndex);

                        // Detects if there are repetitions in eval Array
                        if (k + 1 != evalArray.Length && evalArray[k].Equals(evalArray[k + 1], StringComparison.OrdinalIgnoreCase))
                        {
                            int evalRepCount = CountRepetitionsInArrayAtCurrentIndex(evalArray, k);
                            int correctWordRepCount = CountRepetitionsInArrayAtCurrentIndex(correctWords, i);
                            int repCount = evalRepCount - correctWordRepCount;

                            //Adjusts eval array index to take note of repeitions
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

        private static int CountRepetitionsInArrayAtCurrentIndex(string[] array, int currentIndex)
        {
            int repCount = 0;

            for (int i = currentIndex; i < array.Length; i++)
            {
                if (i + 1 != array.Length)
                {
                    if (array[i].Equals(array[i + 1], StringComparison.OrdinalIgnoreCase))
                    {
                        repCount++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return repCount;
        }

        private void ResetList()
        {
            listOfGroupedWords.Add(String.Join(Constants.space, groupedWords));
            groupedWords.Clear();
        }

    }
}
