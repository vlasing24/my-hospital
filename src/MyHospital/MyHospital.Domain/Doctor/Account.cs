using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Doctor
{
    public class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Account(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login)) throw new ArgumentException("Логин не может быть пустым.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Пароль не может быть пустым");

            Login = login;
            Password = password;
        }
    }
}