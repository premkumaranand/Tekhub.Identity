using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Tekhub.Identity.Model;
using Tekhub.Identity.Services.Interfaces;

namespace Tekhub.Identity.Web.App
{
    public class AccountHelper
    {
        public IUserService UserService { get; set; }

        public AccountHelper(IUserService userService)
        {
            UserService = userService;
        }

        public User GetCurrentUser()
        {
            var currentUserEmail = HttpContext.Current.User.Identity.Name;
            return UserService.GetUser(currentUserEmail);
        }
    }
}
