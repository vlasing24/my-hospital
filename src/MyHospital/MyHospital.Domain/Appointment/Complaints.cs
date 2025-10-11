using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public readonly struct Complaints
    {
        public string Description { get; private set; }

        private Complaint(string description)
        {
            Description = description;
        }

        public static Complaint Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Описание жалобы не может быть пустым.");
            }

            return new Complaint(description);
        }

        public override string ToString()
        {
            return Description;
        }
    }

    public class Complaints
    {
        private readonly ReadOnlyCollection<Complaint> _complaints;

        public ReadOnlyCollection<Complaint> AllComplaints => _complaints;

        public Complaints(List<Complaint> complaints)
        {
            _complaints = new ReadOnlyCollection<Complaint>(complaints);
        }
    }
}