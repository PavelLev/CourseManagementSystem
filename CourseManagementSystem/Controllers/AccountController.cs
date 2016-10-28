using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourseManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security;
using WebGrease.Css.Extensions;
using static System.String;

namespace CourseManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager AuthentictionManager => HttpContext.GetOwinContext().Authentication;

        
        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }
            return View("Register");
        }
        
        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (!ModelState.IsValid) return View("Register", model);
            var user = new User { UserName = model.Email, Email = model.Email, Name = model.Name };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthentictionManager.SignOut();
                AuthentictionManager.SignIn(new AuthenticationProperties()
                {
                    IsPersistent = true
                }, claim);
                
                ReloadBrowserTabs();

                return RedirectToAction("Index", "Home");
            }
            else
                result.Errors.ForEach(err => ModelState.AddModelError("", err));
            return View(model);
        }

        
        public ActionResult LogIn(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.returnUrl = returnUrl;
            return View("LogIn");
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong email or password");
                }
                else
                {
                    var claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthentictionManager.SignOut();
                    AuthentictionManager.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = true
                    }, claim);

                    ReloadBrowserTabs();

                    if (IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult LogOut()
        {
            AuthentictionManager.SignOut();

            ReloadBrowserTabs();

            return RedirectToAction("Index", "Home");
        }

        void ReloadBrowserTabs()
        {
            GlobalHost.ConnectionManager.GetHubContext<MyHub>()
                .Clients.Clients(MyHub.GetConnections(Request.Cookies["__RequestVerificationToken"].Value))
                .reload();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}