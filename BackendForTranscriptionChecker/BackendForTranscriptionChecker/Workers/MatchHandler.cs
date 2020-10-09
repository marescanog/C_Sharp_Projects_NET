using BackendForTranscriptionChecker.Objects;
using BackendForTranscriptionChecker.Workers.SubWorkers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker.Workers
{
    class MatchHandler
    {
        private readonly SubSQSPositionGenerator _subSQSPositionGenerator = new SubSQSPositionGenerator();

        public void HandleMatches(List<string> possibleSubsequences, string evalString, string refString)
        {
            List<MatchObj> evalMatches = new List<MatchObj>(GetMatches(possibleSubsequences, evalString));
            List<MatchObj> refMatches = new List<MatchObj>(GetMatches(possibleSubsequences, refString));


            List<string> test = new List<string>(SegmenttMatches(possibleSubsequences, refString));

            //Need to Get Broken Down Original


            //Get Max capacity
            int maxCapEval = evalString.Split(Constants.s).Length;
            int maxCapRef = refString.Split(Constants.s).Length;
            int maxCapoverall = maxCapEval >= maxCapRef ? maxCapEval : maxCapRef;

            /////Assign Array - Make into function Return MatchObj[], gets int MaxCapacity, gets List<MatchObj> matchObjList
            ///returns List<MatchObj>
            MatchObj[] evalMatchesArray = new MatchObj[maxCapoverall];
            MatchObj[] refMatchesArray = new MatchObj[maxCapoverall];

            List<MatchObj> evalMatchesNew = new List<MatchObj>(evalMatchesArray.ToList());
            List<MatchObj> refMatchesNew = new List<MatchObj>(refMatchesArray.ToList());

            for (int i=0; i< evalMatches.Count; i++)
            {
                evalMatchesNew[i] = evalMatches[i];
            }

            for (int i = 0; i < refMatches.Count; i++)
            {
                refMatchesNew[i] = refMatches[i];
            }
            /////////

            /////Move to right
            for (int i = 0; i < evalMatchesArray.Length; i++)
            {

            }




                //if the first position in evalMatches is 0 then no starting words were added/changed/
                //if the first position in refMatches is 0 then no starting words were dropped <--
                //if the first potition in refmatches 

                Console.WriteLine("aaaaaaaaaaaaaah");
        }


        private List<MatchObj> GetMatches(List<string> possibleSubsequences, string dataString)
        {
            List<MatchObj> listOfMatches = new List<MatchObj>();

            foreach(var sequence in possibleSubsequences)
            {
                Regex regex = new Regex(string.Concat(Constants.regexPatternBuildStart, sequence, Constants.regexPatternBuildEnd), RegexOptions.IgnoreCase);
                MatchCollection matchSubSqsinDataString = regex.Matches(dataString);
                List<int> dataListPos = new List<int>(_subSQSPositionGenerator.GenerateListOfPositions_forthisSubSqs(matchSubSqsinDataString));
                int length = sequence.Split(Constants.s).Length;

                if(matchSubSqsinDataString.Count>0)
                {
                    foreach (var pos in dataListPos)
                    {
                        listOfMatches.Add(new MatchObj(sequence, pos, pos + length - 1));
                    }
                }

                //alter datastring
                dataString = Regex.Replace(dataString, string.Concat(Constants.regexPatternBuildStart, sequence, Constants.regexPatternBuildEnd), "/");
            }

            SortMatches(listOfMatches);

            return listOfMatches;
        }

        private List<string> SegmenttMatches(List<string> possibleSubsequences, string dataString)
        {
            List<string> listOfMatches = new List<string>();

            int replaceCount = 0;

            foreach(var sequence in possibleSubsequences)
            {
                if(dataString.Contains(sequence))
                {
                    Regex regex = new Regex(String.Concat(Constants.regexPatternBuildStart, sequence, Constants.regexPatternBuildEnd), RegexOptions.IgnoreCase);
                    dataString = regex.Replace(dataString, string.Concat("/<", replaceCount, ">/"));
                    replaceCount++;
                }
            }

            List<string> dataArr = dataString.Split('/').ToList();

            for(int i=0; i< replaceCount; i++)
            {
                int IndexToReplace = dataArr.IndexOf(string.Concat("<", i, ">"));
                dataArr[IndexToReplace] = possibleSubsequences[i];
            }

            foreach(var segment in dataArr)
            {
                if( segment!=" ")
                {
                    if(segment != string.Empty)
                    {
                        segment.TrimStart(Constants.s);
                        segment.TrimEnd(Constants.s);
                        listOfMatches.Add(segment);
                    }
                }
            }

            return listOfMatches;
        }

        private void SortMatches(List<MatchObj> listOfMatches)
        {
            listOfMatches.Sort((x, y) => x.GetStartPos().CompareTo(y.GetStartPos()));
        }

    }
}
