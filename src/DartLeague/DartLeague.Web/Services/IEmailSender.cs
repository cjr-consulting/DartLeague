using System.Threading.Tasks;

namespace DartLeague.Web.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
