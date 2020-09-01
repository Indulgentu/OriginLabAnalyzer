using System;
using System.Globalization;

namespace OriginLabAnalyzer.Classes
{
    class Helper
    {
        public static double ParseDouble(String d)
        {
            return double.Parse(d, NumberFormatInfo.InvariantInfo);
        }
    }
}
