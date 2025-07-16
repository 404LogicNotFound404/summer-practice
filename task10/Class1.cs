using System.Reflection;
using AttributeAndInterface;

public class PluginSearch
{
    public static void GetExecute(string args)
    {
        var dllPaths = Directory.GetFiles(args, "*.dll", SearchOption.AllDirectories)
                                .Where(s => s.Contains(Path.Combine("bin", "Debug")))
                                .Where(s => !s.Contains("AttributeAndInterface.dll"))
                                .ToList();

        Assembly[] AllAssembly = new Assembly[dllPaths.Count];
        if (dllPaths.Count > 0)
        {
            int count = 0;
            foreach (var dllPath in dllPaths)
            {
                Assembly assembly = Assembly.LoadFrom(dllPath);
                AllAssembly[count] = assembly;
                count++;
            }
        }

        var allPlugins = new List<PluginInfo>();
        foreach (var Assembly in AllAssembly)
        {
            allPlugins.AddRange(Assembly.GetTypes()
                                        .Where(s => s.GetCustomAttribute<PluginLoadAttribute>() != null)
                                        .Select(s => new PluginInfo { PluginName = s.Name, Dependencies = s.GetCustomAttribute<PluginLoadAttribute>()!.Dependence, type = s }));
        }

        Dictionary<string, List<string>> DependenceGraph = new();
        foreach (var Plugin in allPlugins)
        {
            DependenceGraph[Plugin.PluginName] = Plugin.Dependencies.ToList();
        }

        List<string> sortedPlugins = new();

        var noDependence = DependenceGraph.Where(s => s.Value.Count == 0).Select(s => s.Key).ToList();
        noDependence.ForEach(s => DependenceGraph.Remove(s));
        sortedPlugins.AddRange(noDependence);

        while (DependenceGraph.Count > 0)
        {
            var visited = new List<string>();
            foreach (var Plugin in DependenceGraph)
            {
                if (Plugin.Value.Count > 0)
                {
                    if (Plugin.Value.All(s => sortedPlugins.Contains(s) && !visited.Contains(s)))
                    {
                        sortedPlugins.Add(Plugin.Key);
                        visited.Add(Plugin.Key);
                    }
                }
            }
            visited.ForEach(s => DependenceGraph.Remove(s));
        }

        foreach (var PluginName in sortedPlugins)
        {
            var Plugin = allPlugins.Where(s => s.PluginName == PluginName).First();
            var PluginInstance = (ICommand)Activator.CreateInstance(Plugin.type)!;
            PluginInstance.Execute();
        }
    }
}


public class PluginInfo
{
    public string PluginName { get; set; } = string.Empty;
    public string[] Dependencies { get; set; } = Array.Empty<string>();
    public Type type { get; set; } = null!;
}
