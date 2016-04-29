using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Tekhub.Identity.Web.Controllers
{
    public class BaseLogoutController : Controller
    {
        /// <summary>
        /// Url to redirect the user to after logging out
        /// </summary>
        protected string RedirectUrl { get; set; }

        // GET: Logout
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return Redirect(RedirectUrl);
        }
    }
}
