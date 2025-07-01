using FileSystemCommands;

class task08
{
    static void Main()
    {
        string path_1 = @"D:\универ\practise1\SummerPractise2025\"; 

        var dir_size_cmd = new DirectorySizeCommand(path_1);
        dir_size_cmd.Execute();

        Console.WriteLine($"Размер каталога {dir_size_cmd.DirectorySize} байт");
        

        string path_2 = @"D:\универ\practise1\SummerPractise2025\CommandRunner";
        string pattern = "*.cs";

        var find_files_cmd = new FindFilesCommand(path_2, pattern);
        find_files_cmd.Execute();

        if (find_files_cmd.FoundFiles.Length == 0)
        {
            Console.WriteLine("Файлы не найдены.");
        }
        else
        {
            Console.WriteLine("Найденные файлы:");
            foreach (var file in find_files_cmd.FoundFiles)
            {
                Console.WriteLine(file);
            }
        }
    }
}
