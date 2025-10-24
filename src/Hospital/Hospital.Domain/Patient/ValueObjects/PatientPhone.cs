using System.Text.RegularExpressions;

public record PatientPhone(string Number)
{
    private static readonly Regex PhoneRegex = new(
        @"^\+?7\d{10}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static PatientPhone Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Номер телефона обязателен.");

        var cleanNumber = Regex.Replace(number, @"[^\d+]", "");

        if (!PhoneRegex.IsMatch(cleanNumber))
            throw new ArgumentException("Некорректный формат номера телефона. Ожидается +7XXXXXXXXXX или 7XXXXXXXXXX.");

        if (cleanNumber.StartsWith("7"))
            cleanNumber = "+" + cleanNumber;
        else if (cleanNumber.StartsWith("8"))
            cleanNumber = "+7" + cleanNumber.Substring(1);

        if (!PhoneRegex.IsMatch(cleanNumber))
            throw new ArgumentException("Некорректный формат номера телефона после очистки.");

        return new PatientPhone(cleanNumber);
    }
}