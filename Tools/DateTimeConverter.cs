namespace CleveroadWeatherBackend.Tools;

public static class DateTimeConverter
{
    public static DateTime UnixDateTimeToUtc(int dateTime) => DateTime.UnixEpoch.AddSeconds(dateTime).ToLocalTime();
    public static int UtcToUnixDateTime(DateTime dateTime) => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
}