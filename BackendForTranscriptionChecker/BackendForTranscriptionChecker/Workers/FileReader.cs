using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackendForTranscriptionChecker.Workers
{
    class FileReader
    {
        /// <summary>
        ///No need to convert to uppercase since there is a StringComparison.OrdinalIgnoreCase
        ///StringOriginal.Equals(stringtoCompare, StringComparison.OrdinalIgnoreCase)
        ///Also take out punctuation.
        /// </summary>

        public List<string> Read(string filename)
        {
            string[] fileContent;
            string singleString;
            List<string> convertedList;

            try
            {
                fileContent = File.ReadAllLines(filename);
                singleString = string.Join(" ", fileContent);
                convertedList = SplitbyWord(singleString);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return convertedList;
        }

        private List<string> SplitbyWord(string phrase)
        {
            return phrase.Split(' ').ToList();
        }
    }
}
