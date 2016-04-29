using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekhub.Identity.Model
{
    public class PasswordReset
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string RequestedIp { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime? ActivatedOn { get; set; }
        public DateTime ExpireOn { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}
