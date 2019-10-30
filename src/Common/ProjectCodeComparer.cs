using System;
using System.Collections.Generic;

namespace Jpp.Common
{
    public class ProjectCodeComparer : IComparer<string>
    {
        /// <summary>
        /// Compare to sort project codes
        /// </summary>
        /// <param name="x">First Project Code</param>
        /// <param name="y">Second Project Code</param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            x = TrimEndChars(x);
            y = TrimEndChars(y);

            bool xIsNumeric = IsNumeric(x);
            bool yIsNumeric = IsNumeric(y);

            if (xIsNumeric && yIsNumeric)
            {
                if (Convert.ToInt32(x) > Convert.ToInt32(y)) return 1;
                if (Convert.ToInt32(x) < Convert.ToInt32(y)) return -1;
                if (Convert.ToInt32(x) == Convert.ToInt32(y)) return 0;
            }

            if (xIsNumeric && !yIsNumeric) return -1;
            if (!xIsNumeric && yIsNumeric) return 1;

            return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }

        private static string TrimEndChars(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            string newValue = value.Replace(" ", "");
            newValue = newValue.Replace("ENQ", "");
            if (!char.IsDigit(newValue[newValue.Length - 1]) && char.IsDigit(newValue[newValue.Length - 2]))
            {
                return newValue.Substring(0, newValue.Length - 1);
            }

            return value;
        }

        private static bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
