using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Patient
{
    public readonly struct DateOfBirth
    {
        public DateTime Value { get; }

        public DateOfBirth(DateTime value)
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("DateOfBirth cannot be in the future.");
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd");
        }
    }
}