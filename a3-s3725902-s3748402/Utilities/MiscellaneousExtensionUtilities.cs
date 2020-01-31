///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///   Adapted from Tute lab and modified to suit the requirement
///-----------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace NwbaSystem.Utilities
{
    public static class MiscellaneousExtensionUtilities
    {
        public static bool HasMoreThanNDecimalPlaces(this decimal value, int n) => decimal.Round(value, n) != value;
        public static bool HasMoreThanTwoDecimalPlaces(this decimal value) => value.HasMoreThanNDecimalPlaces(2);

        public static bool IsValidPhone(this string phone)
        {
            Regex regex = new Regex(@"^[(61)]+(-)+\d{10}$");
            return regex.IsMatch(phone);
        }

        public static bool IsAllLetters(this string value)
        {
            foreach (char c in value)
            {
                if ((!Char.IsLetter(c)) && (!Char.IsWhiteSpace(c)))
                {
                    return false;
                }
                else if (Char.IsNumber(c))
                {
                    return false;
                }

            }
            return true;
        }

        public static bool IsAllDigits(this string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

    }
}
