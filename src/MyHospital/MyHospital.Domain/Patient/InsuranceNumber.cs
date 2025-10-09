using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Patient
{
    public readonly struct InsuranceNumber
    {
        public string Value { get; }

        public InsuranceNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Страховой номер не может быть нулевым или пустым.");
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }