class Program
{
    static void Main()
    {
        string[] shellBuiltins = ["echo", "exit", "type"];
        var pathDirectories = Environment
            .GetEnvironmentVariable("PATH")?
            .Split(
                Path.PathSeparator,
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries
            ) ?? [];
        
        while (true)
        {
            Console.Write("$ ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }
            
            // Handles many whitespaces
            var inputArr = input.Split(' ', 
                StringSplitOptions.RemoveEmptyEntries | 
                StringSplitOptions.TrimEntries);

            var command = inputArr[0];
            var arguments = inputArr[1..];

            switch (command)
            {
                case "exit":
                    // Will implement the generic solution later
                    if (arguments.Length == 0) return;
                    Console.WriteLine($"{input}: too many arguments");
                    break;
                
                case "echo":
                    var echoContent = string.Join(" ", arguments);
                    Console.WriteLine(echoContent);
                    break;
                
                case "type":
                    foreach (var arg in arguments)
                    {
                        if (shellBuiltins.Contains(arg))
                        {
                            Console.WriteLine($"{arg} is a shell builtin");
                            continue;
                        }

                        string? executablePath = null;

                        foreach (var pathDir in pathDirectories)
                        {
                            var filePath = Path.Combine(pathDir, arg);
                            if (File.Exists(filePath))
                            {
                                executablePath = filePath;
                                break;
                            }
                        }
                        
                        Console.WriteLine(
                            executablePath is not null
                                ? $"{arg} is {executablePath}"
                                : $"{arg}: not found"
                            );
                    }
                    break;
                
                default:
                    Console.WriteLine($"{input}: command not found");
                    break;
            }
        }
    }
}
