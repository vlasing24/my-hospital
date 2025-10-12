using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHospital.Domain.Appointment
{
    public class AppointmentStatus
    {
        public abstract class AppointmentStatus : Enumeration<AppointmentStatus>
        {
            protected AppointmentStatus(int key, string name) : base(key, name)
            {

            }

            public abstract bool CanCreateComplaint();
            public abstract bool CanUpdateDiagnosis();
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
}