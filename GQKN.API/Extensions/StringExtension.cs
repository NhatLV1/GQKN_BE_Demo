
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    static MD5 md5Instance = MD5.Create();

    public static string EncodeBase64(this string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string MD5Hash(this string input)
    {
        // Step 1, calculate MD5 hash from input
       
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5Instance.ComputeHash(inputBytes);

        // Step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString().ToLower();
    }

    public static string DecodeBase64(this string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static bool IsGuid(this string value)
    {
        return Guid.TryParse(value, out _);
    }

    public static Guid? ToGuid(this string value)
    {
        Guid guid;
        if (Guid.TryParse(value, out guid))
        {
            return guid;
        }

        return null;
    }

    /// <summary>
    ///  ref.: https://html.spec.whatwg.org/multipage/forms.html#valid-e-mail-address (HTML5 living standard, willful violation of RFC 3522)
    /// </summary>
    public static readonly string EmailValidation_Regex = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

    public static readonly Regex EmailValidation_Regex_Compiled = new Regex(EmailValidation_Regex, RegexOptions.IgnoreCase);

    public static readonly string EmailValidation_Regex_JS = $"/{EmailValidation_Regex}/";

    /// <summary>
    /// Checks if the given e-mail is valid using various techniques
    /// </summary>
    /// <param name="email">The e-mail address to check / validate</param>
    /// <param name="useRegEx">TRUE to use the HTML5 living standard e-mail validation RegEx, FALSE to use the built-in validator provided by .NET (default: FALSE)</param>
    /// <param name="requireDotInDomainName">TRUE to only validate e-mail addresses containing a dot in the domain name segment, FALSE to allow "dot-less" domains (default: FALSE)</param>
    /// <returns>TRUE if the e-mail address is valid, FALSE otherwise.</returns>
    public static bool IsValidEmailAddress(this string email, 
        bool useRegEx = false, 
        bool requireDotInDomainName = false)
    {
        var isValid = useRegEx
            // see RegEx comments
            ? email is not null && EmailValidation_Regex_Compiled.IsMatch(email)

            // ref.: https://stackoverflow.com/a/33931538/1233379
            : new EmailAddressAttribute().IsValid(email);

        if (isValid && requireDotInDomainName)
        {
            var arr = email.Split('@', StringSplitOptions.RemoveEmptyEntries);
            isValid = arr.Length == 2 && arr[1].Contains(".");
        }
        return isValid;
    }
    public static string GenarateRandomString(int? length = 10)
    {
        string result = "";
        string randomList = "0123456789abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUWXYZ";
        //var stringChars = new char[1];
        var random = new Random();

        for (var i = 0; i < randomList.Length; i++)
        {
            result = new string(Enumerable.Repeat(randomList, length.Value)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        }
        return result;
    }
}