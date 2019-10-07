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

            bool xIsNumeric = int.TryParse(x, out var xInt);
            bool yIsNumeric = int.TryParse(y, out var yInt);

            int returnVal;

            if (xIsNumeric && yIsNumeric)
            {
                if (xInt > yInt) returnVal = 1;
                else if (xInt < yInt) returnVal = - 1;
                else returnVal = 0;  //xInt == yInt
            }
            else if (xIsNumeric) returnVal = -1; //assumed y is not numeric
            else if (yIsNumeric) returnVal = 1; //assumed x is not numeric
            else returnVal = string.Compare(x, y, StringComparison.OrdinalIgnoreCase);

            return returnVal;
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
    }
}
