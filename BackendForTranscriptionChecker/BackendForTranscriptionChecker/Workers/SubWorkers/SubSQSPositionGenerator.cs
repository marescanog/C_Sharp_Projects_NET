using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace BackendForTranscriptionChecker.Workers.SubWorkers
{
    class SubSQSPositionGenerator
    {
        public List<int> GenerateListOfPositions_forthisSubSqs(MatchCollection matchSubSqsinEvalString)
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

    }
}
