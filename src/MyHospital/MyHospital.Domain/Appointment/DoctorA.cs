using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public class DoctorA
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Specialization { get; private set; }

        public DoctorA(string name, string specialization)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Specialization = specialization ?? throw new ArgumentNullException(nameof(specialization));
        }
    }
}