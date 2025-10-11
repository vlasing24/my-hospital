using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public class Email : IEquatable<Email>
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<Email>("Email не может быть пустым");
            }

            // Регулярное выражение для валидации email
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailRegex))
            {
                return Result.Failure<Email>("Неверный формат email");
            }
            return Result.Success(new Email(email));
        }

        public bool Equals(Email other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Email)obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class PhoneNumber : IEquatable<PhoneNumber>
    {
        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return Result.Failure<PhoneNumber>("Номер телефона не может быть пустым");
            }

            // Регулярное выражение для валидации номера телефона
            // Примечание: это простой пример, реальные требования могут быть сложнее
            string phoneRegex = @"^(\+?\d{1,3})?[-.\s]?(\(\d{1,}\))?[-.\s]?[\d-.\s]{3,}$";
            if (!Regex.IsMatch(phoneNumber, phoneRegex))
            {
                return Result.Failure<PhoneNumber>("Неверный формат номера телефона");
            }

            return Result.Success(new PhoneNumber(phoneNumber));
        }

        public bool Equals(PhoneNumber other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PhoneNumber)obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}