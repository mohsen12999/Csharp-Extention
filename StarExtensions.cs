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
        
        // using Font-Awesome-5
        public static HtmlString StarScore(this int score, int totalStar = 5)
        {
            var st = "";
            int i;
            var stars = score.StartCount(star: totalStar);
            for (i = 0; i < stars.Item1; i++)
            {
                st += "<i class=\"fas fa-star\"></i>";
            }
            for (i = 0; i < stars.Item2; i++)
            {
                st += "<i class=\"fas fa-star-half-alt\"></i>";
            }
            for (i = 0; i < stars.Item3; i++)
            {
                st += "<i class=\"far fa-star\"></i>";
            }
            return new HtmlString(st);
        }
    }
}
