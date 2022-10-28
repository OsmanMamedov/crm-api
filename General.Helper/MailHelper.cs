using General.Core.Entities.Concrete;
using General.Entities;
using General.Entities.Helper;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace General.Api.Helper
{
    public static class MailHelper
    {

        public static ResponseService Send(IHostingEnvironment environment, MailParameters mail, User user)
        {
            ResponseService data = new ResponseService();
            try
            {
                var builder = new StringBuilder();
                var merhab = environment.ContentRootPath;
                using (var reader = System.IO.File.OpenText(environment.WebRootPath + "\\email\\email.html"))
                {
                    builder.Append(reader.ReadToEnd());
                }

                builder.Replace("{{header}}", mail.Header);
                builder.Replace("{{body}}", mail.Body);

                var fromAddress = new MailAddress(mail.From);
                var toAddress = new MailAddress(mail.To);
                using (var smtp = new SmtpClient
                {
                    Host = mail.Host,
                    Port = mail.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, mail.Password)
                })
                {
                    using var messages = new MailMessage(fromAddress, toAddress) { Subject = mail.Header, Body = builder.ToString() };
                    messages.IsBodyHtml = true;
                    smtp.Send(messages);
                }
                data.Success = true;
                return data;

            }
            catch (SmtpException smex)
            {
                data.Success = false;
                data.Message = smex.Message;
                return data;
            }
        }
    }
}
