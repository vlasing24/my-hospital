using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public readonly struct PreliminaryDiagnosis
    {
        public string Value { get; }

        public PreliminaryDiagnosis(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
