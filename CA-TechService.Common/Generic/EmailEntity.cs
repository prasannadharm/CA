using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Generic
{
    public class EmailEntity
    {
        public EmailEntity()
        {
            Port = 587;
            Host = "smtp.gmail.com";
            EnableSsl = true;
        }
        public int Port { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool EnableSsl { get; set; }
    }
}
