using task17;

namespace task18tests
{
    public class UnitTest1
    {
        public class testCommand : ICommand
        {
            public bool Done { get; set; } = false;
            public void Execute()
                => Done = true;
        }

        public class testLongCommand : ILongCommand
        {
            public bool complete { get; set; } = false;
            public int time { get; set; } = 3;
            public void Execute()
            {
                Thread.Sleep(100);
                if (time > 0)
                {
                    time--;
                }
                if (time == 0)
                {
                    complete = true;
                }
            }
        }
        [Fact]
        public void СheckingСorrectProcessingOfRegularAndLongCommands()
        {
            var server = new ServerThread();
            server.ServerStart();

            var command1 = new testCommand();
            var command2 = new testLongCommand();
            var command3 = new testCommand();
            var command4 = new testCommand();

            server.AddCommandToQueue(command1);
            server.AddCommandToQueue(command2);
            server.AddCommandToQueue(command3);
            server.AddCommandToQueue(command4);
            Thread.Sleep(120);

            Assert.True(command1.Done);
            Assert.False(command2.complete);

            Thread.Sleep(700);
            Assert.Equal(0, command2.time);
            Assert.True(command3.Done);
            Assert.True(command4.Done);
        }

        [Fact]
        public void СheckingСorrectProcessingLongCommands()
        {
            var server = new ServerThread();
            server.ServerStart();

            var command1 = new testLongCommand();
            var command2 = new testLongCommand();

            server.AddCommandToQueue(command1);
            server.AddCommandToQueue(command2);
            Thread.Sleep(130);

            Assert.Equal(2, command1.time);
            Assert.Equal(3 ,command2.time);

            Thread.Sleep(100);
            Assert.Equal(2, command1.time);
            Assert.Equal(2, command2.time);
            Thread.Sleep(100);
            Assert.Equal(1, command1.time);
            Assert.Equal(2, command2.time);
        }
    }
}