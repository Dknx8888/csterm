using System.Text;

namespace CodeCrafters.Shell.Helpers;

public static class ParseInput
{
    public static string[] Parse(string input)
    {
        var parts = new List<string>(); // parts = [command, arg1, arg2, ...]
        var currentPart = new StringBuilder(); // To repeatedly add chars without creating a new str each time

        var insideSingleQuotes = false;
        var partStarted = false;

        foreach (var c in input)
        {
            if (c == '\'')
            {
                insideSingleQuotes = !insideSingleQuotes; // Flip for start and end quotes
                partStarted = true;
                continue;
            }
            
            // Meets white space that is not inside '' => one new arg => to parts
            if (char.IsWhiteSpace(c) && !insideSingleQuotes)
            {
                if (partStarted)
                {
                    parts.Add(currentPart.ToString());
                    currentPart.Clear();
                    partStarted = false;
                }
                continue;
            }

            currentPart.Append(c); // Append each normal char to the part until end single quote or space
            partStarted = true;
        }

        if (insideSingleQuotes)
        {
            throw new FormatException("Unterminated single quote");
        }

        // Handle last arg that has no space after
        if (partStarted)
        {
            parts.Add((currentPart.ToString()));
        }

        return [.. parts];
    }
}