namespace CodeCrafters.Shell;

public static class ExecutableResolver
{
    private const UnixFileMode ExecutePermissions = UnixFileMode.UserExecute |
                                                    UnixFileMode.GroupExecute |
                                                    UnixFileMode.OtherExecute;

    public static string? Find(string command)
    {
        var pathDirectories = Environment
            .GetEnvironmentVariable("PATH")?
            .Split(
                Path.PathSeparator,
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries
            ) ?? [];
        
        foreach (var pathDir in pathDirectories)
        {
            var filePath = Path.Combine(pathDir, command);
            
            if (IsExecutable(filePath))
            {
                return filePath;
            }
        }

        return null;
    }

    private static bool IsExecutable(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }
        
        // Unsupported on Win
        var mode = File.GetUnixFileMode(filePath);
        return (mode & ExecutePermissions) != 0;
    }
}