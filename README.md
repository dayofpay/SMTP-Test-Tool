### Features

- Get Full Detailed Error Logs
- Connect to SMTP with arguments
-  Open Source
- API
- SOON MORE
* **Version 1.0**

### Documentation

#### Instalation

`$ git clone https://github.com/dayofpay/SMTP-Test-Tool`

#### Parse arguments ( Source )　

```javascript
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPTest
{
    class Program
    {

        // Developed by dayofpay 
        // Version : 1.0
        // URL: https://github.com/dayofpay
        // License: Free to use
        public static void Main(string[] args)
        {

        Restart:
            Console.Title = "SMTP Test Tool | https://github.com/dayofpay";
            Console.Clear();
            if (args.Length > 0 && !args.Contains("-startManualy")) // Checks if the startup arguments are greater than 0 and we make sure that there is no -startManualy argument
            {
                List<string> realArg = new List<string>();
                string finalR = "";
                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];
                    Console.Write("args index ");
                    Console.Write(i); // Write index
                    Console.Write(" is [");
                    Console.Write(argument); // Write string
                    Console.WriteLine("]");
                    realArg.Add(args[i]);
                }
                foreach (string x in realArg)
                {
                    finalR += x + " ";
                }
                // Login Username
                API.Details.smtpLoginUsername = args[1];
                // End Login Username
                // SMTP Host
                API.Details.smtpServer = args[3];
                // END SMTP Host
                // SMTP Pass
                API.Details.smtpPass = args[5];
                // END SMTP PASS
                // SMTP PORT
                API.Details.smtpPort = int.Parse(args[7]);
                // END SMTP PORT
                // SMTP SSL
                API.Details.smtpUseSSL = bool.Parse(args[9]);

                // END SMTP SSL
                //Subject 


                API.Details.mailSubject = finalR.GetSubject();
                //API.Details.mailSubject = args[11];
                //Body
                API.Details.mailBody = finalR.GetBody();
                //
                API.Details.mailTo = finalR.GetReceiver();
                Console.WriteLine($"[INFO-SMTP Host] {API.Details.smtpServer}");
                Console.WriteLine($"[INFO-SMTP Pass] {API.Details.smtpPass}");
                Console.WriteLine($"[INFO-SMTP Login Username] {API.Details.smtpLoginUsername}");
                Console.WriteLine($"[INFO-SMTP Port] {API.Details.smtpPort}");
                Console.WriteLine($"[INFO-SMTP SSL] {API.Details.smtpUseSSL}");
                Console.WriteLine($"[INFO-Mail Subject] {API.Details.mailSubject}");
                Console.WriteLine($"[INFO-Mail Body] {API.Details.mailBody}");
                Console.WriteLine($"[INFO-Mail Receiver] {API.Details.mailTo}");
                Console.WriteLine(finalR);
                API.Functions.Tools.SendMail();
            }
            else if (args.Contains("-startManualy")) // If there is set argument to setup the smtp details manualy
            {
            StartManual:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("SMTP Host : ");
                var smtpHost = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("SMTP Password : ");
                var smtpPass = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("SMTP Mail: ");
                var smtpMail = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("SMTP Port: ");
                var smtpPort = int.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("SMTP Use SSL (true/false) : ");
                var smtpSSL = bool.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Mail Receiver: ");
                var mailReceiver = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Mail Subject: ");
                var mailSubject = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Message: ");
                var mailMessage = Console.ReadLine();
                Console.Clear();
                try
                {
                    API.Details.smtpLoginUsername = smtpMail;
                    API.Details.smtpServer = smtpHost;
                    API.Details.smtpPass = smtpPass;
                    API.Details.smtpPort = smtpPort;
                    API.Details.smtpUseSSL = smtpSSL;
                    API.Details.mailSubject = mailSubject;
                    API.Details.mailBody = mailMessage;
                    API.Details.mailTo = mailReceiver;
                }
                catch (Exception error)
                {
                    API.Details.errors.Add(error.Message);
                    API.Functions.Tools.CreateError($"Error : \r\n  {error.Message}");
                    API.Functions.Tools.CreateDelay(4);
                    goto Restart;
                }
            ConfirmationR:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO-SMTP Host] {API.Details.smtpServer}");
                Console.WriteLine($"[INFO-SMTP Pass] {API.Details.smtpPass}");
                Console.WriteLine($"[INFO-SMTP Login Username] {API.Details.smtpLoginUsername}");
                Console.WriteLine($"[INFO-SMTP Port] {API.Details.smtpPort}");
                Console.WriteLine($"[INFO-SMTP SSL] {API.Details.smtpUseSSL}");
                Console.WriteLine($"[INFO-Mail Subject] {API.Details.mailSubject}");
                Console.WriteLine($"[INFO-Mail Body] {API.Details.mailBody}");
                Console.WriteLine($"[INFO-Mail Receiver] {API.Details.mailTo}");
                Console.WriteLine("[MESSAGE] Is everything right ? : yes/no");
                var confirmation = Console.ReadLine().ToLower();
                if (confirmation == "yes")
                {
                    API.Functions.Tools.CreateSuccess("Settings Saved ! Sending you to the main menu ...");
                    API.Functions.Tools.CreateDelay(2);
                    API.MainMenu.Launch();
                }
                if (confirmation == "no")
                {
                    goto Restart;
                }
                if (!confirmation.Contains(API.Details.validAnswers.ToString()))
                {
                    API.Functions.Tools.CreateError("Invalid Option ... Sending Confirmation Request Again ...");
                    API.Functions.Tools.CreateDelay(2);
                    goto ConfirmationR;
                }
            }
        }
    }
    static class ArgsSMTPAPI
    {
        public static string GetSubject(this string text, string stopAt = "--message", string startAt = "--subject")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                return text.Substring(text.IndexOf(startAt) + startAt.Length,text.IndexOf(stopAt) - text.IndexOf(startAt) - startAt.Length);
            }

            return String.Empty;
            // SMTPTest.exe --loginusername admin@v-devs.online --address smtp.v-devs.online --smtppass <myCoolPassword> --port 587 --ssl true --subject Hey,how are you doing ? --message Hey,how are you ? --receiver test@v-devs.online --end
        }
        public static string GetBody(this string text, string stopAt = "--receiver", string startAt = "--message")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                return text.Substring(text.IndexOf(startAt) + startAt.Length,text.IndexOf(stopAt)-text.IndexOf(startAt)-startAt.Length);
            }

            return String.Empty;
            // SMTPTest.exe --loginusername admin@v-devs.online --address smtp.v-devs.online --smtppass <myCoolPassword> --port 587 --ssl true --subject Hey,how are you doing ? --message Hey,how are you ? --receiver test@v-devs.online   --end     
        }
        public static string GetReceiver(this string text, string stopAt = "--end", string startAt = "--receiver")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                return text.Substring(text.IndexOf(startAt) + startAt.Length, text.IndexOf(stopAt) - text.IndexOf(startAt) - startAt.Length);
            }

            return String.Empty;
            // SMTPTest.exe --loginusername admin@v-devs.online --address smtp.v-devs.online --smtppass <myCoolPassword> --port 587 --ssl true --subject Hey,how are you doing ? --message Hey,how are you ? --receiver test@v-devs.online --end
        }
    }
}
```




### Arguments

| Argument Name  | Argument Required Info  | Argument Required |
| :------------ |:---------------:| -----:|
| --loginusername     | Your SMTP E-Mail| ✅ |
| --address      | Your SMTP Address        |   ✅ |
| --smtppass | Your SMTP Password        |    ✅ |
| --port | Your SMTP Port        |    ✅ |
| --ssl | Are you using SSL Connection ? true/false         |    ✅ |
| --subject | E-Mail SUBJECT         |    ✅ |
| --message | E-Mail Message         |    ✅ |
| --receiver | E-Mail Receiver         |    ✅ |
| --end | Final argument         |    ✅ |
`Example:             // SMTPTest.exe --loginusername yourcoolusername@domain.com --address smtp.myserver.com --smtppass myCoolpassword --port 587 --ssl true --subject Hey,how are you doing ? --message Hey,adsadsa asd adsa how are you ? --receiver admin@gmail.com --end`
                
----


### If you have questions feel free to contact me :)

### Developed by V-DEVS Bulgaria : https://v-devs.online
