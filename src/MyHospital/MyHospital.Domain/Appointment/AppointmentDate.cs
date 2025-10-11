using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public class AppointmentDate
    {
        public DateTime Value { get; private set; }

        private AppointmentDate(DateTime value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AppointmentDate other = (AppointmentDate)obj;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd HH:mm");
        }

        public AppointmentDate AddDays(int days)
        {
            return new AppointmentDate(Value.AddDays(days));
        }
    }
}