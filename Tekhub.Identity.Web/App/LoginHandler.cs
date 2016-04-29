using System.Collections.Generic;
using System.Web.Security;
using Tekhub.Identity.Model.Dto;

namespace Tekhub.Identity.Web.App
{
    public class LoginHandler : ILoginHandler
    {
        private readonly ICollectionSessionManager<PermissionDto> _userPermissionsSessionManager;

        public LoginHandler(ICollectionSessionManager<PermissionDto> userPermissionsSessionManager)
        {
            _userPermissionsSessionManager = userPermissionsSessionManager;
        }

        public void Login(string email, List<PermissionDto> userPermissions)
        {
            FormsAuthentication.SetAuthCookie(email, false);
            _userPermissionsSessionManager.Clear();
            _userPermissionsSessionManager.AddRange(userPermissions);
        }

    }
}
