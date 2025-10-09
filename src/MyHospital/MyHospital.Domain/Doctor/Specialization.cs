using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Doctor
{
    public readonly struct Specialization
    {
        public string Value { get; }

        public Specialization(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Специализация не может быть пустой.");
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}