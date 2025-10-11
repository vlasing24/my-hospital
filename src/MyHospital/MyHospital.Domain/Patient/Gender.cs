using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHospital.Domain.Patient
{
    public class Gender
    {
        public string Name { get; private set; }
        public int Value { get; private set; }

        private Gender(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public static readonly Gender Male = new Gender("Male", 1);
        public static readonly Gender Female = new Gender("Female", 2);

        public static IEnumerable<Gender> GetAll()
        {
            return new[] { Male, Female };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Gender other = (Gender)obj;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}