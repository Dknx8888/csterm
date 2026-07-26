namespace CodeCrafters.Shell;

class Program
{
    private static async Task Main()
    {
        HashSet<string> shellBuiltins = ["echo", "exit", "type", "pwd", "cd"];
        
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
                    TypeBuiltin.Execute(arguments, shellBuiltins);
                    break;
                
                case "pwd":
                    var currentPath = Directory.GetCurrentDirectory();
                    Console.WriteLine(currentPath);
                    break;
                
                case "cd":
                    if (arguments.Length > 1)
                    {
                        Console.WriteLine($"{input}: too many arguments");
                    }
                    else
                    {
                        try
                        {
                            Directory.SetCurrentDirectory(arguments[0]);
                        }
                        catch
                        {
                            Console.WriteLine($"cd: {arguments[0]}: No such file or directory");
                        }
                    }
                    break;

                default:
                    var executablePath = ExecutableResolver.Find(command);
                    if (executablePath is null)
                    {
                        Console.WriteLine($"{input}: command not found");
                        break;
                    }

                    await ExternalProgramRunner.ExecuteAsync(command, arguments);
                    break;
            }
        }
    }
}