using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;

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

        // A function that checks reCAPTCHA results
        // You might want to move it to some common class
        public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret, ILogger logger)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError("Error while sending request to ReCaptcha");
                return false;
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }
    }
}
