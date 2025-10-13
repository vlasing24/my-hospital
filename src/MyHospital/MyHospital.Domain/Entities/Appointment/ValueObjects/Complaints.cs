using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Entities.Appointment.ValueObjects
{
    public readonly struct Complaints
    {
        public string Description { get; private set; }
        public const int MAX_LENGTH = 255;

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

            if (value.Length > MAX_LENGTH)
                throw new ArgumentException(
                    $"Описание жалоб не может быть больше {MAX_LENGTH} символов"
                );

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