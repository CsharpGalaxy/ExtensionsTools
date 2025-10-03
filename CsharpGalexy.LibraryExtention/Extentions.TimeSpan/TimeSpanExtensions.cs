using System;
using System.Collections.Generic;



    /// <summary>
    /// Extension methods for <see cref="TimeSpan"/>
    /// </summary>
    public static class TimeSpanExtensions
{
        /// <summary>
        /// Converts a <see cref="TimeSpan"/> into a human-readable string (e.g., "2 days, 3 hours, 5 minutes").
        /// </summary>
        public static string ToHumanReadable(this TimeSpan ts)
        {
            if (ts == TimeSpan.Zero)
                return "0 seconds";

            string result = string.Empty;

            if (ts.Days > 0)
                result += $"{ts.Days} days, ";
            if (ts.Hours > 0)
                result += $"{ts.Hours} hours, ";
            if (ts.Minutes > 0)
                result += $"{ts.Minutes} minutes, ";
            if (ts.Seconds > 0)
                result += $"{ts.Seconds} seconds, ";

            return result.Trim().TrimEnd(',');
        }
    /// <summary>
    /// Converts a <see cref="TimeSpan"/> into a human-readable string (e.g., "2 days, 3 hours, 5 minutes").
    /// </summary>
    public static string ToHumanReadablePersian(this TimeSpan ts)
    {
        if (ts == TimeSpan.Zero)
            return "0 seconds";

        string result = string.Empty;

        if (ts.Days > 0)
            result += $"{ts.Days} روز, ";
        if (ts.Hours > 0)
            result += $"{ts.Hours} ساعت, ";
        if (ts.Minutes > 0)
            result += $"{ts.Minutes} دقیقه, ";
        if (ts.Seconds > 0)
            result += $"{ts.Seconds} ثانیه, ";

        return result.Trim().TrimEnd(',');
    }

    /// <summary>
    /// Returns the total minutes in the <see cref="TimeSpan"/> (including days and hours).
    /// </summary>
    public static double TotalMinutesExact(this TimeSpan ts) => ts.TotalMinutes;

        /// <summary>
        /// Returns the total number of weeks in the <see cref="TimeSpan"/>.
        /// </summary>
        public static double TotalWeeks(this TimeSpan ts) => ts.TotalDays / 7;

        /// <summary>
        /// Checks whether the <see cref="TimeSpan"/> is positive.
        /// </summary>
        public static bool IsPositive(this TimeSpan ts) => ts.Ticks > 0;

        /// <summary>
        /// Checks whether the <see cref="TimeSpan"/> is negative.
        /// </summary>
        public static bool IsNegative(this TimeSpan ts) => ts.Ticks < 0;

        /// <summary>
        /// Clamps the <see cref="TimeSpan"/> between two values.
        /// </summary>
        public static TimeSpan Clamp(this TimeSpan ts, TimeSpan min, TimeSpan max)
        {
            if (ts < min) return min;
            if (ts > max) return max;
            return ts;
        }

        /// <summary>
        /// Returns the absolute value of the <see cref="TimeSpan"/> (removes negative sign).
        /// </summary>
        public static TimeSpan Abs(this TimeSpan ts) => ts.Duration();

        /// <summary>
        /// Rounds the <see cref="TimeSpan"/> to the nearest minute.
        /// </summary>
        public static TimeSpan RoundToNearestMinute(this TimeSpan ts) =>
            TimeSpan.FromMinutes(Math.Round(ts.TotalMinutes));

        /// <summary>
        /// Rounds the <see cref="TimeSpan"/> to the nearest hour.
        /// </summary>
        public static TimeSpan RoundToNearestHour(this TimeSpan ts) =>
            TimeSpan.FromHours(Math.Round(ts.TotalHours));

        /// <summary>
        /// Rounds the <see cref="TimeSpan"/> to the nearest day.
        /// </summary>
        public static TimeSpan RoundToNearestDay(this TimeSpan ts) =>
            TimeSpan.FromDays(Math.Round(ts.TotalDays));

