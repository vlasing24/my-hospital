using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Doctor
{
    public readonly struct FullNameD
    {
        public string Value { get; }

        public FullNameD(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Полное имя не может быть нулевым или пустым.");
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}