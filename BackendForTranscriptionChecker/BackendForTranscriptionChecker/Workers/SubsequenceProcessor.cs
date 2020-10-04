using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BackendForTranscriptionChecker
{
    class SubsequenceProcessor
    {
        public List<string> GetListOfAllPossibleSubsequences(string[] refArray, string[] evalArray)
        {
            List<string> listOfAllPossibleSubsequences = new List<string>();
            List<string> refList = refArray.ToList();
            string sEvalString = String.Join(Constants.space, evalArray);

            try
            {
                while (refList.Count > 0)
                {
                    List<string> refListCopy = new List<string>(refList);

                    while (refListCopy.Count > 0)
                    {
                        string sequence = String.Join(Constants.space, refListCopy);

                        if (!listOfAllPossibleSubsequences.Contains(sequence, StringComparer.OrdinalIgnoreCase))
                        {
                            Match match = Regex.Match(sEvalString, sequence, RegexOptions.None, TimeSpan.FromSeconds(Constants.timeOutTime));

                            if (match.Success)
                            {
                                listOfAllPossibleSubsequences.Add(sequence);
                            }
                        }
                        refListCopy.RemoveAt(refListCopy.Count - 1);
                    }
                    refList.Remove(refList[0]);
                }

                if (listOfAllPossibleSubsequences.Count > 1)
                {
                    //Sorts Alphabetically by the first word in the string
                    listOfAllPossibleSubsequences.Sort((x, y) =>
                        string.Compare(
                            x.Split(Constants.s)[0],
                            y.Split(Constants.s)[0]));

                    //Sorts By Number of words in String
                    listOfAllPossibleSubsequences.Sort((x, y) => y.Split(Constants.s).Length - x.Split(Constants.s).Length);
                }
            }
            catch (RegexMatchTimeoutException ex)
            {
                Console.WriteLine(Constants.subProcessor);
                Console.Write(Constants.timedOutRegex);
                Console.Write(ex.Message);
                Console.WriteLine(Constants.stringError);
                Console.Write(sEvalString);
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.programError);
                Console.Write(ex.Message);
                throw ex;
            }

            return listOfAllPossibleSubsequences;
        }

    }
}
