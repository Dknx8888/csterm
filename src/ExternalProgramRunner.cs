using System.ComponentModel;
using System.Diagnostics;

namespace CodeCrafters.Shell;

public static class ExternalProgramRunner
{
    public static async Task<int> ExecuteAsync(string execPath, IEnumerable<string> arguments)
    {
        try
        {
            using var process = Process.Start(execPath, arguments);
            await process.WaitForExitAsync();
            return process.ExitCode;
        }
        catch (Win32Exception exception)
        {
            Console.Error.WriteLine($"{Path.GetFileName(execPath)}: {exception.Message}");
            return 1;
        }
    }
}