using System.Text.RegularExpressions;

namespace Hospital.Domain.Patient.ValueObjects;

public record PatientInsuranceNumber(string Number)
{
    private static readonly Regex InsuranceRegex = new(@"^\d{11}$", RegexOptions.Compiled);

    public static PatientInsuranceNumber Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Номер страхового полиса обязателен.");

        var cleanNumber = Regex.Replace(number, @"[^\d]", "");
        if (!InsuranceRegex.IsMatch(cleanNumber))
            throw new ArgumentException("Номер страхового полиса должен содержать 11 цифр.");

        return new PatientInsuranceNumber(cleanNumber);
    }
}