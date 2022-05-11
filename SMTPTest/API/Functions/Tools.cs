using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
namespace SMTPTest.API.Functions
{
    class Tools
    {
        public static void CreateError(string message) // Create Error
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] > {message}");
        }
        public static void CreateWarn(string message) // Create Warn
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN] > {message}");
        }
        public static void CreateSuccess(string message) // Create Success
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCCESS] > {message}");
        }
        public static void CreateDelay(int seconds) // Create Delay
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }
        public static void SendMail() // Main Method for sending mails
        {
            try
            {
                MailMessage mail = new MailMessage(); // Creating instance
                SmtpClient SmtpServer = new SmtpClient(Details.smtpServer); // Initializes a new instance of the SMTPClient that sends E-Mails by using the specified SMTP server in Details Class

                mail.From = new MailAddress(Details.smtpLoginUsername); // SMTP Login Username
                mail.To.Add(Details.mailTo); // Mail Receiver
                mail.Subject = Details.mailSubject; // Mail Subject
                mail.Body = Details.mailBody; // Mail Body

                SmtpServer.Port = Details.smtpPort; // SMTP Server Port
                SmtpServer.Credentials = new System.Net.NetworkCredential(Details.smtpLoginUsername, Details.smtpPass); // SMTP Server Credentials to authenticate
                SmtpServer.EnableSsl = Details.smtpUseSSL;

                SmtpServer.Send(mail);
                CreateSuccess($"Message {mail.Body} send to {Details.mailTo} ...");
                CreateSuccess($"Sending you to start menu in 3 seconds ...");
                CreateDelay(3);
                String[] manual =  new string[] { "-startManualy" };
                Program.Main(manual);
            }
            catch(SmtpException smtpError)
            {
                CreateError($"Error sending message \r\n {smtpError.Message}");
                API.Details.errors.Add(smtpError.Message);
            }
        }

    }
}
