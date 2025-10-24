using System.Text.RegularExpressions;

namespace Hospital.Domain.Patient.ValueObjects;

public record PatientAddress(
    string City,
    string Street,
    string BuildingNumber,
    string ApartmentNumber)
{
    private static readonly Regex BuildingRegex = new(@"^[a-zA-Z0-9]+$", RegexOptions.Compiled);
    private static readonly Regex ApartmentRegex = new(@"^[a-zA-Z0-9\s\.]+$", RegexOptions.Compiled);

    public static PatientAddress Create(
        string city,
        string street,
        string buildingNumber,
        string apartmentNumber)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Город обязателен.");
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Улица обязательна.");
        if (string.IsNullOrWhiteSpace(buildingNumber))
            throw new ArgumentException("Номер дома обязателен.");
        if (!BuildingRegex.IsMatch(buildingNumber))
            throw new ArgumentException("Некорректный формат номера дома (только буквы и цифры).");

        apartmentNumber ??= string.Empty;
        if (!string.IsNullOrWhiteSpace(apartmentNumber) && !ApartmentRegex.IsMatch(apartmentNumber))
            throw new ArgumentException("Некорректный формат номера квартиры.");

        return new PatientAddress(city, street, buildingNumber, apartmentNumber);
    }

    public override string ToString()
    {
        var address = $"{City}, ул. {Street}, д. {BuildingNumber}";
        if (!string.IsNullOrWhiteSpace(ApartmentNumber))
            address += $", кв. {ApartmentNumber}";
        return address;
    }
}