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
                throw new ArgumentException("Дата рождения не может быть в будущем");
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd");
        }
    }
}