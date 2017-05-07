using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.Models
{

    public enum Specialization
    {
        Internista,
        Kardiolog,
        Dermatolog,
        Ginekolog,
        Urolog,
        Laryngolog,
        Endokrynolog,
        Neurolog,
        Okulista
    }

    public class Doctor
    {
        public int DoctorID { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Specjalizacja")]
        public Specialization Specialization { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
