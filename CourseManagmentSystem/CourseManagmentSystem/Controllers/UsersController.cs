using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourseManagmentSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CourseManagmentSystem.Controllers
{
    public class UsersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager AuthentictionManager => HttpContext.GetOwinContext().Authentication;

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
     
        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.GetUserName());
            if (user == null) return RedirectToAction("LogIn", "Account");
            var model = new EditModel {Name = user.Name, Email = user.Email};
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await UserManager.FindByEmailAsync(User.Identity.GetUserName());
            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.UserName = model.Email;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthentictionManager.SignOut();
                    AuthentictionManager.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("","Something went wrong...");
                }
            }
            else
            {
                ModelState.AddModelError("","User is not find");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete()
        {
           return View();
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.GetUserName());
            if (user == null) return RedirectToAction("Index", "Home");
            var result = await UserManager.DeleteAsync(user);
            return result.Succeeded ? RedirectToAction("LogOut", "Account") : RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Subscriptions(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult Hostings(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}
