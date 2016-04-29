using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;

namespace Tekhub.Identity.Web.Umbraco.Controllers
{
    public class BaseLogoutController : SurfaceController
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
