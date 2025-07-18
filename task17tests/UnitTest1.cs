using task17;
namespace task17tests
{
    public class UnitTest1
    {
        public class testCommand : ICommand
        {
            public bool Done { get; set; } = false;
            public void Execute()
                => Done = true;
        }
        [Fact]
        public void HardStop_CheckHardStopThread()
        {
            var server = new ServerThread();
            server.ServerStart();

            var command1 = new testCommand();
            var command2 = new testCommand();
            HardStop hardStop = new HardStop(server);

            server.AddCommandToQueue(command1);
            server.AddCommandToQueue(hardStop);
            server.AddCommandToQueue(command2);
            Thread.Sleep(100);

            Assert.False(server.launched);
            Assert.True(command1.Done);
            Assert.False(command2.Done);
        }

        [Fact]
        public void SoftStop_CheckSoftStopThread()
        {
            var server = new ServerThread();
            server.ServerStart();

            var command1 = new testCommand();
            var command2 = new testCommand();
            SoftStop softStop = new SoftStop(server);

            server.AddCommandToQueue(command1);
            server.AddCommandToQueue(softStop);
            server.AddCommandToQueue(command2);
            Thread.Sleep(100);

            Assert.False(server.launched);
            Assert.True(command1.Done);
            Assert.True(command2.Done);
        }

        [Fact]
        public void SoftStop_CheckingForTheInabilityToAdd()
        {
            var server = new ServerThread();
            server.ServerStart();

            var command1 = new testCommand();
            var command2 = new testCommand();
            var command3 = new testCommand();
            SoftStop softStop = new SoftStop(server);

            server.AddCommandToQueue(command1);
            server.AddCommandToQueue(softStop);
            server.AddCommandToQueue(command2);
            Thread.Sleep(100);

            Assert.Throws<InvalidOperationException>(() => server.AddCommandToQueue(command3));

            Assert.False(server.launched);
            Assert.True(command1.Done);
            Assert.True(command2.Done);
            Assert.False(command3.Done);
        }

        [Fact]
        public void SoftStopAndHardStop_CheckCorrectThreadStop()
        {
            var server1 = new ServerThread();
            var server2 = new ServerThread();
            server1.ServerStart();
            server2.ServerStart();

            SoftStop softStop1 = new SoftStop(server1);
            HardStop hardStop1 = new HardStop(server1);
            server2.AddCommandToQueue(softStop1);
            server2.AddCommandToQueue(hardStop1);
            Assert.Throws<InvalidOperationException>(() => softStop1.Execute());
            Assert.Throws<InvalidOperationException>(() => hardStop1.Execute());
            Thread.Sleep(100);

            Assert.True(server2.launched);
        }
    }
}
