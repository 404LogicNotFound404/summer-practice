public interface ICommand
{
    void Execute();
}

public interface ILongCommand : ICommand
{
    int time { get; set; }
    bool complete { get; set; }
}
