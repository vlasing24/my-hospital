using MyHospital.Domain.Entities.Doctor.ValueObjects;
using MyHospital.Domain.Patient;
using System;
using static MyHospital.Domain.Entities.Doctor.ValueObjects.Category;

namespace MyHospital.Domain.Entities.Doctor;

public class Doctor
{
    public DoctorID ID { get; private set; }
    public PersonName Name { get; private set; }
    public Specialization Specialization { get; set; }
    public Category Category { get; set; }
    public Account Credentials { get; set; }

    public Doctor(ID id, PersonName name, Specialization specialization, DoctorCategory category, Account credentials)
    {
        ID = id;
        Name = name;
        Specialization = specialization;
        Category = category;
        Credentials = credentials;
    }

    public override string ToString()
    {
        return $"Доктор ID: {ID}, ФИО: {Name}, Специализация: {Specialization}";
    }
}