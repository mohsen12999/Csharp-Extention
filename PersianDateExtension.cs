using System;
using System.Globalization;

namespace Data.Utilities
{
    public static class PersianDateExtension
    {
        public static string PersianDateTime(this DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(dateTime).ToString("0000") + "/" +
                   persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(dateTime).ToString("00") + " " +
                   dateTime.ToString("HH:mm:ss");
        }

        public static string PersianDate(this DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(dateTime).ToString("0000") +
                   persianCalendar.GetMonth(dateTime).ToString("00") +
                   persianCalendar.GetDayOfMonth(dateTime).ToString("00");
        }

        public static string GetFullDate(this DateTime date)
        {
            try
            {
                var cl = new PersianCalendar();
                return cl.GetDayOfMonth(date).ToString("00") + " " + /*date.ToString("MMMM", new CultureInfo("fa-ir"))*/ PersianMonthName(cl.GetMonth(date)) + " " + cl.GetYear(date).ToString("00");
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetFullDateTime(this DateTime date)
        {
            try
            {
                var cl = new PersianCalendar();
                return cl.GetDayOfMonth(date).ToString("00") + " " + /*date.ToString("MMMM", new CultureInfo("fa-ir"))*/ PersianMonthName(cl.GetMonth(date)) + " " + cl.GetYear(date).ToString("00") + " ساعت " + date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + " " + date.ToString("tt", new CultureInfo("fa-ir"));
                // date.ToString("MMMM", new CultureInfo("fa-ir")) => Gregorian month with persian spell
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string PersianMonthName(int month)
        {
            switch (month)
            {
                case 1: return "فررودين";
                case 2: return "ارديبهشت";
                case 3: return "خرداد";
                case 4: return "تير‏";
                case 5: return "مرداد";
                case 6: return "شهريور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دي";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "";
            }
        }


        public static DateTime ToDateTime(string date)
        {
            try
            {
                var cl = new PersianCalendar();
                string[] str = date.Split('/');
                var d = cl.ToDateTime(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                return d;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static DateTime ToDateTime(string date, string time)
        {
            try
            {
                var cl = new PersianCalendar();
                string[] str = date.Split('/');
                string[] strT = time.Split(':');
                var d = cl.ToDateTime(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]), int.Parse(strT[0]), int.Parse(strT[1]), int.Parse(strT[2]), 0);
                return d;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
    }
}
