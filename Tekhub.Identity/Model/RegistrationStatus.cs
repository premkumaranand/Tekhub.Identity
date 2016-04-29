namespace Tekhub.Identity.Model
{
    public enum RegistrationStatus
    {
        EmailNotVerified = 1,
        EmailVerified = 2,
        EmailVerificationExpired = 3,
        Guest = 4
    }
}
