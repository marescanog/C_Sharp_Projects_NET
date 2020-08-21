using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleCalc.Test.Unit
{
    [TestClass]
    public class InputConverterTest
    {
        private readonly InputConverter _iInputConverter = new InputConverter();

        [TestMethod]
        public void ConvertsValidStringInputIntoDouble()
        {
            string inputNumber = "5";
            double convertedNumber = _iInputConverter.ConvertInputToNumeric(inputNumber);
            Assert.AreEqual(5, convertedNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailsToConvertInvalidStringInputIntoDouble()
        {
            string inputNumber = "*";
            double convertedNumber = _iInputConverter.ConvertInputToNumeric(inputNumber);
        }
    }
}
