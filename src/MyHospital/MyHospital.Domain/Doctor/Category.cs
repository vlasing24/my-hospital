using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHospital.Domain.Doctor
{
    public class Category
    {
        public string Name { get; private set; }
        public int Value { get; private set; }

        private Category(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public static readonly Category Doctor = new Category("Doctor", 1);
        public static readonly Category SeniorDoctor = new Category("SeniorDoctor", 2);
        public static readonly Category HeadOfDepartment = new Category("HeadOfDepartment", 3);

        public static IEnumerable<Category> GetAll()
        {
            return new[] { Doctor, SeniorDoctor, HeadOfDepartment };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Category other = (Category)obj;
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