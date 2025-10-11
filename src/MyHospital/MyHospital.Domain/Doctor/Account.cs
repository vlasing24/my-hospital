using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyHospital.Domain.Doctor
{
    public class Account
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public Account(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login)) throw new ArgumentException("Логин не может быть пустым.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Пароль не может быть пустым");

            string loginRegex = @"^[a-zA-Z0-9_]{5,20}$";

            if (!Regex.IsMatch(login, loginRegex))
            {
                throw new ArgumentException("Неверный формат логина. Допустимы только латинские буквы, цифры и подчеркивание (5-20 символов).");
            }

            string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

            if (!Regex.IsMatch(password, passwordRegex))
            {
                throw new ArgumentException("Неверный формат пароля. Минимум 8 символов, должна быть хотя бы одна буква и одна цифра.");
            }

            Login = login;
            Password = password;
        }
    }
}