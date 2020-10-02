using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BackendForTranscriptionChecker.Workers
{
    
    class VectorDistanceCalculator
    {
        public Dictionary<int, double> ComputeCoordinates(Dictionary<int, int> _data)
        {
            Dictionary<int, double> ProcessedData = new Dictionary<int, double>();

            try
            {
                Dictionary<int, int> data = _data;

                foreach (var keypair in data)
                {
                    ProcessedData.Add(keypair.Key, GetDistance(keypair.Key, keypair.Value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Computation of Vector Went wrong: " + ex.Message);
                throw ex;
            }

            return ProcessedData;
        }

        private static double GetDistance(int x2, int y2)
        {
           return Math.Sqrt(Math.Pow((x2 - 0), 2) + Math.Pow((y2 - 0), 2));
        }
    }
}
