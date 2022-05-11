using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SMTPTest.API
{
    class MainMenu
    {
        public static void Launch()
        {
        Restart:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[1] Send Mail");
            Console.WriteLine("[2] Check Settings");
            Console.WriteLine("[3] Check Last Error");
            Console.WriteLine("> ");
            var option = int.Parse(Console.ReadLine());
            try
            {
                if(option == 1)
                {
                    Functions.Tools.SendMail();
                }
                if(option == 2)
                {
                    Console.WriteLine($"[INFO-SMTP Host] {API.Details.smtpServer}");
                    Console.WriteLine($"[INFO-SMTP Pass] {API.Details.smtpPass}");
                    Console.WriteLine($"[INFO-SMTP Login Username] {API.Details.smtpLoginUsername}");
                    Console.WriteLine($"[INFO-SMTP Port] {API.Details.smtpPort}");
                    Console.WriteLine($"[INFO-SMTP SSL] {API.Details.smtpUseSSL}");
                    Console.WriteLine($"[INFO-Mail Subject] {API.Details.mailSubject}");
                    Console.WriteLine($"[INFO-Mail Body] {API.Details.mailBody}");
                    Console.WriteLine($"[INFO-Mail Receiver] {API.Details.mailTo}");
                    Functions.Tools.CreateDelay(10);
                    goto Restart;
                }
                if(option == 3)
                {
                    if (API.Details.errors.Count < 1 || API.Details.errors == null)
                    {
                        API.Functions.Tools.CreateSuccess("NO ERRORS FOUND !");
                    }
                    else
                    {
                        foreach (string vError in API.Details.errors)
                        {
                            API.Functions.Tools.CreateError(vError);
                        }
                    }
                    API.Functions.Tools.CreateDelay(5);
                    goto Restart;
                }
            }
            catch(Exception error)
            {
                API.Details.errors.Add(error.Message);
                API.Functions.Tools.CreateError("We ran into error ... \r\n" + error);
                API.Functions.Tools.CreateDelay(3);
                goto Restart;
            }
        }
    }
}
