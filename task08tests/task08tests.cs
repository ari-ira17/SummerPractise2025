using FileSystemCommands;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateCorrectSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);

        var file1Path = Path.Combine(testDir, "test1.txt");
        var file2Path = Path.Combine(testDir, "test2.txt");
        File.WriteAllText(file1Path, "Hello"); 
        File.WriteAllText(file2Path, "World!"); 

        var command = new DirectorySizeCommand(testDir);
        command.Execute();

        long expectedSize = new FileInfo(file1Path).Length + new FileInfo(file2Path).Length;
        Assert.Equal(expectedSize, command.DirectorySize);

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);

        var txtFile = Path.Combine(testDir, "file1.txt");
        var logFile = Path.Combine(testDir, "file2.log");
        File.WriteAllText(txtFile, "Text");
        File.WriteAllText(logFile, "Log");

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute();

        Assert.Single(command.FoundFiles);
        Assert.Contains(txtFile, command.FoundFiles);

        Directory.Delete(testDir, true);   
    }

    [Fact]
    public void DirectorySizeCommand_ShouldReturnZeroIfDirectoryNotExist()
    {
        var nonExistentDir = Path.Combine(Path.GetTempPath(), "NonExistentDir");

        var command = new DirectorySizeCommand(nonExistentDir);
        command.Execute();

        Assert.Equal(0, command.DirectorySize);
    }

    [Fact]
    public void FindFilesCommand_ShouldReturnEmptyIfNoMatches()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);

        File.WriteAllText(Path.Combine(testDir, "file1.log"), "Log");

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute();

        Assert.Empty(command.FoundFiles);

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldReturnEmptyIfDirectoryNotExist()
    {
        var nonExistentDir = Path.Combine(Path.GetTempPath(), "NonExistentDir");

        var command = new FindFilesCommand(nonExistentDir, "*.txt");
        command.Execute();

        Assert.Empty(command.FoundFiles);
    }
}