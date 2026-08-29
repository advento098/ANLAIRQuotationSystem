using System.Text;
using System.Text.RegularExpressions;

namespace ANLairQuotationSystem.Utilities;

public class TextManager
{
    public static string GenerateProjectId(string projectName, DateTime? dateRequested = null)
    {
        if (string.IsNullOrWhiteSpace(projectName))
            throw new ArgumentException(
                "Project name cannot be empty.",
                nameof(projectName)
            );

        string abbreviation = GenerateAbbreviation(projectName);
        string dateTime = dateRequested == null ?
            DateTime.Now.ToString("yyyyMMddHHmmss") :
            dateRequested.Value.ToString("yyyyMMddHHmmss");
        string randomString = StringIdGenerator.Generate();

        return $"{abbreviation}_{dateTime}_{randomString}";
    }

    private static string GenerateAbbreviation(string projectName)
    {
        string[] words = Regex
            .Replace(projectName.Trim(), @"[^a-zA-Z0-9\s]", "")
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (words.Length == 1)
        {
            return words[0]
                [..Math.Min(4, words[0].Length)]
                .ToUpperInvariant();
        }

        StringBuilder abbreviation = new();

        foreach (string word in words)
        {
            abbreviation.Append(char.ToUpperInvariant(word[0]));
        }

        return abbreviation.ToString();
    }
}
