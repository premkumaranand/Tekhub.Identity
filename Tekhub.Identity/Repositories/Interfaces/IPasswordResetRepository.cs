using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekhub.Identity.Model;

namespace Tekhub.Identity.Repositories.Interfaces
{
    public interface IPasswordResetRepository
    {
        PasswordReset GetUserPasswordLinkByKey(long userId, string key);
        PasswordReset GetPasswordLinkByKey(string key);
        void Insert(PasswordReset pwdReset);
        void Update(PasswordReset pwdReset);
    }
}
