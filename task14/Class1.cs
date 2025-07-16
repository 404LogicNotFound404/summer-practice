using System.Diagnostics.CodeAnalysis;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double result = 0;
        double stepIntegral = (b - a) / threadsnumber;
        Parallel.For(0, threadsnumber, new ParallelOptions { MaxDegreeOfParallelism = threadsnumber }, i =>
        {
            double start = a + i * stepIntegral;
            double end = 0;
            if (start + stepIntegral >= b)
            {
                end = b;
            }
            else
            {
                end = start + stepIntegral;
            }

            double newIntegar = Integral(start, end, function, step);
            double initial;
            double newresult;
            do
            {
                initial = result;
                newresult = initial + newIntegar;
            }
            while (initial != Interlocked.CompareExchange(ref result, newresult, initial));

        });
        return result;
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
