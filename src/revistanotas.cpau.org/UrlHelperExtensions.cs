using CPAU.RevistaNotas.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CPAU.RevistaNotas
{
    public static class UrlHelperExtensions
    {
        private static IOptions<RevistaNotasConfiguration> _configuration;
        public static string Media(this IUrlHelper urlHelper, string path)
        {
            if (_configuration == null)
            {
                _configuration = urlHelper.ActionContext.HttpContext.RequestServices.GetService(typeof(IOptions<RevistaNotasConfiguration>)) as IOptions<RevistaNotasConfiguration>;
            }
            if (_configuration == null)
            {
                return null;
            }
            
            var mediaBaseUrl = _configuration.Value.MediaBaseUrl.TrimEnd('/');
            return !string.IsNullOrEmpty(path) ? $"{mediaBaseUrl}/{path.TrimStart('/')}" : $"{mediaBaseUrl}/";
        }
    }
}
