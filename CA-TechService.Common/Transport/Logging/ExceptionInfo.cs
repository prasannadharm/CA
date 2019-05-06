using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Transport.Logging
{
   public class ExceptionInfo
    {
        public string ApplicationName { get; set; }
        public string ProgrammeName { get; set; }
        public string MachineName { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionSource { get; set; }
        public string CustomMessage { get; set; }

    }
}
