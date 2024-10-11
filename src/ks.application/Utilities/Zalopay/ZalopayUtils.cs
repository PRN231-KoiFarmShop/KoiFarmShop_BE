namespace ks.application.Utilities.Zalopay;
public static class ZalopayUtils
{
    public static long GetTimeStamp(DateTime date)
    {
        return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
    }

    public static long GetTimeStamp()
    {
        return GetTimeStamp(DateTime.Now);
    }
}