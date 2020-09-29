using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace BackendForTranscriptionChecker.Workers
{

    class EvaluatorEngine
    {
        RegExPatternCreator _regExPatternCreator;

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


                        string pattern = _regExPatternCreator.CreateRegexPattern(referenceSegment.ToArray(), evalutaionSegment.ToArray());

                        //CreateRegExPatternIncorrectWords(referenceSegment, intersectionOfSegmentedLists);
                        string data = string.Join(Constants.space, referenceSegment);
                        data = String.Concat(data, Constants.space);
                        int instances = CountInstances(pattern);

                        List<string> incorrectWords = ExtractIncorrectWords(data, pattern, instances);

                        Console.WriteLine("Stop");


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
                if (item == Constants.delimiter) count++;
            }

            return count;
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
