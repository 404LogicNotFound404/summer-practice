using System.Diagnostics;
using task17;
using ScottPlot;
using ScottPlot.TickGenerators.TimeUnits;

class Program
{
    public static Stopwatch stopwatch = new Stopwatch();
    static void Main()
    {

        var server = new ServerThread();
        server.ServerStart();

        var command1 = new testCommand();
        var command2 = new testLongCommand();
        var command3 = new testLongCommand();
        var command4 = new testCommand();
        var command5 = new testCommand();


        stopwatch.Start();


        server.AddCommandToQueue(command1);
        server.AddCommandToQueue(command2);
        server.AddCommandToQueue(command3);
        server.AddCommandToQueue(command4);
        server.AddCommandToQueue(command5);

        Thread.Sleep(1200);

        Dictionary<int, double> OxOy = new Dictionary<int, double>();
        OxOy[1] = command1.timecomplete / Stopwatch.Frequency;
        OxOy[2] = command2.timecomplete / Stopwatch.Frequency;
        OxOy[3] = command3.timecomplete / Stopwatch.Frequency;
        OxOy[4] = command4.timecomplete / Stopwatch.Frequency;
        OxOy[5] = command5.timecomplete / Stopwatch.Frequency;

        var plt = new Plot();
        plt.Add.Scatter(OxOy.Keys.ToArray(), OxOy.Values.ToArray());
        plt.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericFixedInterval(1);
        plt.XLabel("Номер команды");
        plt.YLabel("Время выполнения в секундах");

        string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))))!;
        plt.SavePng(Path.Combine(path, "info.png"), 600, 400);

        string ex = Environment.NewLine;
        string file = $"Время выполнения 1 команды: {command1.stopwatch.ElapsedMilliseconds} мс{ex}" +
            $"Время выполнения 2 команды: {command2.timecommand} мс{ex}" +
            $"Время выполнения 3 команды: {command3.timecommand} мс{ex}" +
            $"Время выполнения 4 команды: {command4.stopwatch.ElapsedMilliseconds} мс{ex}" +
            $"Время выполнения 5 команды: {command5.stopwatch.ElapsedMilliseconds} мс{ex}";
        File.WriteAllText(Path.Combine(path, "file.txt"), file);
    }
}
public class testCommand : ICommand
{
    public bool Done { get; set; } = false;
    public double timecomplete { get; set; }
    public Stopwatch stopwatch = new Stopwatch();
    public void Execute()
    {
        stopwatch.Start();
        Thread.Sleep(150);
        Done = true;
        timecomplete = Program.stopwatch.ElapsedTicks;
        stopwatch.Stop();
    }
}

public class testLongCommand : ILongCommand
{
    public bool complete { get; set; } = false;
    public double timecomplete { get; set; }
    Stopwatch stopwatch1 = new Stopwatch();
    public int timecommand = 0;
    public int time { get; set; } = 3;
    public void Execute()
    {
        stopwatch1.Restart();
        stopwatch1.Start();
        Thread.Sleep(100);
        stopwatch1.Stop();
        timecommand += (int)stopwatch1.Elapsed.TotalMilliseconds;

        if (time > 0)
        {
            time--;
        }
        if (time == 0)
        {
            complete = true;
            timecomplete = Program.stopwatch.ElapsedTicks;
        }
    }
}
