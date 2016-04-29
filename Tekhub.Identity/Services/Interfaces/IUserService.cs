using System.Collections.Generic;
using Tekhub.Identity.Model;

namespace Tekhub.Identity.Services.Interfaces
{
    public interface IUserService
    {
        User GetUser(long userId);
        User GetUser(string email);
        bool IsGuest(User user);
        void UpdateRegistrationStatus(long userId, RegistrationStatus registrationStatus);
        bool UserExists(string email);

        void Update(long userId, string name = null, string email = null, string password = null, 
            RegistrationStatus? registrationStatus = null, UserType userType = null, string phoneNumber = null);

        List<User> GetUsers(int userTypeId);
        void DeactivateUser(int userId);
        void ActivateUser(int userId);
    }
}
