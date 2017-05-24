using System.Threading.Tasks;

namespace DartLeague.Web.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
