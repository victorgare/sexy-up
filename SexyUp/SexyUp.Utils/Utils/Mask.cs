namespace SexyUp.Utils.Utils
{
    public static class Mask
    {
        public static string RemoveMask(string value)
        {
            return value?.Trim()
                .Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Replace("-", string.Empty)
                .Replace("/", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty);
        }
    }
}
