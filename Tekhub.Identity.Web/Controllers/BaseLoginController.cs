using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;
using Tekhub.Identity.Services.Interfaces;
using Tekhub.Identity.Web.App;

namespace Tekhub.Identity.Web.Controllers
{
    public class BaseLoginController : Controller
    {
        protected IAccountService AccountService { get; set; }

        protected BaseLoginController()
        {
            
        }

        protected BaseLoginController(IAccountService accountService)
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
                throw new InvalidOperationException("BaseLoginController: Login cannot be handled before initalizing AccountService");
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
                throw new InvalidOperationException("BaseLoginController: Login cannot be handled before initalizing AccountService");
            }

            var permissions = AccountService.LogInFacebookUser(authToken, email, userTypeId);

            var loginHandler = new LoginHandler(new UserPermissionsSessionManager(Session));
            loginHandler.Login(email, permissions);

            return Redirect(!string.IsNullOrEmpty(redirectUrl) ? redirectUrl : successDefaultUrl);
        }
    }
}
