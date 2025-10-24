namespace Hospital.Domain.Patient.ValueObjects;

public readonly record struct PatientGender
{
    public string Code { get; }

    private PatientGender(string code) => Code = code;

    public static readonly PatientGender Male = new("M");
    public static readonly PatientGender Female = new("F");

    public static PatientGender From(string code)
    {
        return code switch
        {
            "M" => Male,
            "F" => Female,
            _ => throw new ArgumentException($"Неизвестный пол: {code}")
        };
    }
}