namespace task10tests
{
    public class UnitTest1
    {
        [Fact]
        public void Task10Main_OutputVerification()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            string ex = Environment.NewLine;
            string expected = $"Class1{ex}" +
                $"Class4{ex}" +
                $"Class2{ex}" +
                $"Class5{ex}" +
                $"Class3{ex}" +
                $"Class6{ex}" +
                $"Class7{ex}";

            string solution = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))))!;
            var pluginsDirectory = Path.Combine(solution, "Libraries");

            Program.Main(new[] { pluginsDirectory });
            Assert.Equal(expected, output.ToString());
        }
    }
}
