using System;
using System.Collections.Generic;
using System.Linq;


namespace BackendForTranscriptionChecker.Workers
{
    class RegExPatternCreator
    {
        public string CreateRegexPattern(List<string> reference, List<string> evaluation)
        {

            List<string> intersection = GetInterSection(reference, evaluation);
            List<string> Temp = new List<string>();

            if (intersection.Count()==0)
            {
                foreach(var item in reference)
                {
                    Temp.Add("(.*?)");
                }
            }
            else
            {
                for (int i = 0, j = 0; i < reference.Count(); i++)
                {
                    if (!reference[i].Equals(intersection[j], StringComparison.OrdinalIgnoreCase))
                    {
                        Temp.Add("(.*?)");
                    }
                    else
                    {
                        Temp.Add(reference[i]);

                        if (j < intersection.Count - 1) j++;
                    }

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
    }
}
