using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Patient
{
    public class ContactInfo : IEquatable<ContactInfo>
    {
        public Email Email { get; }
        public PhoneNumber PhoneNumber { get; }

        private ContactInfo(Email email, PhoneNumber phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static Result<ContactInfo> Create(Email email, PhoneNumber phoneNumber)
        {
            var errors = new List<string>();

            if (email == null)
                errors.Add("Email не может быть пустым");

            if (phoneNumber == null)
                errors.Add("Номер телефона не может быть пустым");

            if (errors.Count > 0)
            {
                return Result.Failure<ContactInfo>(string.Join("; ", errors));
            }

            return Result.Success(new ContactInfo(email, phoneNumber));
        }

        public override bool Equals(object obj)
        {
            if (obj is ContactInfo other)
            {
                return Email.Equals(other.Email) &&
                       PhoneNumber.Equals(other.PhoneNumber);
            }
            return false;
        }

        public bool Equals(ContactInfo other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Email.Equals(other.Email) &&
                   PhoneNumber.Equals(other.PhoneNumber);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, PhoneNumber);
        }

        public override string ToString() => $"Почта: {Email}, Телефон: {PhoneNumber}";
    }
}