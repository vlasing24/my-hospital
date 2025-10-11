using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Doctor
{
    public class Specialization
    {
        public string Value { get; private set; }

        private Specialization(string value)
        {
            Value = value;
        }

        public static Specialization Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Специализация не может быть пустой.");
            }
            return new Specialization(value);
        }
    }
}