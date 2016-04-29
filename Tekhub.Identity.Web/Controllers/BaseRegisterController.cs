using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tekhub.Identity.Model;
using Tekhub.Identity.Services.Interfaces;
using Tekhub.Identity.Web.App;

namespace Tekhub.Identity.Web.Controllers
{
    public class BaseRegisterController : Controller
    {
        protected IAccountService AccountService { get; set; }

        protected BaseRegisterController()
        {
            
        }

        protected BaseRegisterController(IAccountService accountService)
        {
            AccountService = accountService;
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
                throw new InvalidOperationException("BaseLoginController: Login cannot be handled before initalizing AccountService");
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
