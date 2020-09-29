using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace BackendForTranscriptionChecker.Workers
{

    class EvaluatorEngine
    {
        RegExPatternCreator _regExPatternCreator = new RegExPatternCreator();

        public EvaluatorEngine(RegExPatternCreator regExPatternCreator)
        {
            _regExPatternCreator = regExPatternCreator;
        }

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
                        List<string> intersectionOfSegmentedLists = GetInterSection(referenceSegment, evaluation);

                        if (referenceSegment.Count() >= intersectionOfSegmentedLists.Count()) //means missing or changed words
                        {
                            string pattern = CreateRegExPatternIncorrectWords(referenceSegment, intersectionOfSegmentedLists);
                            string data = string.Join(" ", referenceSegment);
                            data = String.Concat(data, " ");
                            int instances = CountInstances(pattern);

                            List<string> incorrectWords = ExtractIncorrectWords(data, pattern, instances);

                            Console.WriteLine("Stop");
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

        
        private static List<string> ExtractIncorrectWords(string data, string patten, int instances)
        {
            Regex r = new Regex(patten, RegexOptions.IgnoreCase);
            Match incorrectWords = r.Match(data);

            List<string> missingwords = new List<string>();

            for (int i = 1; i <= instances; i++)
            {
                if (incorrectWords.Groups[i].Value != string.Empty)
                    missingwords.Add(incorrectWords.Groups[i].Value);
            }

            return missingwords;
        }

        private static int CountInstances(string pattern)
        {
            string[] array = pattern.Split(' ');
            int count = 0;
            foreach (var item in array)
            {
                if (item == "(.*?)") count++;
            }

            return count;
        }
        public string CreateRegExPatternIncorrectWords(List<string> referenceSegment, List<string> intersectionOfSegmentedLists)
        {
            List<string> Temp = new List<string>();
            for (int i = 0, j = 0; i < referenceSegment.Count(); i++)
            {
                if (!referenceSegment[i].Equals(intersectionOfSegmentedLists[j], StringComparison.OrdinalIgnoreCase))
                {
                    Temp.Add("(.*?)");
                }
                else
                {
                    Temp.Add(referenceSegment[i]);

                    if (j < intersectionOfSegmentedLists.Count - 1) j++;
                }

            }

            string regularExText = string.Join(" ", Temp);
            return String.Concat(regularExText, " (.*?)");
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
