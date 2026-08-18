using System.Security.Cryptography;

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
}
