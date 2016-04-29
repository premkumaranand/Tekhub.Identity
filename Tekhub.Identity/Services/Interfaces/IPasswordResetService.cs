using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekhub.Identity.Model;

namespace Tekhub.Identity.Services.Interfaces
{
    public interface IPasswordResetService
    {
        PasswordReset CreatePasswordResetForUser(long userId, int passwordExpireInDays);
        PasswordReset GetPasswordLink(string activateLinkKey);
        bool IsKeyValid(PasswordReset passwordLink);
        void UpdateLinkActivatedDate(PasswordReset passwordLink);
    }
}
