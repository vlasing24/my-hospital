using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public class AppointmentStatus
    {
        public string Name { get; private set; }
        public int Value { get; private set; }

        private AppointmentStatus(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public static readonly AppointmentStatus InConsideration = new AppointmentStatus("В рассмотрении", 1);
        public static readonly AppointmentStatus InProgress = new AppointmentStatus("В процессе", 2);
        public static readonly AppointmentStatus Processed = new AppointmentStatus("Обработан", 3);
        public static readonly AppointmentStatus Rejected = new AppointmentStatus("Отклонен", 4);
        public static readonly AppointmentStatus Missed = new AppointmentStatus("Пропущен", 5);


        public static IEnumerable<AppointmentStatus> GetAll()
        {
            return new[] { InConsideration, InProgress, Processed, Rejected, Missed };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AppointmentStatus other = (AppointmentStatus)obj;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public bool CanBeChangedTo(AppointmentStatus newStatus)
        {

            return true;
        }
    }
}