namespace AttributeAndInterface
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginLoadAttribute : Attribute
    {
        public string[] Dependence { get; }
        public PluginLoadAttribute()
        {
            Dependence = Array.Empty<string>();
        }

        public PluginLoadAttribute(params string[] Dependence)
        {
            this.Dependence = Dependence;
        }

    }

    public interface ICommand
    {
        void Execute();
    }
}
