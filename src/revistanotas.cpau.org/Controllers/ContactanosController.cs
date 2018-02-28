using System;
using System.IO;
using CPAU.RevistaNotas.Configuration;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CPAU.RevistaNotas.Controllers
{
    [Route("[controller]")]
    public class ContactanosController : Controller
    {
        private readonly IHostingEnvironment environment;
        private readonly SmtpConfiguration smtpConfiguration;

        public ContactanosController(
            IHostingEnvironment environment,
            Data.ApplicationDbContext context,
            IOptions<SmtpConfiguration> smtpConfiguration)
        {
            this.environment = environment;
            this.smtpConfiguration = smtpConfiguration.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new Models.ContactanosViewModel.IndexViewModel();
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
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtpConfiguration.FromName, smtpConfiguration.FromAddress));
                message.To.Add(new MailboxAddress(smtpConfiguration.ToName, smtpConfiguration.ToAddress));
                message.Cc.Add(new MailboxAddress(smtpConfiguration.CcAddress));
                message.Subject = "Contacto desde Revista Notas";

                var templatePath = Path.Combine(environment.WebRootPath, "assets", "contactanos_template.html");
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
                    client.Connect(smtpConfiguration.Host, 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(smtpConfiguration.UserName, smtpConfiguration.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }

            model.MessageSent = true;
            return View(model);
        }
    }
}