using System;
using System.Globalization;

namespace villaayab.Data.Utilities
{
    public static class PersianDateExtension
    {
        public static string PersianDate(this DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(dateTime).ToString("0000") +
                   persianCalendar.GetMonth(dateTime).ToString("00") +
                   persianCalendar.GetDayOfMonth(dateTime).ToString("00");
        }

        public static string PersianDateTime(this DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(dateTime).ToString("0000") + "/" +
                   persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(dateTime).ToString("00") + " " +
                   dateTime.ToString("HH:mm:ss");
        }
    }
}
