using System.Collections.Concurrent;
namespace task17
{
    public class ServerThread
    {
        public BlockingCollection<ICommand> queue = new BlockingCollection<ICommand>();
        public bool launched = false;
        public bool softStopRequest;
        public Thread? thread;
        ExceptionHandler handler = new ExceptionHandler();
        public Scheduler scheduler = new Scheduler();
        public void ServerStart()
        {
            launched = true;
            softStopRequest = false;
            thread = new Thread(ServerRun);
            thread.Start();
        }

        public void ServerRun()
        {
            bool alternation = false;
            while (launched)
            {
                if (softStopRequest == true && queue.Count == 0 && scheduler.commandsInScheduler.Count == 0)
                {
                    launched = false;
                    thread?.Interrupt();
                    break;
                }
                if (queue.Count > 0 && (scheduler.commandsInScheduler.Count == 0 || alternation))
                {
                    var command = queue.Take();
                    try
                    {
                        command.Execute();
                    }
                    catch (Exception ex)
                    {
                        handler.Handle(command, ex);
                    }
                    alternation = false;
                }
                else if (scheduler.commandsInScheduler.Count > 0)
                {
                    var command = (ILongCommand)scheduler.Select();
                    command.Execute();

                    if (!command.complete)
                    {
                        scheduler.Add(command);
                    }
                    alternation = true;
                }
            }
        }
        

        public void AddCommandToQueue(ICommand command)
        {
            if (softStopRequest == false && launched == true && !(command is ILongCommand longCommand))
            {
                queue.Add(command);
                return;
            }
            if (softStopRequest == false && launched == true && command is ILongCommand longCommand1)
            {
                scheduler.Add(command);
                return;
            }
            throw new InvalidOperationException("Поток завершен или выполняется завершение потока");
        }

        public bool IsInWorkThread()
        {
            return Thread.CurrentThread == thread;
        }
    } 
    public class ExceptionHandler
    {
            public void Handle(ICommand command, Exception ex)
            {
                Console.WriteLine($"{command} {ex}");
            }
    }

    public class HardStop : ICommand 
    {
        readonly ServerThread serverThread;
        public HardStop(ServerThread serverThread)
        {
            this.serverThread = serverThread;
        }
        public void Execute()
        {
            if (!serverThread.IsInWorkThread())
            {
                throw new InvalidOperationException("Error");
            }
            serverThread.launched = false;
            serverThread.thread!.Interrupt();
        }
    }

    public class SoftStop : ICommand 
    {
        readonly ServerThread serverThread;
        public SoftStop(ServerThread serverThread)
        {
            this.serverThread = serverThread;
        }
        public void Execute()
        {
            if (!serverThread.IsInWorkThread())
            {
                throw new InvalidOperationException("Error");
            }
            serverThread.softStopRequest = true;
        }
    }

    public class Scheduler : IScheduler
    {
        public ConcurrentQueue<ICommand> commandsInScheduler = new ConcurrentQueue<ICommand>();
        public bool HasCommand()
            => (commandsInScheduler.Count > 0);

        public ICommand Select()
        {
            if (commandsInScheduler.TryDequeue(out ICommand? command))
            {
                return command;
            }
            else
            {
                throw new InvalidOperationException("Error");
            }
        }

        public void Add(ICommand command)
            => commandsInScheduler.Enqueue(command);
    }
}
