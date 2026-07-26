namespace CodeCrafters.Shell;

public static class TypeBuiltin
{
    public static void Execute(string[] arguments, HashSet<string> shellBuiltins)
    {
        foreach (var arg in arguments)
        {
            if (shellBuiltins.Contains(arg))
            {
                Console.WriteLine($"{arg} is a shell builtin");
                continue;
            }

            var executablePath = ExecutableResolver.Find(arg);
                        
            Console.WriteLine(
                executablePath is not null
                    ? $"{arg} is {executablePath}"
                    : $"{arg}: not found"
            );
        }
    }
}