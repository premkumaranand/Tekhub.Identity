using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekhub.Identity.Model;
using Tekhub.Identity.Services.Interfaces;
using Tekhub.Identity.Repositories.Interfaces;

namespace Tekhub.Identity.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IPasswordResetRepository _passwordResetRepository;

        public PasswordResetService(IPasswordResetRepository passwordResetRepository)
        {
            _passwordResetRepository = passwordResetRepository;
        }

        public PasswordReset CreatePasswordResetForUser(long userId, int passwordExpireInDays)
        {
            var pwdResetEntry = new PasswordReset
            {
                UserId = userId,
                ExpireOn = DateTime.Now.AddDays(passwordExpireInDays),
                Key = Guid.NewGuid().ToString("N"),
                RequestedOn = DateTime.Now
            };

            _passwordResetRepository.Insert(pwdResetEntry);

            return pwdResetEntry;
        }

        public PasswordReset GetPasswordLink(string activateLinkKey)
        {
            return _passwordResetRepository.GetPasswordLinkByKey(activateLinkKey);
        }

        public bool IsKeyValid(PasswordReset passwordLink)
        {
            return (passwordLink != null && passwordLink.ExpireOn > DateTime.UtcNow &&
                    !passwordLink.ActivatedOn.HasValue);
        }

        public void UpdateLinkActivatedDate(PasswordReset passwordLink)
        {
            passwordLink.ActivatedOn = DateTime.UtcNow;

            _passwordResetRepository.Update(passwordLink);
        }
    }
}
