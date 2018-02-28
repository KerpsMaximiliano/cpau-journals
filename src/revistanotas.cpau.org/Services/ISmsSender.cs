using System.Threading.Tasks;

namespace CPAU.RevistaNotas.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
