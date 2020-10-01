using System;
using System.Collections.Generic;
using BackendForTranscriptionChecker.Workers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranscriptionChecker.Test.Unit
{
    [TestClass]
    public class PatternGroupBuilderTest
    {
        private readonly PatternGroupBuilder _patternGroupBuilder = new PatternGroupBuilder();

        [TestMethod]
        public void AllCorrect()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D", "E" };

            string[] expected = { "A B C D E" };

            string[] generatedPattern = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void AllSameCorrect()
        {
            string[] refArray = { "A", "A", "A", "A", "A" };
            string[] evalArray = { "A", "A", "A", "A", "A" };

            string[] expected = { "A A A A A" };

            string[] generatedPattern = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        /* tO do LATER
        public void AllWrong()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "F", "F" };

            string[] expected = { "A B C D E" };

            string[] generatedPattern = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }
        */
        [TestMethod]
        public void TwoBreaksInThePattern()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "F", "D", "E" };

            string[] expected = { "A B", "D E" };

            string[] generatedPattern = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        
        [TestMethod]
        public void CheckthisBreaksInThePattern() //check this later
        {
            string[] refArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "H" };

            string[] expected = { "A B", "D E", "G H" };

            string[] generatedPattern = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }
        
        

        [TestMethod]
        public void ThreeBreaksInThePattern()
        {
            string[] refArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "X", "G", "H" };

            string[] expected = { "A B", "D E", "G H" };

            string[] generatedPattern = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void ThreeBreaksInThePatternFirstBreakHas2DroppedWords()
        {
            string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "H" };

            string[] expected = { "A B", "D E", "G H" };

            string[] generatedPattern = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void ThreeBreaksInThePatternFirstBreakHas2DroppedWordsandEvalArrayhas2AddedWordinSecondBreak()
        {
            string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "G", "G","G", "H" };

            string[] expected = { "A B", "D E", "G H" };

            string[] generatedPattern = _patternGroupBuilder.GroupSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }
    }
}
