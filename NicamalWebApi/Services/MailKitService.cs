using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace NicamalWebApi.Services
{
    public class MailKitService: IMailKitService
    {
        private readonly string _serverEmail = "nicamal.app@gmail.com";
        private readonly string _serverName = "Nicamal App";
        private readonly string _serverPassword = "nicamal_app2021";
        
        public async Task sendMail(string email, string name, string text, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_serverName, _serverEmail));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = subject;
            message.Body =new TextPart(MimeKit.Text.TextFormat.Html) { Text = CreateBody(name)};
            using (var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(_serverEmail, _serverPassword);
                await client.SendAsync(message);
                    
                client.Disconnect(true);
            }
        }

        private string CreateBody(string name)
        {
            string body = string.Empty;

            using (StreamReader SourceReader = System.IO.File.OpenText("./EmailTemplate.html"))
            {
                body = SourceReader.ReadToEnd();
            }

            body = body.Replace("{name}", name);

            return body;
        }
    }
}