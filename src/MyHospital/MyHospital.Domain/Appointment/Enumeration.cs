using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public abstract class Enumeration<TEnum>
      where TEnum : Enumeration<TEnum>
    {
        private static Dictionary<int, Func<TEnum>> _keyFactories = [];
        private static Dictionary<string, Func<TEnum>> _nameFactories = [];
        public int Key { get; }
        public string Name { get; }
        protected Enumeration(int key, string name)
        {
            Key = key;
            Name = name;
        }
        public static TEnum FromKey(int key)
        {
            return _keyFactories.TryGetValue(key, out Func<TEnum>? factory)
                ? factory()
                : throw new ArgumentException("Не поддерживаемый ключ перечисления");
        }

        public static TEnum FromName(string name)
        {
            return _nameFactories.TryGetValue(name, out Func<TEnum>? factory)
                ? factory()
                : throw new ArgumentException("Не поддерживаемый название перечисления");

        }
        public static Dictionary<int, Func<TEnum>> FetchKeyFactories()
        {
            Type enumType = typeof(TEnum);
            IEnumerable<Type> childs = enumType
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(enumType) && !t.IsAbstract);
            Dictionary<int, Func<TEnum>> _factories = [];
            foreach (Type entry in childs)
            {
                ConstructorInfo constructor = entry
                    .GetConstructors()
                    .First(c => c.GetParameters().Length == 0);
                Func<TEnum> factory = () => (TEnum)constructor.Invoke(null);
                TEnum enumeration = factory();
                int key = enumeration.Key;
                _factories.Add(key, factory);
            }
            return _factories;
        }
        public static Dictionary<string, Func<TEnum>> FetchNameFactories()
        {
            IEnumerable<Type> childs = FetchTypes();
            Dictionary<string, Func<TEnum>> _factories = [];
            foreach (Type entry in childs)
            {
                Func<TEnum> factory = CreateFactoryFromConstructor(entry);
                TEnum enumeration = factory();
                string name = enumeration.Name;
                _factories.Add(name, factory);
            }
            return _factories;
        }
        public static Func<TEnum> CreateFactoryFromConstructor(Type type)
        {
            ConstructorInfo constructor = type.GetConstructors()
                .First(c => c.GetParameters().Length == 0);
            Func<TEnum> factory = () => (TEnum)constructor.Invoke(null);
            return factory;
        }
        private static IEnumerable<Type> FetchTypes()
        {
            Type enumType = typeof(TEnum);
            return enumType.Assembly.GetTypes().Where(t => t.IsSubclassOf(enumType) && !t.IsAbstract);
        }
    }

    public sealed class AppointmentStatusInConsideration : AppointmentStatus
    {
        public AppointmentStatusInConsideration() : base(1, "В рассмотрении") { }
        public override bool CanCreateComplaint() => false;
        public override bool CanUpdateDiagnosis() => false;
    }

    public sealed class AppointmentStatusInProgress : AppointmentStatus
    {
        public AppointmentStatusInProgress() : base(2, "В процессе") { }
        public override bool CanCreateComplaint() => false;
        public override bool CanUpdateDiagnosis() => true;
    }

    public sealed class AppointmentStatusProcessed : AppointmentStatus
    {
        public AppointmentStatusProcessed() : base(3, "Обработан") { }
        public override bool CanCreateComplaint() => true;
        public override bool CanUpdateDiagnosis() => false;
    }

    public sealed class AppointmentStatusRejected : AppointmentStatus
    {
        public AppointmentStatusRejected() : base(4, "Отклонен") { }
        public override bool CanCreateComplaint() => false;
        public override bool CanUpdateDiagnosis() => false;
    }

    public sealed class AppointmentStatusMissed : AppointmentStatus
    {
        public AppointmentStatusMissed() : base(5, "Пропущен") { }
        public override bool CanCreateComplaint() => false;
        public override bool CanUpdateDiagnosis() => false;
    }
}