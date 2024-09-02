using System.Globalization;

namespace Resume.Bussines.Convertors;

public static class DateConvertor
{
    public static string ToShamsi(this DateOnly value)
    {
        PersianCalendar persianCalendar = new PersianCalendar();

        DateTime dateTime = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);

        return persianCalendar.GetYear(dateTime) + "/" +
               persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
               persianCalendar.GetDayOfMonth(dateTime).ToString("00");
    }
}