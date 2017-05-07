using Medic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic
{
    public class WebMedicInitializer : System.Data.Entity.DropCreateDatabaseAlways<WebMedicContext>
    {
        public override void InitializeDatabase(WebMedicContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
                , string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }

        protected override void Seed(WebMedicContext context)
        {
            var doctors = new List<Doctor>
            {
                new Doctor {Name="Jan", Surname="Kowalski", Specialization=Specialization.Dermatolog },
                new Doctor {Name="Felicjan", Surname="Swędzidół", Specialization=Specialization.Kardiolog},
                new Doctor {Name="Adan", Surname="Nowak", Specialization=Specialization.Laryngolog }
            };
            doctors.ForEach(d => context.Doctors.Add(d));
            context.SaveChanges();

            var patients = new List<Patient>
            {
                new Patient {Name="Wolny", Surname="Termin" },
                new Patient {Name="Zbigniew", Surname="Wodecki" },
                new Patient {Name="Jan", Surname="Panasewicz" },
                new Patient {Name="Krzysztof", Surname="Ibisz" },
            };
            patients.ForEach(d => context.Patients.Add(d));
            context.SaveChanges();

            var appointments = new List<Appointment>
            {
                new Appointment { PatientID=1, DoctorID=1, Date=DateTime.Parse("2001-09-01"), Place=Place.Placówka1 },
                new Appointment { PatientID=1, DoctorID=2, Date=DateTime.Parse("2001-09-02"), Place=Place.Placówka2 },
                new Appointment { PatientID=3, DoctorID=2, Date=DateTime.Parse("2001-09-03"), Place=Place.Placówka2 },
                new Appointment { PatientID=2, DoctorID=3, Date=DateTime.Parse("2001-09-04"), Place=Place.Placówka3},
                new Appointment { PatientID=2, DoctorID=1, Date=DateTime.Parse("2001-09-05"), Place=Place.Placówka1 },
            };
            appointments.ForEach(d => context.Appointments.Add(d));
            context.SaveChanges();
        }
    }
}
