using CodeCrafters.Shell.Builtins;
using CodeCrafters.Shell.Helpers;

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

            string[] inputArr;

            try
            {
                inputArr = ParseInput.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

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
                    EchoBuiltin.Execute(arguments);
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
                        ChangeDirectoryBuiltin.Execute(arguments[0]);
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