using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.Common
{
    public static class DateExtensions
    {

        public static bool IsSameDate(this DateTime date, DateTime other)
        {
            return date.Date == other.Date;
        }


        public static bool IsOnOrBetweenDates(this DateTime date, DateTime startDate, DateTime endDate)
        {
            return (date.Date >= startDate.Date && date.Date <= endDate.Date);
        }


        public static DateTime FirstDayOfMonth(this DateTime date)
        {            
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, daysInMonth);
        }


        public static int Quarter(this DateTime date)
        {
            switch (date.Month)
            {
                case 1: return 1;
                case 2: return 1;
                case 3: return 1;
                case 4: return 2;
                case 5: return 2;
                case 6: return 3;
                case 7: return 3;
                case 8: return 3;
                case 9: return 3;
                case 10: return 4;
                case 11: return 4;
                case 12: return 4;
                default:
                    throw new ArgumentException($"Illegal month value {date.Month}");
            }
        }


        public static int MonthInQuarter(this DateTime date)
        {
            switch (date.Month)
            {
                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                case 4: return 1;
                case 5: return 2;
                case 6: return 3;
                case 7: return 1;
                case 8: return 2;
                case 9: return 3;
                case 10: return 1;
                case 11: return 2;
                case 12: return 3;
                default:
                    throw new ArgumentException($"Illegal month value {date.Month}");
            }
        }

    }
}
