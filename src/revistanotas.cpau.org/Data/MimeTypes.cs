namespace CPAU.RevistaNotas.Data
{
    public enum MimeTypes
    {
        Png,
        Jpg,
        Gif
    }

    public static class MimeTypeUtils
    {
        public static string ToString(byte mimeType)
        {
            switch (mimeType)
            {
                case 0:
                    return "png";
                case 1:
                    return "jpg";
                case 2:
                    return "gif";
                default:
                    return "png";
            }
        }

        public static byte ToByte(string mimeType)
        {
            switch (mimeType)
            {
                case "png":
                    return 0;
                case "jpg":
                    return 1;
                case "gif":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
