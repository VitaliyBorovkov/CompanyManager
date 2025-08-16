using System.Text.RegularExpressions;

public class InputValidation
{
    private static readonly Regex latin = new Regex(@"^[a-zA-Z\s]+$");

    public static bool IsLatin(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        return latin.IsMatch(input);
    }
}
