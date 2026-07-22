class Program
{
    static void Main()
    {
        Console.Write("$ ");
        var command = Console.ReadLine()?.Trim();
        
        Console.WriteLine($"{command}: command not found");
    }
}
