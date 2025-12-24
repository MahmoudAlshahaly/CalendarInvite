using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace CalendarInvite
{
    internal class Program
    {
        static async Task Main()
        {
            //"ashmoh789@gmail.com"
            var calendarService = new GoogleCalendarService();

            var appointment = new AppointmentRequest
            {
                DoctorEmail = "anaengineer34@gmail.com",
                PatientName = "Ahmed Ali",
                AppointmentType = "Telehealth Consultation",
                Description = "Initial patient consultation",
                StartUtc = DateTime.UtcNow.AddHours(1),
                EndUtc = DateTime.UtcNow.AddHours(2),
                IsTeleHealth = true
            };

            var meetingLink = await calendarService.CreateAppointmentAsync(appointment);

            Console.WriteLine("Appointment created successfully");

            if (meetingLink != null)
            {
                Console.WriteLine($"Google Meet Link: {meetingLink}");
                // Save this in DB → Appointment.MeetingLink
            }
        }
    }
}


