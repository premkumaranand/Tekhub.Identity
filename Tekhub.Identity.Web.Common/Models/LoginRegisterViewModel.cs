using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekhub.Identity.Web.Common.Models
{
    public class LoginRegisterViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string RedirectUrl { get; set; }

        public bool IsLogin { get; set; }

        public bool IsRegister { get; set; }
    }
}
