using System;

namespace Data.Utilities
{
    public static class StarExtensions
    {
        // full star - half star - empty star
        public static Tuple<int, int, int> StartCount(this int number, int max = 10, int star = 5)
        {
            var numberPercent = (double)number / max;
            var full = Convert.ToInt32(numberPercent * star);
            var empty = Convert.ToInt32((1 - numberPercent) * star);
            var half = star - (full + empty);
            return new Tuple<int, int, int>(full,half,empty);
        }
        
    }
}
