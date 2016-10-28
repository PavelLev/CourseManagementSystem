using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourseManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.SignalR;

namespace CourseManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager AuthentictionManager => HttpContext.GetOwinContext().Authentication;

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
            if (user == null)
                return RedirectToAction("LogIn", "Account");
            if (!User.Identity.IsAuthenticated || User.Identity.GetUserId() != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new EditModel {Name = user.Name, Email = user.Email};
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await UserManager.FindByEmailAsync(User.Identity.GetUserName());
            if (user != null)
            {
                var hubClients = GlobalHost.ConnectionManager.GetHubContext<MyHub>().Clients;
                hubClients.User(User.Identity.Name).updateTopEmail(model.Email);
                hubClients.User(User.Identity.Name).updateEditData(model.Name, model.Email);

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
        public async Task<ActionResult> Delete()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.GetUserName());
            if (user == null)
                return RedirectToAction("Index", "Home");
            if (!User.Identity.IsAuthenticated || User.Identity.GetUserId() != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

       
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.GetUserName());
            if (user == null)
                return RedirectToAction("Index", "Home");
            var result = await UserManager.DeleteAsync(user);

            LogOutUserTabs();

            return result.Succeeded ? RedirectToAction("LogOut", "Account") : RedirectToAction("Index","Home");
        }

        void LogOutUserTabs()
        {
            GlobalHost.ConnectionManager.GetHubContext<MyHub>().Clients.User(User.Identity.Name).logOut();
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
            if (!User.Identity.IsAuthenticated || User.Identity.GetUserId() != user.Id)
            {
                return RedirectToAction("Index", "Home");
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
            if (!User.Identity.IsAuthenticated || User.Identity.GetUserId() != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}
