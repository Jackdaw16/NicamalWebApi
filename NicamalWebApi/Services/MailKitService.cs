using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;

namespace NicamalWebApi.Services
{
    public class MailKitService: IMailKitService
    {
        private readonly string _serverEmail = "nicamal.app@gmail.com";
        private readonly string _serverName = "Nicamal App";
        private readonly string _serverPassword = "nicamal_app2021";
        private readonly IServiceProvider _serviceProvider;

        public MailKitService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async void SendMail(string email, string name, string text, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_serverName, _serverEmail));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = subject;
            message.Body = CreateBody(name);
            
            using (var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(_serverEmail, _serverPassword);
                await client.SendAsync(message);
                    
                client.Disconnect(true);
            }
        }

        private MimeEntity CreateBody(string name)
        {
            string HtmlFormat = string.Empty;
            var builder = new BodyBuilder();
            
            using (FileStream fileStream = new FileStream($"{Directory.GetCurrentDirectory()}/wwwroot/email/EmailTemplate.html", FileMode.Open))
            {
                using (StreamReader sourceReader = new StreamReader(fileStream, Encoding.GetEncoding("gbk")))
                {
                    HtmlFormat = sourceReader.ReadToEnd();
                }
            }

            var imagePath = $"{Directory.GetCurrentDirectory()}/wwwroot/email/header.png";
            var image = builder.LinkedResources.Add(imagePath);
            image.ContentId = MimeUtils.GenerateMessageId();
            HtmlFormat = HtmlFormat.Replace(Path.GetFileName(imagePath), string.Format("cid:{0}", image.ContentId));
            HtmlFormat = HtmlFormat.Replace("{name}", name);
            builder.HtmlBody = HtmlFormat;

            return builder.ToMessageBody();
        }
    }
}