using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseManagementSystem.Models;
using Microsoft.AspNet.Identity;

namespace CourseManagementSystem.Controllers
{
    public class LessonMessageController : Controller
    {
        private AppDbContext db = new AppDbContext();

        [HttpPost]
        public ActionResult Create([Bind(Include = "ThreadId, Text")] LessonMessage message)
        {
            if (message.Text == null)
            {
                return new EmptyResult();
            }
            message.UserId = User.Identity.GetUserId();
            message.User = db.Users.Find(message.UserId);
            message.CreationDateTime = DateTime.Now;
            db.LessonMessages.Add(message);
            message.Thread = db.LessonThreads.Find(message.ThreadId);
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