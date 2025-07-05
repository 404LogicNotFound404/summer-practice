using Xunit;
using FileSystemCommands;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        var command = new DirectorySizeCommand(testDir);
        command.Execute(); // Проверяем, что не возникает исключений

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void DirectorySizeCommand_CheckCorrectSize()
    {
        var testDir1 = Path.Combine(Path.GetTempPath(), "TestDir1");
        Directory.CreateDirectory(testDir1);
        File.WriteAllText(Path.Combine(testDir1, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir1, "test2.txt"), "World");

        var output = new StringWriter();
        Console.SetOut(output);

        var command = new DirectorySizeCommand(testDir1);
        command.Execute();

        string expected = "10\n";
        Assert.Equal(expected, output.ToString());

        Directory.Delete(testDir1, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute(); // Должен найти 1 файл

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_CheckCorrectOutput()
    {
        var testDir4 = Path.Combine(Path.GetTempPath(), "TestDir4");
        Directory.CreateDirectory(testDir4);
        File.WriteAllText(Path.Combine(testDir4, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir4, "file2.log"), "Log");

        var output = new StringWriter();
        Console.SetOut(output);

        var command = new FindFilesCommand(testDir4, "*.txt");
        command.Execute();

        string expected = "file1.txt\n";
        Assert.Equal(expected, output.ToString());

        Directory.Delete(testDir4, true);
    }

    [Fact]
    public void CommandRunner_OutputVerification()
    { 
        var output = new StringWriter();
        Console.SetOut(output);

        string expected = "7\nfile2.log\n";

        CommandRunner.Main();
        Assert.Equal(expected, output.ToString());
    }

}
