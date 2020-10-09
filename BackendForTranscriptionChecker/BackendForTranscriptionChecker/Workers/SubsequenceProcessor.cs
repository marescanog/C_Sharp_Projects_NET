using BackendForTranscriptionChecker.Objects;
using BackendForTranscriptionChecker.Workers.SubWorkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BackendForTranscriptionChecker
{
    class SubsequenceProcessor
    {
        private readonly Dictionary<string, Subsequence> _subSqsDictionary = new Dictionary<string, Subsequence>();
        private readonly SubSQSPositionGenerator _subSQSPositionGenerator = new SubSQSPositionGenerator();
        private bool hasGeneratedListOfAllPossibleSubSequences = false;

        public List<string> GetListOfAllPossibleSubsequences(string[] refArray, string[] evalArray)
        {
            List<string> listOfAllPossibleSubsequences = new List<string>();
            List<string> refList = refArray.ToList();
            string sEvalString = String.Join(Constants.space, evalArray);

            try
            {
                GetAllValidMatches(listOfAllPossibleSubsequences, refList, sEvalString);
                FilterAllValidMatches(listOfAllPossibleSubsequences);

                CustomSort_Alpha_WC(listOfAllPossibleSubsequences);
                hasGeneratedListOfAllPossibleSubSequences = true;
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

        public Dictionary<string, Subsequence> RetreiveDisctionaryOfSubsequences() /// Maybe just make it return the Dictionary instead of a list (Refactor Code)
        {
            if(hasGeneratedListOfAllPossibleSubSequences)
            return _subSqsDictionary;
            else
                throw new Exception("Dictionary of Subsequences has not been generated: GetListOfAllPossibleSequences First");
        }

        private void GetAllValidMatches(List<string> listOfAllValidSubsequences, List<string> refList, string sEvalString)
        {
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
                        ////////
                        int totalMatchesForThissbSQS = matchSubSqsinEvalString.Count;

                        if (matchSubSqsinEvalString.Count>0)
                        {
                            
                            List<int> listPos = new List<int>(_subSQSPositionGenerator.GenerateListOfPositions_forthisSubSqs(matchSubSqsinEvalString));

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
                                        Subsequence data_otherSubSqs = _subSqsDictionary[otherSubSqs];
                                        int totalMatchesinOtherSubSQS = data_otherSubSqs.GetTotalMatches(); 

                                        //foreach (var foundMatch in matchSubSqsinEvalString)
                                        for (int index = 0; index < matchSubSqsinEvalString.Count; index++)
                                        {

                                            int startIndexThisSubsqsMatchElement = listPos[index];
                                            int endIndexThisSubsqsMatchElement = listPos[index] + sequence.ToCharArray().Length - 1;

                                            for (int otherSbSQSMatchNumber = 1; otherSbSQSMatchNumber <= totalMatchesinOtherSubSQS; otherSbSQSMatchNumber++)
                                            {
                                                int startIndexOtherSubsqs = data_otherSubSqs.GetStartPosIndex(otherSbSQSMatchNumber);
                                                int endIndexOtherSubsqs = data_otherSubSqs.GetEndPosIndex(otherSbSQSMatchNumber);

                                                bool isThisSbSQS_inside_OtherSbSQS = startIndexThisSubsqsMatchElement >= startIndexOtherSubsqs && endIndexThisSubsqsMatchElement <= endIndexOtherSubsqs;
                                                
                                                if(isThisSbSQS_inside_OtherSbSQS)
                                                {
                                                    totalMatchesForThissbSQS--;
                                                }
                                            }
                                        }
                                    }

                                    if(totalMatchesForThissbSQS <= 0)//change maybe total matches isn't best validator
                                    {
                                        isSubSqsInSubSqs = true;
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
                _subSqsDictionary.Add(sequence, temp); //key is sequence string to find the SubSequence Object
            }
        }

        private void FilterAllValidMatches(List<string> SubSqsList)
        {
            CustomSort_Reverse_Alpha_WC(SubSqsList);

            for (int i=0; i< SubSqsList.Count;)
            {
                Subsequence data_thisSubSqs = _subSqsDictionary[SubSqsList[i]];
                int totalMatchesForThissbSQS = data_thisSubSqs.GetTotalMatches();
                int dynamicTotalMatchThisSbSQS = data_thisSubSqs.GetTotalMatches();
                string thisSubSqsString = data_thisSubSqs.GetString();

                for (int k=i+1; k<SubSqsList.Count; k++)
                {
                    Regex regex= new Regex(string.Concat(Constants.regexPatternBuildStart, thisSubSqsString, Constants.regexPatternBuildEnd), RegexOptions.IgnoreCase);

                    Subsequence data_otherSubSqs = _subSqsDictionary[SubSqsList[k]];
                    int totalMatchesinOtherSubSQS = data_otherSubSqs.GetTotalMatches();
                    string otherSubSqsString = data_otherSubSqs.GetString();

                    Match match = regex.Match(otherSubSqsString);

                    if(match.Success)
                    {
                        for (int index = 1; index <= totalMatchesForThissbSQS; index++)
                        {
                            int startIndexThisSubsqsMatchElement = data_thisSubSqs.GetStartPosIndex(index);
                            int endIndexThisSubsqsMatchElement = data_thisSubSqs.GetEndPosIndex(index);

                            for (int otherSbSQSMatchNumber = 1; otherSbSQSMatchNumber <= totalMatchesinOtherSubSQS; otherSbSQSMatchNumber++)
                            {
                                int startIndexOtherSubsqs = data_otherSubSqs.GetStartPosIndex(otherSbSQSMatchNumber);
                                int endIndexOtherSubsqs = data_otherSubSqs.GetEndPosIndex(otherSbSQSMatchNumber);

                                bool isThisSbSQS_inside_OtherSbSQS = startIndexThisSubsqsMatchElement >= startIndexOtherSubsqs 
                                    && endIndexThisSubsqsMatchElement <= endIndexOtherSubsqs;

                                if (isThisSbSQS_inside_OtherSbSQS)
                                {
                                    dynamicTotalMatchThisSbSQS--;
                                }
                            }
                        }
                    }

                }

                //foreach (var foundMatch in matchSubSqsinEvalString)
                if (dynamicTotalMatchThisSbSQS <= 0)
                {
                    SubSqsList.Remove(data_thisSubSqs.GetString());
                    _subSqsDictionary.Remove(data_thisSubSqs.GetString());
                }
                else
                {
                    i++;
                }

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

        private static void CustomSort_Reverse_Alpha_WC(List<string> listOfAllPossibleSubsequences)
        {
            if (listOfAllPossibleSubsequences.Count > 1)
            {
                //Sorts Alphabetically by the first word in the string
                listOfAllPossibleSubsequences.Sort((x, y) =>
                    string.Compare(
                        x.Split(Constants.s)[0],
                        y.Split(Constants.s)[0]));

                //Sorts By NWC - Word Count in String
                listOfAllPossibleSubsequences.Sort((x, y) => x.Split(Constants.s).Length - y.Split(Constants.s).Length);
            }
        }

    }



}

