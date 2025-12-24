using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarInvite
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;

    public class GoogleCalendarService
    {
        private readonly CalendarService _calendarService;

        public GoogleCalendarService()
        {
            _calendarService = CreateCalendarService().Result;
        }

        private async Task<CalendarService> CreateCalendarService()
        {
            using var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read);

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                new[] { CalendarService.Scope.Calendar },
                "mahmoudahmed10197@gmail.com", // sender (system account)
                CancellationToken.None,
                new FileDataStore("token.json", true)
            );

            return new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Appointment Scheduler"
            });
        }

        public async Task<string?> CreateAppointmentAsync(AppointmentRequest request)
        {
            var calendarEvent = new Event
            {
                Summary = $"{request.PatientName} - {request.AppointmentType}",
                Description = request.Description,
                Start = new EventDateTime
                {
                    DateTime = request.StartUtc,
                    TimeZone = "UTC"
                },
                End = new EventDateTime
                {
                    DateTime = request.EndUtc,
                    TimeZone = "UTC"
                },
                Attendees = new List<EventAttendee>
            {
                new EventAttendee { Email = request.DoctorEmail }
            }
            };

            // Telehealth → Create Google Meet
            if (request.IsTeleHealth)
            {
                calendarEvent.ConferenceData = new ConferenceData
                {
                    CreateRequest = new CreateConferenceRequest
                    {
                        RequestId = Guid.NewGuid().ToString(),
                        ConferenceSolutionKey = new ConferenceSolutionKey
                        {
                            Type = "hangoutsMeet"
                        }
                    }
                };
            }

            var insertRequest = _calendarService.Events.Insert(calendarEvent, "primary");
            insertRequest.SendUpdates = EventsResource.InsertRequest.SendUpdatesEnum.All;
            insertRequest.ConferenceDataVersion = request.IsTeleHealth ? 1 : 0;

            var createdEvent = await insertRequest.ExecuteAsync();

            // Return Google Meet Link (if exists)
            return createdEvent.ConferenceData?
                .EntryPoints?
                .FirstOrDefault(x => x.EntryPointType == "video")?
                .Uri;
        }
    }

}
