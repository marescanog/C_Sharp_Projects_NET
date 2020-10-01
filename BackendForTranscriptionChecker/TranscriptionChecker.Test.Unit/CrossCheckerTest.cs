using System;
using BackendForTranscriptionChecker;
using BackendForTranscriptionChecker.Workers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranscriptionChecker.Test.Unit
{
    [TestClass]
    public class CrossCheckerTest
    {
        CrossChecker _crossChecker = new CrossChecker();

        [TestMethod]
        public void AllCorrect()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D", "E" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B C D E");

        }

        [TestMethod]
        public void AllSameCorrect()
        {
            string[] refArray = { "A", "A", "A", "A", "A" };
            string[] evalArray = { "A", "A", "A", "A", "A" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A A A A A");

        }

        [TestMethod]
        public void AllWrong()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "F", "F" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(check.Length == 0);

        }

        [TestMethod]
        public void OneMistakePos1AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "B", "C", "D", "E" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "B C D E");

        }

        [TestMethod]
        public void OneMistakePos2AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "F", "C", "D", "E" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A C D E");

        }

        [TestMethod]
        public void OneMistakePos3AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "F", "D", "E" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B D E");

        }

        [TestMethod]
        public void OneMistakePos4AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "F", "E" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B C E");

        }

        [TestMethod]
        public void OneMistakePos5AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D", "F" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B C D");

        }

        [TestMethod]
        public void TwoMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "C", "D", "E" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "C D E");

        }

        [TestMethod]
        public void ThreeMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "D", "E" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "D E");

        }

        [TestMethod]
        public void FourMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "F", "E" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "E");

        }

        [TestMethod]
        public void RepetitivePattern()
        {
            string[] refArray = { "A", "B", "A", "B", "A" };
            string[] evalArray = { "A", "B", "A", "F", "A" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B A A");

        }

        [TestMethod]
        public void RepetitivePatternComplex()
        {
            string[] refArray = { "A", "B", "A", "B", "D", "A", "B", "F" };
            string[] evalArray = { "A", "B", "A", "F", "A", "B" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B A A B");

        }

        [TestMethod]
        public void RepetitivePatternComplex2()
        {
            string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
            string[] evalArray = { "A", "B", "A", "F", "A", "B" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "B A A B");

        }

        [TestMethod]
        public void RepetitivePatternComplex3()
        {
            string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
            string[] evalArray = { "B", "A", "F", "A", "B" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "B A A B");

        }

        [TestMethod]
        public void TwoSegmentsRefLeadingRepSep()
        {
            string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "O", "G", "H", "I", "J", "K", "L" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B G H I J K L");
        }

        [TestMethod]
        public void TwoSegmentsRefLeadingTruRepSep()
        {
            string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "O", "G", "H", "I", "J", "K", "L", "F" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B G H I J K L");
        }

        [TestMethod]
        public void WhatIfTheSecondArrayKeepsRepeating() //highlight of my life
        {
            string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "B", "B", "B", "I", "I", "I", "I", "F" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B I");
        }

        [TestMethod]
        public void TwoSegmentsEvalLeadingTruRepSep()
        {
            string[] refArray = { "A", "B", "E", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "E", "E", "E", "O", "G", "H", "I", "J", "K", "L" };

            string[] check = _crossChecker.GetCorrectWords(refArray, evalArray);
            string arrayPattern = String.Join(Constants.space, check);

            Assert.IsTrue(arrayPattern == "A B E H I J K L");
        }
    }
}
