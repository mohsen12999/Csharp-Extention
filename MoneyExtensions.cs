
namespace Data.Utilities
{
    public static class MoneyExtensions
    {
        public static string TomanValue(this decimal money, bool simple = true)
        {
            if (simple || money < 1000)
                return money.ToString("N0") + " تومان";
            if (money < 1000000)
                return (money / 1000).ToString("N3") + " هزار تومان";
            return (money / 1000000).ToString("N3") + " میلیون تومان";
        }

    }
}
