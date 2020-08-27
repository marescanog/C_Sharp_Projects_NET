using System;
using System.IO;

namespace WordUnscrambler.Workers
{
    class FileReader
    {
        string[] fileContent;
        public string[] Read(string filename)
        {
            try 
            {
                string[] fileContent = File.ReadAllLines(filename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return fileContent;
        }
    }
}
