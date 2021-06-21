using System.Threading.Tasks;

namespace NicamalWebApi.Services
{
    public interface IMailKitService
    {
        Task sendMail(string email, string name, string text, string subject);
    }
}