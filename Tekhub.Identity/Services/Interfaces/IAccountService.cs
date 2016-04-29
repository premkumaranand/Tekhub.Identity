using System.Collections.Generic;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;

namespace Tekhub.Identity.Services.Interfaces
{
    public interface IAccountService
    {
        void Activate(string linkKey);
        User RegisterUser(User user, int userTypeId);
        User RegisterFacebookUser(string userFbAuthToken, User user, int userTypeId);
        List<PermissionDto> LogIn(string email, string password);
        List<PermissionDto> LogInFacebookUser(string userFbAuthToken, string email, int userTypeId);
        void LogOut();
        bool ConfirmEmail(string email);
    }
}
