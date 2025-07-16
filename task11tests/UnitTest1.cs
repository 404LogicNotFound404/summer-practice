using Xunit;
using task11;

namespace task11tests
{
    public class UnitTest1
    {
        [Fact]
        public void CalculatorAdd_ReturnCorrectResult()
        {
            CalculatorLoader loader = new CalculatorLoader();
            var calculator = CalculatorLoader.Calculator();
            Assert.Equal(10, calculator.Add(5, 5));
        }

        [Fact]
        public void CalculatorMinus_ReturnCorrectResult()
        {
            CalculatorLoader loader = new CalculatorLoader();
            var calculator = CalculatorLoader.Calculator();
            Assert.Equal(0, calculator.Minus(5, 5));
        }

        [Fact]
        public void CalculatorMul_ReturnCorrectResult()
        {
            CalculatorLoader loader = new CalculatorLoader();
            var calculator = CalculatorLoader.Calculator();
            Assert.Equal(25, calculator.Mul(5, 5));
        }

        [Fact]
        public void CalculatorDiv_ReturnCorrectResult()
        {
            CalculatorLoader loader = new CalculatorLoader();
            var calculator = CalculatorLoader.Calculator();
            Assert.Equal(2, calculator.Div(10, 5));
        }
    }
}
