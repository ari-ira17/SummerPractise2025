using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
    private readonly string _path;

    public long DirectorySize = 0;

    public DirectorySizeCommand(string path)
    {
        _path = path;
    }
    
    public void Execute()
    {
        if (!Directory.Exists(_path))
        {
            DirectorySize = 0;
            return;
        }
        
        foreach (var file in Directory.GetFiles(_path, "*", SearchOption.AllDirectories))
        {
            DirectorySize += new FileInfo(file).Length;
        }
    }
}

public class FindFilesCommand : ICommand
{
    private readonly string _path;
    private readonly string _pattern;

    public string[] FoundFiles = Array.Empty<string>();

    public FindFilesCommand(string path, string pattern)
    {
        _path = path;
        _pattern = pattern;
    }
    
    public void Execute()
    {
        if (!Directory.Exists(_path))
        {
            FoundFiles = Array.Empty<string>();
            return;
        }
        
        FoundFiles = Directory.GetFiles(_path, _pattern, SearchOption.AllDirectories);
    }
}
