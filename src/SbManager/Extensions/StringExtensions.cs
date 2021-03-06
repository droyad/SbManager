﻿namespace SbManager.Extensions
{
    public static class StringExtensions
    {
        public static string UnescapePathName(this string pathname)
        {
            return pathname.Replace("_", "/");
        }

        public static string RemoveDeadLetterPath(this string pathname)
        {
            return pathname.Replace("/$DeadLetterQueue", "");
        }

        public static string MakeDeadLetterPath(this string pathname)
        {
            return IsDeadLetterPath(pathname) ? pathname : pathname + "/$DeadLetterQueue";
        }

        public static bool IsDeadLetterPath(this string pathname)
        {
            return pathname.Contains("$DeadLetterQueue");
        }

    }

    public static class To
    {
        public static string String(object o)
        {
            if (o == null) return null;
            return o.ToString();
        }
        public static int Int(object o)
        {
            return int.Parse(o.ToString());
        }
    }
}
