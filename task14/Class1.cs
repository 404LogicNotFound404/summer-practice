using System.Diagnostics.CodeAnalysis;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double result = 0;
        double stepIntegral = (b - a)/ threadsnumber;
        Barrier barrier = new Barrier(threadsnumber + 1);

        for (int i = 0; i < threadsnumber; i++)
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

            Thread integral = new Thread(() => 
            {
                double newIntegar = Integral(start, end, function, step);
                double initial;
                double newresult;
                do
                {
                    initial = result;
                    newresult = initial + newIntegar;
                }
                while (initial != Interlocked.CompareExchange(ref result, newresult, initial));
                barrier.SignalAndWait();
            });
            
            integral.Start();
        }
        barrier.SignalAndWait();
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
