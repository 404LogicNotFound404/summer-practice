using AttributeAndInterface;

namespace ClassLibrary1
{
    [PluginLoadAttribute()]
    public class Class1 : ICommand
    {
        public void Execute() 
        {
            Console.WriteLine("Class1");
        }
    }

    [PluginLoadAttribute("Class1")]
    public class Class2 : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Class2");
        }
    }

    [PluginLoadAttribute("Class1", "Class2")]
    public class Class3 : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Class3");
        }
    }
}
