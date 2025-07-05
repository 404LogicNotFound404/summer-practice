using System.Data;
using System.Diagnostics.Tracing;
using System.Reflection;
using FileSystemCommands;
public class CommandRunner
{
    public static void Main()
    {
        var assembly = Assembly.LoadFrom("FileSystemCommands.dll");

        var testDir2 = Path.Combine(Path.GetTempPath(), "TestDir2");
        Directory.CreateDirectory(testDir2);
        File.WriteAllText(Path.Combine(testDir2, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir2, "file2.log"), "Log");

        var commands = assembly.GetTypes()
                               .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface)
                               .Where(s => s != null)
                               .ToList();

        foreach (var command in commands) 
        {
            if (command == typeof(DirectorySizeCommand))
            {
                var directorySizeCommand = (ICommand)Activator.CreateInstance(command, testDir2)!;
                if (directorySizeCommand != null)
                {
                    directorySizeCommand.Execute();
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }

            if (command == typeof(FindFilesCommand))
            {
                var findFilesCommand = (ICommand)Activator.CreateInstance(command, testDir2, "*.Log")!;
                if (findFilesCommand != null)
                {
                    findFilesCommand.Execute();
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
        }
        Directory.Delete(testDir2, true);
    }
}
