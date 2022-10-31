namespace PVI.GQKN.API.Extensions;

public static class DateTimeExtension
{
    public static long ToUnixEpoch(this DateTime date)
    {
        long timestamp = ((DateTimeOffset)date).ToUnixTimeSeconds();
        return timestamp;
    }

    public static DateTime FromToUnixEpoch(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
}
