using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Transport.User
{
    public class ChangePasswordEntity
    {
        public int USER_ID { get; set; }
        public string OLD_PASSWORD { get; set; }
        public string NEW_PASSWORD { get; set; }
    }
}
