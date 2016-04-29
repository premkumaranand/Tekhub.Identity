namespace Tekhub.Identity.Services.Interfaces
{
    public interface IPasswordService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activateLinkKey"></param>
        /// <returns>UserId for the link</returns>
        long ActivatePassword(string activateLinkKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The reset link key</returns>
        string RequestReset(string email, int passwordExpireInDays);
        void UpdatePassword(string newPassword, string passwordResetEntryKey);
    }
}
