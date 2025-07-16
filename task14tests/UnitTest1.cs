namespace task14tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestIntegralX1_ReturnCorrectValue()
        {
            var X = (double x) => x;
            Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2), 1e-4);
        }

        [Fact]
        public void TestIntegralSIN_ReturnCorrectValue()
        {
            var SIN = (double x) => Math.Sin(x);
            Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8), 1e-4);
        }

        [Fact]
        public void TestIntegralX2_ReturnCorrectValue()
        {
            var X = (double x) => x;
            Assert.Equal(12.5, DefiniteIntegral.Solve(0, 5, X, 1e-6, 8), 1e-5);
        }
    }
}
