using System;
using BackendForTranscriptionChecker.Workers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranscriptionChecker.Test.Unit
{
    [TestClass]
    public class TCWGLGeneratorTest
    {
        private readonly CorrectWordGroupListGenerator _correctWordGroupListGenerator = new CorrectWordGroupListGenerator();
        [TestMethod]
        public void AllCorrect()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D", "E" };

            string[] expected = { "A B C D E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

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

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void AllWrong()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "F", "F" };

            string[] expected = { };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void OneMistakePos1AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "B", "C", "D", "E" };

            string[] expected = { "B C D E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void OneMistakePos2AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "F", "C", "D", "E" };

            string[] expected = { "C D E", "A" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void OneMistakePos3AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "F", "D", "E" };

            string[] expected = { "A B", "D E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void OneMistakePos4AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "F", "E" };

            string[] expected = { "A B C", "E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void OneMistakePos5AllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "C", "D", "F" };

            string[] expected = { "A B C D" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void TwoMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "C", "D", "E" };

            string[] expected = { "C D E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void ThreeMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "D", "E" };

            string[] expected = { "D E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void FourMistakeAllDifferent()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "F", "F", "F", "F", "E" };

            string[] expected = { "E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void RepetitivePattern()
        {
            string[] refArray = { "A", "B", "A", "B", "A" };
            string[] evalArray = { "A", "B", "A", "F", "A" };

            string[] expected = { "A B A", "A" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void RepetitivePatternComplex()
        {
            string[] refArray = { "A", "B", "A", "B", "D", "A", "B", "F" };
            string[] evalArray = { "A", "B", "A", "F", "A", "B" };

            string[] expected = { "A B A", "A B" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void RepetitivePatternComplex2()
        {
            string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
            string[] evalArray = { "A", "B", "A", "F", "A", "B" };

            string[] expected = { "B A", "A B" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void RepetitivePatternComplex3()
        {
            string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
            string[] evalArray = { "B", "A", "F", "A", "B" };

            string[] expected = { "B A", "A B" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }

        }

        [TestMethod]
        public void Pattern1()
        {
            string[] refArray = { "A", "B", "C", "D", "E" };
            string[] evalArray = { "A", "B", "F", "D", "E" };

            string[] expected = { "A B", "D E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void Pattern2()
        {
            string[] refArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "H" };

            string[] expected = { "A B", "D E", "G H" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void Pattern3()
        {
            string[] refArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "X", "G", "H" };

            string[] expected = { "A B", "D E", "G H" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void Pattern4()
        {
            string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "H" };

            string[] expected = { "A B", "D E", "G H" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void Pattern5()
        {
            string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "G", "H" };

            string[] expected = { "A B", "D E", "G H" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void Pattern6()
        {
            string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "G", "G", "H" };

            string[] expected = { "G G H", "D E","A B" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void Pattern7()
        {
            string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "G", "G", "G", "H" };
            string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "G", "G", "G", "G", "H" };

            string[] expected = { "A B", "D E", "G G G G H" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void TwoSegments()
        {
            string[] refArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "C", "D", "E", "O", "G", "H", "I", "J", "K", "L" };

            string[] expected = { "G H I J K L", "A B C D E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }


        [TestMethod]
        public void TwoSegmentsRefLeadingRepSep()
        {
            string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "O", "G", "H", "I", "J", "K", "L" };

            string[] expected = { "G H I J K L","A B" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void TwoSegmentsRefTrailingingRepSep()
        {
            string[] refArray = { "A", "B", "C", "D", "E", "F", "F", "F", "F", "F", "K", "L" };
            string[] evalArray = { "A", "B", "C", "D", "E", "O", "K", "L" };

            string[] expected = { "A B C D E", "K L" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void TwoSegmentsEvalLeadingRepSep()
        {
            string[] refArray = { "A", "B", "E", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "E", "E", "E", "O", "G", "H", "I", "J", "K", "L" };

            string[] expected = { "G H I J K L", "A B E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void TwoSegmentsEvaltRAILINGRepSep()
        {
            string[] refArray = { "A", "B", "C", "D", "E", "F", "Q", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "C", "D", "E", "O", "G", "G", "G", "G", "K", "L" };

            string[] expected = { "A B C D E", "K L" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }

        [TestMethod]
        public void ThreeSegments()
        {
            string[] refArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
            string[] evalArray = { "A", "B", "C", "D", "E", "O", "G", "H", "I", "J", "K", "L" };

            string[] expected = { "G H I J K L", "A B C D E" };

            string[] generatedPattern = _correctWordGroupListGenerator.GetGroupOfSuccessiveCorrectWords(refArray, evalArray);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i] == generatedPattern[i]);
            }
        }
    }
}
