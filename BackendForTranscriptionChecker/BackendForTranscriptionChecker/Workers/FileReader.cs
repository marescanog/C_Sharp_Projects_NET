using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackendForTranscriptionChecker.Workers
{
    class FileReader
    {
        public List<string> Read(string filename)
        {
            string[] fileContent;
            string singleString;
            List<string> convertedList;

            try
            {
                fileContent = File.ReadAllLines(filename);
                singleString = string.Join(" ", fileContent);
                convertedList = SplitbyWordToUpper(singleString);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return convertedList;
        }

        private List<string> SplitbyWordToUpper(string phrase)
        {
            return phrase.ToUpper().Split(' ').ToList();
        }
    }
}
