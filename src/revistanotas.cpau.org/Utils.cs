namespace CPAU.RevistaNotas
{
    public static class Utils
    {
        public static string SeName(string value)
        {
            value = value
                .Trim().ToLower()
                .Replace("%", string.Empty)
                .Replace("&", string.Empty)
                .Replace("\"", string.Empty)
                .Replace("?", string.Empty)
                .Replace("¿", string.Empty)
                .Replace("° ", string.Empty)
                .Replace("°", string.Empty)
                .Replace(".", string.Empty)
                .Replace("  ", "-")
                .Replace(' ', '-')
                .Replace(':', '-')
                .Replace('|', '-')
                .Replace('/', '-')
                .Replace('á', 'a')
                .Replace('é', 'e')
                .Replace('í', 'i')
                .Replace('ó', 'o')
                .Replace('ú', 'u')
                .Replace("--", "-")
                .Replace("--", "-");

            return value;
        }
    }
}
