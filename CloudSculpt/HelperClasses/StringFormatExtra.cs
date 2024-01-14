using System;
using System.Text;

namespace CloudSculpt.HelperClasses;

public static class StringFormatExtra
{
    #region Debug

    public static void ShowCharacterTypesAndEncoding(string input)
    {
        Console.WriteLine("Character\tType\t\tEncoding");
        Console.WriteLine("-------------------------------------");

        foreach (char c in input)
        {
            Console.WriteLine($"{c}\t\t{GetCharacterType(c)}\t\t{GetCharacterEncoding(c)}");
        }
    }

    #endregion
    
    public static string GetFilteredString(string input)
    {
        StringBuilder filteredStringBuilder = new StringBuilder();

        foreach (char c in input)
        {
            string characterType = GetCharacterType(c);

            // Add characters that are not of type "Other" or "Whitespace" Excluding '.' to the filtered string
            if (characterType != "Other" && characterType != "Whitespace" || c == '.')
            {
                filteredStringBuilder.Append(c);
            }
        }

        return filteredStringBuilder.ToString();
    }
    
    static string GetCharacterType(char c)
    {
        if (char.IsLetter(c))
            return "Letter";
        if (char.IsDigit(c))
            return "Digit";
        if (char.IsWhiteSpace(c))
            return "Whitespace";
        return "Other";
    }
    
    static string GetCharacterEncoding(char c)
    {
        byte[] bytes = Encoding.Unicode.GetBytes(new char[] { c });
        return BitConverter.ToString(bytes);
    }
}