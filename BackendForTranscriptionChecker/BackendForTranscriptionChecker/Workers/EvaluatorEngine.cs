using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker.Workers
{
    class EvaluatorEngine
    {       
        public void EvaluateText(List<string> reference, List<string> evaluation)
        {
            try
            {
                for (int i = 0, j=0; i < reference.Count; i++, j++)
                {
                    if(!reference[i].Equals(evaluation[i], StringComparison.OrdinalIgnoreCase))
                    {
                        //Get segment
                        List<string> referenceSegment = CaptureSegment(i, reference);
                        List<string> evalutaionSegment = CaptureSegment(i, evaluation);

                        //Get Intersection of those two segments
                        List<string> intersectionOfSegmentedLists = GetInterSection(referenceSegment, evalutaionSegment);

                        if (referenceSegment.Count() == intersectionOfSegmentedLists.Count()) //means changed words
                        {

                        }
                        else if (referenceSegment.Count() > intersectionOfSegmentedLists.Count()) //means missing words
                        {
                            CreateRegularExpressionForMissingWords(referenceSegment, intersectionOfSegmentedLists);
                        }
                        else if (referenceSegment.Count() < intersectionOfSegmentedLists.Count()) //means added words
                        { 

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void CreateRegularExpressionForMissingWords(List<string> referenceSegment, List<string> intersectionOfSegmentedLists)
        {
            int countMissing = 0;
            string check;

            for (int i = 0; i < referenceSegment.Count(); i++)
            {
                check = (i < intersectionOfSegmentedLists.Count) ? intersectionOfSegmentedLists[i] : string.Empty;

                if (!referenceSegment[i].Equals(check, StringComparison.OrdinalIgnoreCase))
                {
                    //The quick brown fox jumps over the lazy dog
                    //The black fox jumps over the lazy dog
                    intersectionOfSegmentedLists.Insert(i, "(.*?)");
                    countMissing++;
                }

            }
            string regularExText = string.Join(" ", intersectionOfSegmentedLists);
            string reference = string.Join(" ", referenceSegment);

            regularExText = String.Concat(regularExText, " (.*?)");
            reference = String.Concat(reference, " ");

            Regex r = new Regex(regularExText, RegexOptions.IgnoreCase);
            Match mc = r.Match(reference);

            List<string> missingwords = new List<string>();

            for(int i=1; i<= countMissing; i++)
            {
                missingwords.Add(mc.Groups[i].Value);
            }

        }

        private List<string> GetInterSection(List<string> reference, List<string> evaluation)
        {
            var intersection = reference.Intersect(evaluation);
            return intersection.ToList();
        }
        
        private List<string> CaptureSegment(int currentIndex, List<string> list)
        {
            List<string> temp = new List<string>();
            int maxIndexforSegment = createSegmentIndex(currentIndex, list);

            for(int i=currentIndex; i <= maxIndexforSegment; i++)
            {
                temp.Add(list[i]);
            }

            return temp;
        }

        private int createSegmentIndex(int currentIndex, List<string> list)
        {
            return isTail(currentIndex, list) ? list.Count() - 1 : currentIndex + 6;
        }

        private bool isTail(int currentIndex, List<string> list)
        {
            return !(list.Count() - 1 - currentIndex > 6);
        }
    }
}
