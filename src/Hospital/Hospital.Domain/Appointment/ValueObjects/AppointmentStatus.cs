namespace Hospital.Domain.Appointment.ValueObjects
{
    public sealed class AppointmentStatus : IEquatable<AppointmentStatus>
    {
        public string Code { get; }
        public string DisplayName { get; }

        public static readonly AppointmentStatus Scheduled = new("ЗАП", "Запланирован");
        public static readonly AppointmentStatus Completed = new("ЗАВ", "Завершён");
        public static readonly AppointmentStatus Cancelled = new("ОТМ", "Отменён");

        private static readonly IReadOnlyCollection<AppointmentStatus> All = new[]
        {
        Scheduled, Completed, Cancelled
    };

        private AppointmentStatus(string code, string displayName)
        {
            Code = code;
            DisplayName = displayName;
        }

        public static AppointmentStatus FromCode(string code) =>
            All.FirstOrDefault(s => s.Code == code)
            ?? throw new ArgumentException($"Неизвестный статус приёма: {code}");

        public static IReadOnlyCollection<AppointmentStatus> GetAll() => All;

        public bool Equals(AppointmentStatus? other) => other != null && Code == other.Code;
        public override bool Equals(object? obj) => Equals(obj as AppointmentStatus);
        public override int GetHashCode() => Code.GetHashCode();
        public override string ToString() => DisplayName;
    }
}