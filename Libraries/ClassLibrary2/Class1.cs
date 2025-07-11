using AttributeAndInterface;

namespace ClassLibrary2
{
    [PluginLoadAttribute()]
    public class Class4 : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Class4");
        }
    }

    [PluginLoadAttribute("Class4", "Class5", "Class6")]
    public class Class7: ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Class7");
        }
    }

    [PluginLoadAttribute("Class4")]
    public class Class5 : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Class5");
        }
    }

    [PluginLoadAttribute("Class4", "Class5")]
    public class Class6 : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Class6");
        }
    }
}
