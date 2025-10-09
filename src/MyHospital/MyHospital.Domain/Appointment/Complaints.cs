using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public readonly struct Complaints
    {
        public string Value { get; }

        public Complaints(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Жалобы не могут быть пустыми.");
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}