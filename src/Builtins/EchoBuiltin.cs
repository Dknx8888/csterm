namespace CodeCrafters.Shell.Builtins;

public static class EchoBuiltin
{
    public static void Execute(string[] arguments)
    {
        var echoContent = string.Join(" ", arguments);
        Console.WriteLine(echoContent);
    }
}