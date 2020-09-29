using System;
using System.Collections.Generic;
using System.Linq;


namespace BackendForTranscriptionChecker.Workers
{
    class RegExPatternCreator
    {
        public string CreateRegexPattern(string[] reference, string[] evaluation)
        {

            string[] intersection = GetInterSection(reference, evaluation);
            List<string> Temp = new List<string>();

            if (intersection.Count()==0)
            {
                //Turns every word into a delimeter
                foreach(var item in reference)
                {
                    Temp.Add(Constants.delimiter);
                }
            }
            else
            {
                for (int i = 0, j = 0; i < reference.Count(); i++)
                {
                    if (!reference[i].Equals(intersection[j], StringComparison.OrdinalIgnoreCase))
                    {
                        Temp.Add(Constants.delimiter);
                    }
                    else
                    {
                        Temp.Add(reference[i]);

                        if (j < intersection.Length - 1) j++;
                    }

                }
            }

            return string.Join(Constants.space, Temp);
        }

        private string[] GetInterSection(string[] reference, string[] evaluation)
        {
            var intersection = reference.Intersect(evaluation);
            return intersection.ToArray();
        }
    }
}
