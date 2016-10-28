using CourseManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseManagementSystem.Controllers
{
    public class CourseMessageController : Controller
    {
        private AppDbContext db = new AppDbContext();

        [HttpPost]
        public ActionResult Create([Bind(Include = "ThreadId, Text")] CourseMessage message)
        {
            if (message.Text == null)
            {
                return new EmptyResult();
            }
            message.UserId = User.Identity.GetUserId();
            message.User = db.Users.Find(message.UserId);
            message.CreationDateTime = DateTime.Now;
            db.CourseMessages.Add(message);
            message.Thread = db.CourseThreads.Find(message.ThreadId);
            message.Thread.LastChangeDateTime = message.CreationDateTime;
            db.Entry(message.Thread).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            return PartialView("Bar", message);
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