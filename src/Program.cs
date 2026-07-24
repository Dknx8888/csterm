using CodeCrafters.Shell;

class Program
{
    private static async Task Main()
    {
        string[] shellBuiltins = ["echo", "exit", "type"];
        
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
