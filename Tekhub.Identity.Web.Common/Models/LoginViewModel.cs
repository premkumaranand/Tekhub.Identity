namespace Tekhub.Identity.Web.Common.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string RedirectUrl { get; set; }
    }
}
