using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
namespace task11
{
    public interface ICalculator
    {
        int Add(int a, int b);
        int Minus(int a, int b);
        int Mul(int a, int b);
        int Div(int a, int b);

    }
    public class CalculatorLoader
    {
        public static ICalculator Calculator()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                public class Calculator : task11.ICalculator
                {
                    public int Add(int a, int b) => a + b;
                    public int Minus(int a, int b) => a - b;
                    public int Mul(int a, int b) => a * b;
                    public int Div(int a, int b) => a / b;
                 }"
             );

            var compilation = CSharpCompilation.Create("compilation")
                                               .AddSyntaxTrees(syntaxTree)
                                               .AddReferences(MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location))
                                               .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                                               .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


            var memory = new MemoryStream();
            compilation.Emit(memory);

            var assembly = Assembly.Load(memory.ToArray());

            var typeCalculator = assembly.GetTypes()
                                         .Where(s => s.GetInterfaces().Contains(typeof(ICalculator)))
                                         .First();

            ICalculator calculator = (ICalculator)Activator.CreateInstance(typeCalculator)!;
            return calculator;
        }
    }
}
