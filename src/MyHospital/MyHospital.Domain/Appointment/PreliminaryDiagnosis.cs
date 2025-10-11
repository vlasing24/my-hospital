using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public readonly struct PreliminaryDiagnosis
    {
        public string Value { get; private set; }

        private PreliminaryDiagnosis(string value)
        {
            Value = value;
        }

        public static PreliminaryDiagnosis Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Предварительный диагноз не может быть пустым.");
            }
            return new PreliminaryDiagnosis(value);
        }
    }
}