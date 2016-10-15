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

namespace CourseManagmentSystem.Controllers
{
    public class UsersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

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
            User user = db.Users.Find(id);
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
            if (user != null)
            {
                var model = new EditModel {Name = user.Name, Email = user.Email};
                return View(model);
            }
            
            return RedirectToAction("LogIn","Account");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(User.Identity.GetUserName());
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Details", "Users", new {id = User.Identity.GetUserId()});
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
            if (user != null)
            {
                var result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogOut", "Account");
                }
            }
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Profile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        
    }
}
