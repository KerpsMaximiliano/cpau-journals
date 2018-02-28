using System.Threading.Tasks;

namespace CPAU.RevistaNotas.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
