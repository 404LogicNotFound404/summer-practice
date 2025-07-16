namespace FileSystemCommands
{
    public class DirectorySizeCommand : ICommand
    {
        public string nameDirectory;
        public DirectorySizeCommand(string nameDirectory)
        {
            this.nameDirectory = nameDirectory;
        }
        public void Execute()
            => Console.WriteLine(Directory.GetFiles(nameDirectory,"*",SearchOption.AllDirectories)
                                          .Select(s => new FileInfo(s).Length)
                                          .Sum());
    }

    public class FindFilesCommand : ICommand
    {
        public string nameDirectory;
        public string mask;
        public FindFilesCommand(string nameDirectory, string mask)
        {
            this.nameDirectory = nameDirectory;
            this.mask = mask;
        }
        public void Execute()
            => Directory.GetFiles(nameDirectory, mask, SearchOption.AllDirectories)
                        .Select(Path.GetFileName)
                        .ToList()
                        .ForEach(s => Console.WriteLine(s));
    }
}
