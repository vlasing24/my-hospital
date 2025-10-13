namespace MyHospital.Domain.Entities.Patient.ValueObjects
{
    internal record PatientId
    {
        public Guid Id { get; }
        private PatientId(Guid id)
        {
            Id = id;
        }
        public static PatientId Create(Guid value)
        {
            if (Guid.Empty == value)
            {
                throw new ArgumentNullException("Такой ID уже существует");
            }
            return new PatientId(value);
        }
        public static PatientId Create()
        {
            Guid Id = Guid.NewGuid();
            return new PatientId(Id);
        }
    }
}