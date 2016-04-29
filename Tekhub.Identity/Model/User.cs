using System.Collections.Generic;

namespace Tekhub.Identity.Model
{
    public class User
    {
        public User()
        {
            IsActive = true;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RegistrationStatus RegistrationStatus { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public bool IsFacebookUser { get; set; }
        public bool IsActive { get; set; }

        public virtual List<Permission> Permissions { get; set; }
    }
}
