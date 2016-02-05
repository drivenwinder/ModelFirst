using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using EAP.Logging;

namespace EAP.Utils
{
    public class MailHelper
    {
        public static void Send(string subject, string toAddress, string content)
        {
            Send(subject, toAddress, content, (List<string>)null);
        }

        public static void Send(string subject, string toAddress, string content, string attachment)
        {
            Send(subject, toAddress, content, new List<string>(attachment.Split(';', '；')));
        }

        public static void Send(string subject, string toAddress, string content, List<string> attachment)
        {
            SmtpClient client = new SmtpClient();

            MailAddress from = new MailAddress("no-reply@EAP.com", "EAP", System.Text.Encoding.UTF8);
            MailMessage message = new MailMessage();

            message.From = from;
            string[] to = toAddress.Split(';', '；', ',');
            foreach (string s in to)
            {
                if (s.IsNotEmpty() && s.IndexOf('@') > 0)
                {
                    message.To.Add(s);
                }
            }
            //Log.Info("Mail Subject:" + subject + " \r\nMail to:" + toAddress);

            message.Body = content;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Priority = MailPriority.High;

            if (attachment != null && attachment.Count > 0)
            {
                foreach (string file in attachment)
                {
                    if (File.Exists(file))
                    {
                        Attachment a = new Attachment(file);
                        message.Attachments.Add(a);
                    }
                }
            }

            client.Send(message);
        }
    }
}
