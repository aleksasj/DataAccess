namespace DataAccess.Helper;

public static class GeoLocation
{
    public static float FormatToStandart(float cordinate)
    {
        return (float)Math.Round(cordinate, 5);
    }
}
