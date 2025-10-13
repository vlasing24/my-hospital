using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Entities.Doctor.ValueObjects
{
    internal record DoctorID
    {
        public Guid Id { get; }
        private DoctorID(Guid id)
        {
            Id = id;
        }
        public static PatientId Create(Guid value)
        {
            if (Guid.Empty == value)
            {
                throw new ArgumentNullException("Такой ID уже существует");
            }
            return new DoctorID(value);
        }
        public static DoctorID Create()
        {
            Guid Id = Guid.NewGuid();
            return new DoctorID(Id);
        }
    }
}