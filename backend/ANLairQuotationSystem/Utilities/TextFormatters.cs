using System.Globalization;

namespace ANLairQuotationSystem.Utilities;

public class TextFormatters
{
    public static string ToTitleCase(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        return CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(text.Trim().ToLower());
    }
}
