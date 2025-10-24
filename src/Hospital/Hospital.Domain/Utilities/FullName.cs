using System.Text.RegularExpressions;

namespace Hospital.Domain.Utilities;

public record FullName(string FirstName, string MiddleName, string LastName)
{
    private static readonly Regex NameRegex = new(@"^[a-zA-Zа-яА-ЯёЁ\s\-']+$", RegexOptions.Compiled);

    public static FullName Create(string firstName, string middleName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Имя обязательно.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Фамилия обязательна.");

        if (!NameRegex.IsMatch(firstName))
            throw new ArgumentException("Имя содержит недопустимые символы.");
        if (!string.IsNullOrWhiteSpace(middleName) && !NameRegex.IsMatch(middleName))
            throw new ArgumentException("Отчество содержит недопустимые символы.");
        if (!NameRegex.IsMatch(lastName))
            throw new ArgumentException("Фамилия содержит недопустимые символы.");

        return new FullName(firstName, middleName ?? string.Empty, lastName);
    }

    public override string ToString() => $"{LastName} {FirstName} {MiddleName}".Trim();
}