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
            "A B C D E"
            });

            Sort(expectedList);

            CollectionAssert.AreEqual(expectedList, processedactual);
        }


        [TestMethod]
        public void AllIncorrect()
        {
        string[] refArray = { "A", "B", "C", "D", "E" };
        string[] evalArray = { "G", "X", "W", "G,", "P" };

        List<string> expectedList = new List<string>();
        List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

        expectedList.AddRange(new List<string>() { 

        });

        Sort(expectedList);
        CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void RepetitiveReferenceAllCorrect()
        {
            string[] refArray = { "A", "A", "A", "A", "A" };
            string[] evalArray = { "A", "A", "A", "A", "A" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>() {
            "A A A A A"
            });

            Sort(expectedList);

            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void RepetitiveEvalStringAllInCorrect()
        {
            string[] refArray = { "A", "A", "A", "A", "A" };
            string[] evalArray = { "U", "U", "U", "U", "U" };

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
                "A B C D"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }


        [TestMethod]
        public void OneMistakePos2()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "F", "C", "D", "E" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "C D E",
                "A"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void OneMistakePos3()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "F", "D", "E" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "A B",
                "D E"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void OneMistakePos4()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "F", "E" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "A B C",
                "E"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void LeadingTwoMistakes()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "C", "D", "E" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "C D E"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void LeadingThreeMistakes()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "D", "E" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "D E"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void LeadingFourMistakes()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "F", "E" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "E"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void Pattern1()
        {
            string[] refArray = { "A", "B", "A", "F", "A", "B" };
            string[] evalArray = { "D", "B", "A", "B", "A", "A", "B", "F" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "A B A",
                "B A",
                "F",
                "A B"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void Pattern2()
        {
            string[] refArray = { "A", "B", "A", "B", "A" };
            string[] evalArray = { "A", "B", "A", "F", "A" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "A B A",
                "A"
            }) ;

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void Pattern3()
        {
            string[] refArray = { "A", "B", "A", "B", "D", "A", "B", "F" };
            string[] evalArray = { "A", "B", "A", "F", "A", "B" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "A B A",
                "A B",
                "F"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        
        [TestMethod]
        public void Pattern4()
        {
            string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
            string[] evalArray = { "A", "B", "A", "F", "A", "B" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "A B A",
                "A B",
                "F",
                //"B A"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }
        

        [TestMethod]
        public void Pattern5()
        {
            string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
            string[] evalArray = { "B", "A", "F", "A", "B" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "B A",
                "A B",
                "F"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void Pattern6()
        {
            string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "B", "B", "B", "I", "I", "I", "I", "F" };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "F",
                "A B",
                "B",
                "I"
            });

            Sort(expectedList);
            CollectionAssert.AreEqual(expectedList, processedactual);
        }

        [TestMethod]
        public void SpeechCarlSaganPaleBlueDot()
        {
            string[] refArray = {
                    "Look", "again", "at", "that", "dot",
                    "That's", "here", "That's", "home", "That's", "us",
                    "On", "it", "everyone", "you", "love",
                    "everyone", "you", "know", "everyone", "you",
                    "ever", "heard", "of", "every", "human",
                    "being", "who", "ever", "was", "lived",
                    "out", "their", "lives"
                };
            string[] evalArray = {
                    "Look", "again", "at", "that", "dot",
                    "That's", "here", "That's", "home", "That's", "is",
                    "An", "it", "everyone", "he", "love",
                    "everyone", "you", "know", "you",
                    "ever", "heard", "of", "every", "human",
                    "being", "who", "ever", "has", "lived",
                    "out", "their", "lives"
                };

            List<string> expectedList = new List<string>();
            List<string> processedactual = _subsequenceProcessor.GetListOfAllPossibleSubsequences(refArray, evalArray);

            expectedList.AddRange(new List<string>()
            {
                "Look again at that dot That's here That's home That's",
                "it everyone",
                //"everyone you",
                "love everyone you know",
                "you ever heard of every human being who ever",
                "lived out their lives",
                //"you"

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
