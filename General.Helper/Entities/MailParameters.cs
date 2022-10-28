using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities.Helper
{
    public class MailParameters
    {
        public string Header { get; set; }
        public string Body { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public string Password { get; set; }
        public string Host { get; set; } = "smtp.yandex.com.tr";
        public int Port { get; set; } = 587;


    }
}
