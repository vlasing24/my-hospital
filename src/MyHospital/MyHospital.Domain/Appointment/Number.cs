using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public class Number
    {
        public string NumberValue { get; private set; }

        public Number(string numberValue)
        {
            NumberValue = numberValue ?? throw new ArgumentNullException(nameof(numberValue));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Number other = (Number)obj;
            return NumberValue == other.NumberValue;
        }

        public override int GetHashCode()
        {
            return NumberValue.GetHashCode();
        }
    }
}