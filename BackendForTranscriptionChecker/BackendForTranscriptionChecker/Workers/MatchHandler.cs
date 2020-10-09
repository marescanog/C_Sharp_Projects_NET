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

            List<string> evalSeg = new List<string>(ConvertMatchList_ToStringList(evalMatches));
            List<string> refSeg = new List<string>(ConvertMatchList_ToStringList(refMatches));
            List<string> originalRefString = new List<string>(SegmenttMatches(possibleSubsequences, refString));

            List<string> regexPatternCorrectmatch = new List<string>();
            List<string> regexPatternMismatch = new List<string>();

            for (int oS = 0, r = 0, e = 0; oS < originalRefString.Count;)
            {
                if(originalRefString[oS] == refSeg[r])
                {
                    if( refSeg[r] == evalSeg[e])
                    {
                        if(e < evalSeg.Count - 1 && r < refSeg.Count - 1)
                        {
                            //its Correct
                            regexPatternCorrectmatch.Add(Constants.delimiter);
                            regexPatternMismatch.Add(string.Concat("(?:", originalRefString[oS], ")"));

                            oS++;
                            e++;
                            r++;
                        }
                        else if (e < evalSeg.Count - 1)
                        {
                            regexPatternCorrectmatch.Add(Constants.delimiter);
                            regexPatternMismatch.Add(string.Concat("(?:", originalRefString[oS], ")"));
                            oS++;
                            e++;
                        }
                        else if (r < refSeg.Count - 1)
                        {
                            //Its correct
                            regexPatternCorrectmatch.Add(Constants.delimiter);
                            regexPatternMismatch.Add(string.Concat("(?:", originalRefString[oS], ")"));

                            oS++;
                            r++;
                        }
                        else
                        {
                            regexPatternCorrectmatch.Add(Constants.delimiter);
                            regexPatternMismatch.Add(string.Concat("(?:", originalRefString[oS], ")"));
                            oS++;
                        }
                    }
                    else
                    {
                        if(e < evalSeg.Count - 1)
                        {
                            //is a mistake
                            regexPatternCorrectmatch.Add(string.Concat("(?:", evalSeg[e], "?)"));
                            regexPatternMismatch.Add(Constants.delimiter);

                            e++;
                        }
                        else
                        {
                            if (r < refSeg.Count - 1)
                            {
                                //Not Sure
                                regexPatternCorrectmatch.Add(string.Concat("(?:", originalRefString[oS], ")"));
                                regexPatternMismatch.Add(Constants.delimiter);
                                r++;
                                oS++;
                            }
                            else
                            {
                                regexPatternCorrectmatch.Add(string.Concat("(?:", originalRefString[oS], ")"));
                                regexPatternMismatch.Add(Constants.delimiter);
                                oS++;
                            }
                        }
                    }
                }
                else
                {
                    //(?:regex) non capturing group since incorrect
                    regexPatternCorrectmatch.Add(string.Concat("(?:",originalRefString[oS], ")"));
                    regexPatternMismatch.Add(Constants.delimiter);
                    oS++;
                }
            }

            string regexPatCorMatch = String.Join("\\b", regexPatternCorrectmatch);
            string regexPatMisMatch = String.Join("\\b", regexPatternMismatch);

            Console.WriteLine("aaaaaaaaaaaaaah");
        }

        private List<string> ConvertMatchList_ToStringList(List<MatchObj> matchObjList)
        {
            List<string> listString = new List<string>();

            foreach(var match in matchObjList)
            {
                listString.Add(match.GetString());
            }

            return listString;
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
