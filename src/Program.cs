class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("$ ");
            var command = Console.ReadLine()?.Trim();

            switch (command)
            {
                case "exit":
                    return;
                default:
                    Console.WriteLine($"{command}: command not found");
                    break;
            }
        }
    }
}
