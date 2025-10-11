using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;

namespace MyHospital.Domain.Doctor
{
    public class Account
    {
        private static readonly Regex _loginRegex = new Regex(@"^[a-zA-Z0-9_]{5,20}$", RegexOptions.Compiled);
        private static readonly Regex _passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.Compiled);

        public string Login { get; private set; }
        private string PasswordHash { get; set; }

        private Account(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }

        public static Result<Account> Create(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login)) return Result.Failure<Account>("Логин не может быть пустым.");
            if (string.IsNullOrWhiteSpace(password)) return Result.Failure<Account>("Пароль не может быть пустым");


            if (!_loginRegex.IsMatch(login))
            {
                return Result.Failure<Account>("Неверный формат логина. Допустимы только латинские буквы, цифры и подчеркивание (5-20 символов).");
            }


            if (!_passwordRegex.IsMatch(password))
            {
                return Result.Failure<Account>("Неверный формат пароля. Минимум 8 символов, должна быть хотя бы одна буква и одна цифра.");
            }


            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            return Result.Success(new Account(login, passwordHash));
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}