using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyHospital.Domain.Entities.Patient.ValueObjects
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

            string omsRegex = @"^\d{16}$";

            if (!Regex.IsMatch(value, omsRegex))
            {
                throw new ArgumentException("Неверный формат страхового номера ОМС. Должно быть 16 цифр.");
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}