        /// <summary>
        /// Calculates the percentage of this <see cref="TimeSpan"/> compared to another.
        /// </summary>
        public static double PercentageOf(this TimeSpan ts, TimeSpan other)
        {
            if (other == TimeSpan.Zero) return 0;
            return (ts.TotalMilliseconds / other.TotalMilliseconds) * 100;
        }

        /// <summary>
        /// Checks if the <see cref="TimeSpan"/> is between two values.
        /// </summary>
        public static bool Between(this TimeSpan ts, TimeSpan min, TimeSpan max)
        {
            return ts >= min && ts <= max;
        }

        /// <summary>
        /// Adds business days (skipping Saturday and Sunday).
        /// </summary>
        public static TimeSpan AddBusinessDays(this TimeSpan ts, int businessDays)
        {
            var date = DateTime.Now.Add(ts);
            int addedDays = 0;

            while (addedDays < businessDays)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    addedDays++;
            }

            return date - DateTime.Now;
        }

        /// <summary>
        /// Converts a <see cref="TimeSpan"/> to a short string (e.g., "2h 30m").
        /// </summary>
        public static string ToShortString(this TimeSpan ts)
        {
            if (ts == TimeSpan.Zero) return "0m";

            List<string> parts = new();
            if (ts.Days > 0) parts.Add($"{ts.Days}d");
            if (ts.Hours > 0) parts.Add($"{ts.Hours}h");
            if (ts.Minutes > 0) parts.Add($"{ts.Minutes}m");
            if (ts.Seconds > 0 && ts.Days == 0) parts.Add($"{ts.Seconds}s");

            return string.Join(" ", parts);
        }

        /// <summary>
        /// Multiplies the <see cref="TimeSpan"/> by a factor.
        /// </summary>
        public static TimeSpan Multiply(this TimeSpan ts, double factor)
        {
            return TimeSpan.FromTicks((long)(ts.Ticks * factor));
        }

        /// <summary>
        /// Divides the <see cref="TimeSpan"/> by a divisor.
        /// </summary>
        public static TimeSpan Divide(this TimeSpan ts, double divisor)
        {
            if (divisor == 0) throw new DivideByZeroException();
            return TimeSpan.FromTicks((long)(ts.Ticks / divisor));
        }

        /// <summary>
        /// Truncates to minutes (removes seconds and milliseconds).
        /// </summary>
        public static TimeSpan TruncateToMinutes(this TimeSpan ts)
        {
            return TimeSpan.FromMinutes(Math.Floor(ts.TotalMinutes));
        }

        /// <summary>
        /// Truncates to hours (removes minutes and below).
        /// </summary>
        public static TimeSpan TruncateToHours(this TimeSpan ts)
        {
            return TimeSpan.FromHours(Math.Floor(ts.TotalHours));
        }

        /// <summary>
        /// Truncates to days (removes hours and below).
        /// </summary>
        public static TimeSpan TruncateToDays(this TimeSpan ts)
        {
            return TimeSpan.FromDays(Math.Floor(ts.TotalDays));
        }

        /// <summary>
        /// Checks whether the <see cref="TimeSpan"/> is zero.
        /// </summary>
        public static bool IsZero(this TimeSpan ts) => ts == TimeSpan.Zero;

        /// <summary>
        /// Converts the <see cref="TimeSpan"/> into a dictionary of components (days, hours, minutes, seconds, milliseconds).
        /// </summary>
        public static Dictionary<string, int> ToDictionary(this TimeSpan ts)
        {
            return new Dictionary<string, int>
            {
                { "Days", ts.Days },
                { "Hours", ts.Hours },
                { "Minutes", ts.Minutes },
                { "Seconds", ts.Seconds },
                { "Milliseconds", ts.Milliseconds }
            };
        }

    /// <summary>
    /// Converts the <see cref="TimeSpan"/> into a dictionary of components (days, hours, minutes, seconds, milliseconds).
    /// </summary>
    public static Dictionary<string, int> ToDictionaryPersian(this TimeSpan ts)
    {
        return new Dictionary<string, int>
            {
                { "روز", ts.Days },
                { "ساعت", ts.Hours },
                { "دقیقه", ts.Minutes },
                { "ثانیه", ts.Seconds },
                { "میلی ثانیه", ts.Milliseconds }
            };
    }
}

