namespace CodeCrafters.Shell.Builtins;

public static class ChangeDirectoryBuiltin
{
    public static void Execute(string dirInput)
    {
        switch (dirInput)
        {
            case "~":
            {
                var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                SetDir(homeDir);
                break;
            }
            case ".":
            case "./":
                break;
            
            default:
            {
                if (dirInput[0] == '/')
                {
                    SetDir(dirInput);
                }
                else
                {
                    ChainingParentDir(dirInput);
                }

                break;
            }
        }
    }

    private static void SetDir(string dirInput)
    {
        try
        {
            Directory.SetCurrentDirectory(dirInput);
        }
        catch
        {
            Console.WriteLine($"cd: {dirInput}: No such file or directory");
        }
    }

    private static void ChainingParentDir(string dir)
    {
        if (dir is ".." or "../")
        {
            SetParentDir();
            return;
        }
        
        if (dir.StartsWith("../", StringComparison.Ordinal))
        {
            SetParentDir();
            var trimmed = dir[3..];
            ChainingParentDir(trimmed);
            return;
        }

        SetDir(dir);
    }

    private static void SetParentDir()
    {
        var currentDir = Directory.GetCurrentDirectory();
        if (currentDir == "/") return;

        var lastSlashIndex = currentDir.LastIndexOf('/');
        var parentDir = lastSlashIndex == 0 ? "/" : currentDir[..lastSlashIndex];
        
        Directory.SetCurrentDirectory(parentDir);
    }
}