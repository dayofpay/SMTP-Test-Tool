using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPTest.API
{
    class Details
    {
        public static List<string> errors = new List<string>();
        public static String[] validAnswers = { "yes","no"};
        String[] defaultArgs = new String[] { "-startManualy" };
        public static string smtpServer { get; set; }
        public static string mailFrom { get; set; }
        public static string mailTo { get; set; }
        public static string mailSubject { get; set; }
        public static string mailBody { get; set; }
        public static int smtpPort { get; set; }
        public static string smtpLoginUsername { get; set; }
        public static bool smtpUseSSL { get; set; }
        public static string smtpPass { get; set; }
    }
}
