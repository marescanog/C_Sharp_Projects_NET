using System;
using Xunit;

namespace SimpleCalculator.Test.Unit
{
    public class CalculatorEngineTest
    {
        private readonly CalculatorEngine _calculatorEngine = new CalculatorEngine();
        [Fact]
        public void AddTwoNumbersAndReturnsValidResultForNonSymbolOperations()
        {
            int number1 = 1;
            int number2 = 2;
            double result = _calculatorEngine.Calculate("add", number1, number2);
            Assert.Equal(3, result);
        }
    }
}
