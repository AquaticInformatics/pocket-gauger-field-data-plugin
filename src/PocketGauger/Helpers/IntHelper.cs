using System.Globalization;

namespace PocketGauger.Helpers
{
    public static class IntHelper
    {
        public static int? Parse(string value)
        {
            int result;

            return int.TryParse(value, out result) ? result : default(int?);
        }

        public static string Serialize(int? value)
        {
            return value?.ToString(NumberFormatInfo.InvariantInfo);
        }
    }
}
