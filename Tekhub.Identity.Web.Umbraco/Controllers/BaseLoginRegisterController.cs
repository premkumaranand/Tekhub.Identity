using System;
using System.Web.Mvc;
using Tekhub.Identity.Model;
using Tekhub.Identity.Services.Interfaces;
using Tekhub.Identity.Web.Common.App;
using Umbraco.Web.Mvc;

namespace Tekhub.Identity.Web.Umbraco.Controllers
{
    public class BaseLoginRegisterController : SurfaceController
    {
        protected IAccountService AccountService { get; set; }

        protected BaseLoginRegisterController()
        {
            
        }

        protected BaseLoginRegisterController(IAccountService accountService)
        {
            AccountService = accountService;
        }

        /// <summary>
        /// Handles custom app login
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <param name="redirectUrl">Url from which the user tried to login</param>
        /// <param name="successDefaultUrl">Default url to get the user to, if the user logged in directly from the login page</param>
        /// <returns></returns>
        protected ActionResult HandleAppLogin(string email, string password, string redirectUrl, string successDefaultUrl)
        {
            if (AccountService == null)
            {
                throw new InvalidOperationException("BaseLoginRegisterController: Login cannot be handled before initalizing AccountService");
            }
            
            var permissions = AccountService.LogIn(email, password);

            var loginHandler = new LoginHandler(new UserPermissionsSessionManager(Session));
            loginHandler.Login(email, permissions);

            return Redirect(!string.IsNullOrEmpty(redirectUrl) ? redirectUrl : successDefaultUrl);
        }

        /// <summary>
        /// Handles facebook OAuth login
        /// </summary>
        /// <param name="authToken">Facebook auth_token for the user</param>
        /// <param name="email">User email</param>
        /// <param name="userTypeId">User type</param>
        /// <param name="redirectUrl"></param>
        /// <param name="successDefaultUrl"></param>
        /// <returns></returns>
        protected ActionResult HandleFacebookLogin(string authToken, string email, string redirectUrl,
                                string successDefaultUrl, int userTypeId)
        {
            if (AccountService == null)
            {
                throw new InvalidOperationException("BaseLoginRegisterController: Login cannot be handled before initalizing AccountService");
            }

            var permissions = AccountService.LogInFacebookUser(authToken, email, userTypeId);

            var loginHandler = new LoginHandler(new UserPermissionsSessionManager(Session));
            loginHandler.Login(email, permissions);

            return Redirect(!string.IsNullOrEmpty(redirectUrl) ? redirectUrl : successDefaultUrl);
        }

        /// <summary>
        /// Handles custom app register. User will be taken to successDefaultUrl after registration.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <param name="userTypeId">User type</param>
        /// <param name="successDefaultUrl">User will be taken to this url after completing registration.</param>
        /// <returns></returns>
        protected ActionResult HandleAppRegister(string email, string password, int userTypeId, string successDefaultUrl)
        {
            if (AccountService == null)
            {
                throw new InvalidOperationException("BaseLoginRegisterController: Login cannot be handled before initalizing AccountService");
            }

            AccountService.RegisterUser(new User
            {
                Email = email,
                Password = password,
                RegistrationStatus = RegistrationStatus.EmailNotVerified,
                Name = string.Empty,
                PhoneNumber = string.Empty
            }, userTypeId);

            var permissions = AccountService.LogIn(email, password);

            var loginHandler = new LoginHandler(new UserPermissionsSessionManager(Session));
            loginHandler.Login(email, permissions);

            return Redirect(successDefaultUrl);
        }
    }
}
