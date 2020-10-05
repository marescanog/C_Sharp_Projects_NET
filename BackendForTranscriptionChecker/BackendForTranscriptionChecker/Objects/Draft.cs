using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker.Objects
{
    class Draft

    {
        /*
        Regex r = new Regex(string.Concat("(", sequence, ")"), RegexOptions.IgnoreCase);
        Match match = r.Match(sEvalString);
        int matchCount = 0;
                        while (match.Success)
                        {
                            Console.WriteLine("Match" + (++matchCount));
                            for (int i = 1; i <= 2; i++)
                            {
                                Group g = match.Groups[i];
        Console.WriteLine("Group" + i + "='" + g + "'");

                                CaptureCollection cc = g.Captures;
                                for (int j = 0; j<cc.Count; j++)
                                {
                                    Capture c = cc[j];
        System.Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
                                }
        }
        match = match.NextMatch();
                        }

        */

        /*




        private static void CheckSubsequenceInSubsequence(List<string> listOfAllPossibleSubsequences)
        {
        //CUSTOM LAMBDA I MADE
            Func<List<string>, int, bool> isEqualInSizeTo_PreviousElement = (list, currentIndex)
                => list[currentIndex].Split(Constants.s).Length == list[currentIndex - 1].Split(Constants.s).Length;

            if (listOfAllPossibleSubsequences.Count>1)
            {
                List<string> resizeableList = new List<string>();

                TransferLargestElementsToNewList(listOfAllPossibleSubsequences, resizeableList);

                //Start at index 1 since first element is largest
                for (int i = 1, counter = 0; i < listOfAllPossibleSubsequences.Count; i++, counter++)
                {
                    if(!isEqualInSizeTo_PreviousElement(listOfAllPossibleSubsequences, i))
                    {
 
                    }
                }



            }


            void TransferLargestElementsToNewList(List<string> OriginalList, List<string> newList)
            {
                newList.Add(OriginalList[0]); //First Element is always the Largest in list

                for (int i = 1; i < OriginalList.Count && isEqualInSizeTo_PreviousElement(OriginalList, i); i++)
                {
                    newList.Add(OriginalList[i]);
                }
            }
        }















        */
    }
}
