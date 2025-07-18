using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double stepIntegral = (b - a) / threadsnumber;

        return Enumerable.Range(0, threadsnumber)
                         .AsParallel()
                         .WithDegreeOfParallelism(threadsnumber)
                         .Select(i =>
                         {
                             double start = a + i * stepIntegral;
                             double end = (i == threadsnumber - 1) ? b : start + stepIntegral;
                             return Integral(start, end, function, step);
                         })
                         .Sum();
    }

    public static double Integral(double a, double b, Func<double, double> function, double step)
    {
        int NumberSplitsIntegral = (int)((b - a) / step) + 1;
        double result = 0;

        for (int i = 0; i < NumberSplitsIntegral; i++)
        {
            double start = a + i * step;
            double end = 0;

            if (start + step >= b)
            {
                end = b;
            }
            else
            {
                end = start + step;
            }

            result += (function(start) + function(end)) * (end - start) / 2;
        }
        return result;
    }
}
