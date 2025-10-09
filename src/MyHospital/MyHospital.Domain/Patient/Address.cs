using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Patient
{
    public class Address
    {

        public string Street { get; }
        public string City { get; }
        public string Country { get; }

        private Address(string street, string city, string country)
        {

            Street = street;
            City = city;
            Country = country;
        }

        public static Result<Address> Create(string street, string city, string country)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(street))
                errors.Add("Улица не может быть пустой");
            if (string.IsNullOrWhiteSpace(city))
                errors.Add("Город не может быть пустым");
            if (string.IsNullOrWhiteSpace(country))
                errors.Add("Страна не может быть пустой");

            return errors.Count > 0
               ? Result.Failure<Address>(string.Join("; ", errors))
               : Result.Success(new Address(street, city, country));
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {Country}";
        }
    }
}