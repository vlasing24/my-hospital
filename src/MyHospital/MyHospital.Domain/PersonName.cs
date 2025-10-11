using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain
{
    public class PersonName
    {
        public string Name { get; private set; }  
        public string Surname { get; private set; } 
        public string Thirdname { get; private set; } 

        private PersonName(string name, string surname, string thirdname)
        {
            Name = name;
            Surname = surname;
            Thirdname = thirdname;
        }

        public static PersonName Create(string name, string surname, string thirdname)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname)) || string.IsNullOrWhiteSpace(thirdname))
            {
                throw new ArgumentException("ФИО не может быть пустым");
            }
            return new PersonName(name, surname, thirdname);
        }
    }