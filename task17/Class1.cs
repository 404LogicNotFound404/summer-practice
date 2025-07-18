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
        public void ServerStart()
        {
            launched = true;
            softStopRequest = false;
            thread = new Thread(ServerRun);
            thread.Start();
        }

        public void ServerRun()
        {
            while (launched)
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
                if (softStopRequest == true && queue.Count == 0)
                {
                    launched = false;
                    thread!.Interrupt();
                }
            }
        }

        public void AddCommandToQueue(ICommand command)
        {
            if (softStopRequest == false && launched == true)
            {
                queue.Add(command);
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
}
