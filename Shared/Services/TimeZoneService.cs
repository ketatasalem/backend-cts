using Labo_Cts_backend.Shared.IServices;

namespace Labo_Cts_backend.Shared.Services
{
    public class TimeZoneService : ITimeZoneService
    {
        public DateTime GetLocalTime(string timeZoneId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        }
    }
}
