using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarInvite
{
    public class AppointmentRequest
    {
        public string DoctorEmail { get; set; }
        public string PatientName { get; set; }
        public string AppointmentType { get; set; } // Consultation, Follow-up, History Call
        public string Description { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public bool IsTeleHealth { get; set; }
    }

}
