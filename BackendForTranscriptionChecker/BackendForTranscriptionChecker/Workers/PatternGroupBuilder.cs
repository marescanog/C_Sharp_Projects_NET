using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker.Workers
{
    class PatternGroupBuilder
    {
        private CrossChecker _crossChecker = new CrossChecker();

        public string[] GroupSuccessiveCorrectWords(string[] refArray, string[] evalArray)
        {
            string[] correctWords = _crossChecker.GetCorrectWords(refArray, evalArray);

            List<string> groupedWords = new List<string>();
            List<string> listOfGroupedWords = new List<string>();

            // B A F F A B
            // A A B

            for (int i=0, k=0; i<=correctWords.Length;)
            {
                if(i== correctWords.Length)
                {
                    ResetList(groupedWords, listOfGroupedWords);
                    i++;
                }
                else if(correctWords[i]== evalArray[k])
                {
                    groupedWords.Add(correctWords[i]);
                    i++;
                    k++;
                }
                else
                {
                    if(groupedWords.Count==0)
                    {
                        k++;
                    }
                    else
                    {
                        ResetList(groupedWords, listOfGroupedWords);
                        k++;
                    }
                }
            }

            return listOfGroupedWords.ToArray();
        }

        private static void ResetList(List<string> groupedWords, List<string> listOfGroupedWords)
        {
            listOfGroupedWords.Add(String.Join(Constants.space, groupedWords));
            groupedWords.Clear();
        }
    }
}
