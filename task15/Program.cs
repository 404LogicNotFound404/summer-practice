using System.Diagnostics;
using ScottPlot;


class Program
{
    static void Main()
    {
        double start = -1000;
        double end = 1000;
        var SIN = (double x) => Math.Sin(x);

        double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
        double minstep = double.MaxValue;
        steps.ToList().ForEach(step => 
        {
            if (minstep == double.MaxValue)
            {
                double result = DefiniteIntegral.Solve(start, end, SIN, step, 4);
                if (Math.Abs(result) < 1e-4)
                {
                    minstep = step;
                }
            }
        });

        int[] threadsnumbers = { 1, 2, 3, 4, 5, 6};
        Dictionary<int, double> FlowDependentSpeed = new Dictionary<int, double>();
        threadsnumbers.ToList().ForEach(threadnumber =>
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 100; i++) 
            {
                DefiniteIntegral.Solve(start, end, SIN, minstep, threadnumber);
            }
            stopwatch.Stop();
            FlowDependentSpeed[threadnumber] = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000 / 100;
        });

        var plt = new Plot();
        plt.Add.Scatter(FlowDependentSpeed.Keys.ToArray(), FlowDependentSpeed.Values.ToArray());
        plt.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericFixedInterval(1);
        plt.XLabel("Количество потоков");
        plt.YLabel("Время в миллисекундах");

        string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))))!;
        plt.SavePng(Path.Combine(path,"info.png"), 600, 400);



        double timeIntegral = 0;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < 100; i++) 
        {
            DefiniteIntegral.Integral(start, end, SIN, minstep);
        }
        stopwatch.Stop();
        timeIntegral = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000 / 100;

        var percent = FlowDependentSpeed.Values.Min() / timeIntegral * 100;
        if (percent > 15) 
        {
            string ex = Environment.NewLine;
            string file = $"Размер шага: {minstep}{ex}" +
                $"Оптимальное кол-во потоков: {FlowDependentSpeed.Where(s => s.Value == FlowDependentSpeed.Values.Min())
                                                                 .Select(s => s.Key.ToString()).First()}{ex}" +
                $"Скорость оптимальной многопоточной версии в сравнении с однопоточной: {Math.Round(percent, 1)}";
            File.WriteAllText(Path.Combine(path, "file.txt"), file);
        }
        else
        {
            string file = "Необходима оптимизация";
            File.WriteAllText(Path.Combine(path, "file.txt"), file);
        }
    }
}
