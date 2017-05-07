using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.Models
{
    public enum Place
    {
        Placówka1,
        Placówka2,
        Placówka3
    }

    public class Appointment
    {
        public int AppointmentID { get; set; }

        [Display(Name = "Lekarz")]
        public int DoctorID { get; set; }

        [Display(Name = "Pacjent")]
        public int PatientID { get; set; } = 1;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Termin")]
        public DateTime Date { get; set; }

        [Display(Name = "Miejsce")]
        public Place Place { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
