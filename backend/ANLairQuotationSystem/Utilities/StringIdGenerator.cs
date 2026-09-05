using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ANLairQuotationSystem.Utilities;

public class StringIdGenerator
{
    private static readonly char[] Alphabet =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_".ToCharArray();

    public static string Generate(int size = 15)
    {
        return string.Create(size, Alphabet, (span, alphabet) =>
        {
            RandomNumberGenerator.GetItems(alphabet, span);
        });
    }

    public static string GenerateUniqueId(string subjectName, DateTime? dateRequested = null)
    {
        if (string.IsNullOrWhiteSpace(subjectName))
            throw new ArgumentException(
                "Project name cannot be empty.",
                nameof(subjectName)
            );

        string abbreviation = GenerateAbbreviation(subjectName);
        string dateTime = dateRequested == null ?
            DateTime.Now.ToString("yyyyMMddHHmmss") :
            dateRequested.Value.ToString("yyyyMMddHHmmss");
        string randomString = StringIdGenerator.Generate();

        return $"{abbreviation}_{dateTime}_{randomString}";
    }

    private static string GenerateAbbreviation(string subjectName)
    {
        string[] words = Regex
            .Replace(subjectName.Trim(), @"[^a-zA-Z0-9\s]", "")
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
