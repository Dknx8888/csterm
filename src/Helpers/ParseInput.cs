using System.Text;

namespace CodeCrafters.Shell.Helpers;

public static class ParseInput
{
    private const char SingleQuote = '\'';
    private const char DoubleQuote = '\"';
    private const char Backslash = '\\';
    
    public static string[] Parse(string input)
    {
        var parts = new List<string>(); // parts = [command, arg1, arg2, ...]
        var currentPart = new StringBuilder(); // To repeatedly add chars without creating a new str each time

        var insideSingleQuotes = false;
        var insideDoubleQuotes = false;
        var partStarted = false;

        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            
            switch (c)
            {
                case Backslash when !insideDoubleQuotes && !insideSingleQuotes:
                {
                    if (i + 1 >= input.Length)
                    {
                        throw new FormatException("Backslash must be followed by another character");
                    }

                    var escapedChar = input[++i];

                    currentPart.Append(escapedChar);
                    partStarted = true;
                    continue;
                }
                
                case DoubleQuote when !insideSingleQuotes:
                    insideDoubleQuotes = !insideDoubleQuotes;
                    partStarted = true;
                    continue;
                
                case SingleQuote when !insideDoubleQuotes:
                    insideSingleQuotes = !insideSingleQuotes; // Flip for start and end quotes
                    partStarted = true;
                    continue;
            }

            // Meets white space that is not inside '' or "" => one new arg => to parts
            if (char.IsWhiteSpace(c) && !insideSingleQuotes && !insideDoubleQuotes)
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

        if (insideDoubleQuotes)
        {
            throw new FormatException("Unterminated double quote");
        }

        // Handle last arg that has no space after
        if (partStarted)
        {
            parts.Add((currentPart.ToString()));
        }

        return [.. parts];
    }
}