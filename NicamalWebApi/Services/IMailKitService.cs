using System.Threading.Tasks;

namespace NicamalWebApi.Services
{
    public interface IMailKitService
    {
        void SendMail(string email, string name, string text, string subject);
    }
}