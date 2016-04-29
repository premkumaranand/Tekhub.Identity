using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tekhub.Identity.Model.Dto;
using Tekhub.Identity.Web.App;

namespace Tekhub.Identity.Web.Filters
{
    public class AuthorizeUser : AuthorizeAttribute
    {
        private PermissionDto _userPermission;
        private readonly string _actionType;

        public AuthorizeUser(string actionType = null)
        {
            _actionType = actionType;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var routeData = filterContext.RouteData;
            if (filterContext.HttpContext.Session != null)
            {
                var userPermissionsSessionManager = new UserPermissionsSessionManager(filterContext.HttpContext.Session);
                var loggedInUserPermissions = userPermissionsSessionManager.GetAll();

                if (!(filterContext.HttpContext.User.Identity).IsAuthenticated)
                {
                    loggedInUserPermissions = null;
                }


                if (loggedInUserPermissions == null)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                    base.OnAuthorization(filterContext);
                    return;
                }

                _userPermission = new PermissionDto
                {
                    Domain = Convert.ToString(routeData.Values["domain"]),
                    Controller = routeData.GetRequiredString("controller"),
                    Action = routeData.GetRequiredString("action"),
                    Area = Convert.ToString(routeData.DataTokens["area"]),
                    ActionType = _actionType
                };

                if (string.IsNullOrEmpty(_userPermission.Area))
                {
                    _userPermission.Area = "Root";
                }

                if (!IsAllowed(loggedInUserPermissions))
                {
                    throw new UnauthorizedAccessException();
                }
            }

            base.OnAuthorization(filterContext);
        }

        private bool IsAllowed(IList<PermissionDto> loggedInUserPermissions)
        {
            if (loggedInUserPermissions == null)
            {
                return false;
            }

            return loggedInUserPermissions.Any(uc => uc.Equals(this._userPermission));
        }
    }
}
