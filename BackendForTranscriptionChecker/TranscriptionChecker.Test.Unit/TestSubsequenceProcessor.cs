using System;
using System.Collections.Generic;
using BackendForTranscriptionChecker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranscriptionChecker.Test.Unit
{
    [TestClass]
    public class TestSubsequenceProcessor
    {
        private readonly SubsequenceProcessor _subsequenceProcessor = new SubsequenceProcessor();

        [TestMethod]
        public void AllEmpty()
        {
            string[] refArray = { };
            string[] evalArray = { };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {

            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void AllCorrect()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D", "E" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>() { 
            "A B C D E",
            "A B C D",
            "A B C",
            "A B",
            "A",
            "B C D E",
            "B C D",
            "B C",
            "B",
            "C D E",
            "C D",
            "C",
            "D E",
            "D",
            "E"
            });

            Sort(expectedList);

            CollectionAssert.AreEqual(expectedList, processedactual);
        }


        [TestMethod]
        public void AllIncorrect()
        {
        string[] refArray = { "A", "B", "C", "D", "E" };
        string[] evalArray = { "N", "N", "N", "N,", "N" };

        List<string> expectedList = new List<string>();
        List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

        expectedList.AddRange(new List<string>() { 

        });

        Sort(expectedList);
        CollectionAssert.AreEqual(expectedList, processedactual);
        }


        [TestMethod]
        public void FirstIncorrect()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "N", "B", "C", "D", "E" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "B C D E",
                "B C D",
                "B C",
                "B",
                "C D E",
                "C D",
                "C",
                "D E",
                "D",
                "E"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void LastIncorrect()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D,", "N" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "A B C D",
                "A B C",
                "A B",
                "A",
                "B C D",
                "B C",
                "B",
                "C D",
                "C",
                "D"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }




        ///METHODS
        void Sort(List<string> expectedList)
        {
            //Sorts Alphabetically by the first word in the string
            expectedList.Sort((x, y) =>
                string.Compare(
                    x.Split(Constants.s)[0],
                    y.Split(Constants.s)[0]));

            //Sorts By Number of words in String
            expectedList.Sort((x, y) => y.Split(Constants.s).Length - x.Split(Constants.s).Length);
        }
    }
}
