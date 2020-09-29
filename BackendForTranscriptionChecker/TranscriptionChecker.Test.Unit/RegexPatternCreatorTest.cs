using System;
using System.Collections.Generic;
using System.Linq;
using BackendForTranscriptionChecker.Workers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranscriptionChecker.Test.Unit
{
    [TestClass]
    public class RegexPatternCreatorTest
    {
        private readonly RegExPatternCreator _regExPatternCreator = new RegExPatternCreator();

        [TestMethod]
        public void AllCorrect()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D", "E" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "A B C D E");

        }

        [TestMethod]
        public void AllSameCorrect()
        {
            string[] refArray = { "A", "A", "A", "A", "A" };
            string[] evalArray = { "A", "A", "A", "A", "A" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "A A A A A");

        }

        [TestMethod]
        public void AllWrong()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "F", "F" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "(.*) (.*) (.*) (.*) (.*)");

        }

        [TestMethod]
        public void OneMistakePos1AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "B", "C", "D", "E" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "(.*) B C D E");

        }

        [TestMethod]
        public void OneMistakePos2AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "F", "C", "D", "E" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "A (.*) C D E");

        }

        [TestMethod]
        public void OneMistakePos3AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "F", "D", "E" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "A B (.*) D E");

        }

        [TestMethod]
        public void OneMistakePos4AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "F", "E" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "A B C (.*) E");

        }

        [TestMethod]
        public void OneMistakePos5AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D", "F" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "A B C D (.*)");

        }

        [TestMethod]
        public void TwoMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "C", "D", "E" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "(.*) (.*) C D E");

        }

        [TestMethod]
        public void ThreeMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "D", "E" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "(.*) (.*) (.*) D E");

        }

        [TestMethod]
        public void FourMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "F", "E" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "(.*) (.*) (.*) (.*) E");

        }

        [TestMethod]
        public void RepetitivePattern()
        {
            string[] refArray = { "A", "B", "A", "B", "A" };
            string[] evalArray = { "A", "B", "A", "F", "A" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "A B A (.*) A");

        }

        [TestMethod]
        public void RepetitivePatternComplex()
        {
            string[] refArray = { "A", "B", "A", "B", "D", "A", "B", "F" };
            string[] evalArray = { "A", "B", "A", "F", "A", "B" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "A B A (.*) (.*) A B (.*)");

        }

        [TestMethod]
        public void RepetitivePatternComplex2()
        {
            string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
            string[] evalArray = { "A", "B", "A", "F", "A", "B" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "(.*) B A (.*) A B (.*) (.*)");

        }

        [TestMethod]
        public void RepetitivePatternComplex3()
        {
            string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
            string[] evalArray = { "B", "A", "F", "A", "B" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "(.*) B A (.*) (.*) A B (.*)");

        }

        /*
        [TestMethod]
        public void RepetitivePattern()
        {
            string[] refArray = { "A", "B", "A", "B", "A" };
            string[] evalArray = { "A", "B", "A", "B", "A" };

            string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

            Assert.IsTrue(regexPattern == "(.*) (.*) (.*) (.*) E");

        }
        */
    }
}
