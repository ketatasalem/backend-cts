namespace Labo_Cts_backend.Shared.IServices
{
    public interface ITimeZoneService
    {
        DateTime GetLocalTime(string timeZoneId);
    }
}
