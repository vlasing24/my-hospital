using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Patient
{
    public readonly struct FullName
    {
        public string Value { get; }

        public FullName(string value)
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