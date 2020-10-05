using BackendForTranscriptionChecker.Objects;
using System;
using System.CodeDom;
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
                GetAllValidMatches(listOfAllPossibleSubsequences, refList, sEvalString);
                //CustomSort_Alpha_WC(listOfAllPossibleSubsequences);
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

        private static void GetAllValidMatches(List<string> listOfAllValidSubsequences, List<string> refList, string sEvalString)
        {
            Dictionary<string, Subsequence> subSqsDictionary = new Dictionary<string, Subsequence>();

            //Computing time is roughly (n^2)/2

            while (refList.Count > 0) //Loop Used for scanning reflist
            {
                List<string> refListCopy = new List<string>(refList); //Creates a new list each iteration since refList dynamically changes 
                bool isMatchFound = false;

                while (refListCopy.Count > 0 && !isMatchFound) //Loop Used for scanning reflistCopy
                {
                    string sequence = String.Join(Constants.space, refListCopy);
                    bool isSubSqsInSubSqs = false;
                   
                    if (!listOfAllValidSubsequences.Contains(sequence, StringComparer.OrdinalIgnoreCase))
                    {
                        Regex regexSubSqs = new Regex(string.Concat(Constants.regexPatternBuildStart, sequence, Constants.regexPatternBuildEnd), RegexOptions.IgnoreCase);
                        MatchCollection matchSubSqsinEvalString = regexSubSqs.Matches(sEvalString);

                        if(matchSubSqsinEvalString.Count>0)
                        {
                            List<int> listPos = new List<int>(GenerateListOfPositions_forthisSubSqs(matchSubSqsinEvalString));

                            //ByPass sbsqs checking if this sequence is the first Value - Add to List Right Away
                            if (listOfAllValidSubsequences.Count != 0)
                            {
                                /////new method
                                ///Checks if the current sequence is a subsequence of another subsequence
                                ///Goes through the list of all possible subsequences
                                foreach (var otherSubSqs in listOfAllValidSubsequences)
                                {
                                    MatchCollection matchSubSQSinSubSQS = regexSubSqs.Matches(otherSubSqs);

                                    //Bypass subsqs in sbsqs Verification if not found in current otherSubSqs element in list
                                    if (matchSubSQSinSubSQS.Count > 0)
                                    {
                                        //check how many matches are found in the subsequence
                                        //int totalMatchesinOtherSubSQS = subSqsDictionary[otherSubSqs].GetTotalMatches(); //what if there's 2 matches?

                                        int totalMatchesForThisSubSQS = matchSubSqsinEvalString.Count;
                                        foreach (var match in matchSubSqsinEvalString)
                                        {


                                            //compare with each value in totalMatchesinOtherSubSQS
                                        }

                                        ///Check if it has a lone match aside from matches in other subsequences
                                        ///*
                                        ///     ///array match with position?
                                        ///      data needed otherSubSqs.match count, dictionary with [matchNumber, postion] -> or just list, yes list case match number 0above
                                        ///     
                                        ///      for each match in matchSubSqsinEvalString match its position range in relation to otherSubSqs position ranges
                                        ///      index-posRan       index-posRan
                                        ///      1 - 4 + len        1 - 4 + len
                                        ///      2 - 8 + len        2 - 8 + len
                                        ///      3 - 12 + len
                                        ///     
                                        ///       break when range does not match/out of bounds (start, end)
                                        /// (4, 6)      (4,8)   if start > start and end < end
                                        /// (10, 12)     (9,13)
                                        /// (13, 115)       
                                        /// 
                                        /// 
                                        ///

                                    }
                                }

                                //Add to List of Valid Subsqs since it is a valid Subsequence
                                if (!isSubSqsInSubSqs)
                                {
                                    AddToListOfSubSQS(sequence, listPos, matchSubSqsinEvalString);
                                }
                            }
                            else
                            {
                                //Valid Subsequence since it is the first match
                                AddToListOfSubSQS(sequence, listPos, matchSubSqsinEvalString);
                            }

                            isMatchFound = true;
                        }
                    }
                    refListCopy.RemoveAt(refListCopy.Count - 1);//Remove Last Element
                }

                refList.Remove(refList[0]);//Remove FirstElement
            }

            void AddToListOfSubSQS(string sequence, List<int> listPos, MatchCollection matchSubSqsinEvalString)
            {
                Subsequence temp = new Subsequence(sequence, sequence.ToCharArray().Length, listPos, matchSubSqsinEvalString.Count);
                listOfAllValidSubsequences.Add(sequence);
                subSqsDictionary.Add(sequence, temp); //key is sequence string to find the SubSequence Object
            }
        }

        private static List<int> GenerateListOfPositions_forthisSubSqs(MatchCollection matchSubSqsinEvalString)
        {
            List<int> listPos = new List<int>();

            //Makes list of positions for all the matches in this subsequence
            foreach (Match match in matchSubSqsinEvalString)
            {
                for (int i = 1; i <= 2; i++)
                {
                    Group g = match.Groups[i];
                    CaptureCollection cc = g.Captures;
                    for (int j = 0; j < cc.Count; j++)
                    {
                        Capture c = cc[j];
                        listPos.Add(c.Index);
                    }
                }
            }

            return listPos;
        }

        private static void CustomSort_Alpha_WC(List<string> listOfAllPossibleSubsequences)
        {
            if (listOfAllPossibleSubsequences.Count > 1)
            {
                //Sorts Alphabetically by the first word in the string
                listOfAllPossibleSubsequences.Sort((x, y) =>
                    string.Compare(
                        x.Split(Constants.s)[0],
                        y.Split(Constants.s)[0]));

                //Sorts By NWC - Word Count in String
                listOfAllPossibleSubsequences.Sort((x, y) => y.Split(Constants.s).Length - x.Split(Constants.s).Length);
            }
        }
        
    }
}
