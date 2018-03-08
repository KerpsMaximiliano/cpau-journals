using System;
using System.IO;
using CPAU.RevistaNotas.Configuration;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CPAU.RevistaNotas.Controllers
{
    [Route("[controller]")]
    public class ContactanosController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _environment;
        private readonly SmtpConfiguration _smtpConfiguration;
        private readonly GoogleReCaptchaConfiguration _googleReCaptchaConfiguration;

        public ContactanosController(
            ILogger<ContactanosController> logger,
            IHostingEnvironment environment,
            Data.ApplicationDbContext context,
            IOptions<SmtpConfiguration> smtpConfiguration,
            IOptions<GoogleReCaptchaConfiguration> googleReCaptchaConfiguration)
        {
            _logger = logger;
            _environment = environment;
            _smtpConfiguration = smtpConfiguration.Value;
            _googleReCaptchaConfiguration = googleReCaptchaConfiguration.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new Models.ContactanosViewModel.IndexViewModel();
            model.RecaptchaSiteKey = _googleReCaptchaConfiguration.SiteKey;
            /*
            model.NombresYApellidos = "Pablo Cejas";
            model.CorreoElectronico = "pcejas@gmail.com";
            model.Mensaje = "Test";
            model.Telefono = "+5491158022424";
            */
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Models.ContactanosViewModel.IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Utils.ReCaptchaPassed(
                        Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
                        _googleReCaptchaConfiguration.SecretKey,
                        _logger
                ))
                {

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(_smtpConfiguration.FromName, _smtpConfiguration.FromAddress));
                    message.To.Add(new MailboxAddress(_smtpConfiguration.ToName, _smtpConfiguration.ToAddress));
                    message.Cc.Add(new MailboxAddress(_smtpConfiguration.CcAddress));
                    message.Subject = "Contacto desde Revista Notas";

                    var templatePath = Path.Combine(_environment.WebRootPath, "assets", "contactanos_template.html");
                    var body = System.IO.File.ReadAllText(templatePath);

                    if (string.IsNullOrEmpty(body))
                    {
                        throw new Exception("Contactanos template not found");
                    }

                    body = body.Replace("{$host}", Request.Host.Value);
                    body = body.Replace("{$name}", model.NombresYApellidos);
                    body = body.Replace("{$email}", model.CorreoElectronico);
                    body = body.Replace("{$telephone}", model.Telefono);
                    body = body.Replace("{$message}", model.Mensaje);

                    message.Body = new TextPart("html")
                    {
                        Text = body
                    };

                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect(_smtpConfiguration.Host, 587, false);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(_smtpConfiguration.UserName, _smtpConfiguration.Password);
                        client.Send(message);
                        client.Disconnect(true);
                    }

                    model.MessageSent = true;
                    return View(model);
                }
            }

            model.MessageSent = false;
            model.RecaptchaSiteKey = _googleReCaptchaConfiguration.SiteKey;
            return View(model);
        }
    }
}