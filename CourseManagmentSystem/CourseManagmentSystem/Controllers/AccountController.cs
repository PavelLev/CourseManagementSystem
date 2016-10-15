using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourseManagmentSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebGrease.Css.Extensions;
using static System.String;

namespace CourseManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager AuthentictionManager => HttpContext.GetOwinContext().Authentication;

        
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
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

                return RedirectToAction("Details", "Users", new { id = user.Id });
            }
            else
                result.Errors.ForEach(err => ModelState.AddModelError("", err));
            return View(model);
        }

        
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            return RedirectToAction("LogIn", "Account");
        }

    }
}