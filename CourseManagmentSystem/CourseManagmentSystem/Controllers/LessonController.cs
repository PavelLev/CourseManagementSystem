using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseManagmentSystem.Models;
using Microsoft.AspNet.SignalR;

namespace CourseManagmentSystem.Controllers
{
    public class LessonController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Lesson
        public ActionResult Index()
        {
            return View(db.Lessons.ToList());
        }

        // GET: Lesson/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }*/

        public ActionResult Details()
        {
            return View();
        }
        // GET: Lesson/Create
        public ActionResult Create(int courseId)
        {
            return View(new LessonViewModel()
            {
                CourseId = courseId
            });
        }

        // POST: Lesson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LessonViewModel model)
        {
            if (!ModelState.IsValidField("Name"))
                GlobalHost.ConnectionManager.GetHubContext<MyHub>()
                    .Clients.User(User.Identity.Name)
                    .updateCreateData(model.Name, model.CourseId, model.VideoLink);

            else if (!ModelState.IsValidField("TxtFile") || !ModelState.IsValidField("PdfFile"))
            {
                GlobalHost.ConnectionManager.GetHubContext<MyHub>()
                   .Clients.User(User.Identity.Name)
                   .updateCreateData(model.Name, model.CourseId, model.VideoLink);
            }
            var lesson = new Lesson()
            {
                TimeOfEdit = null,
                Name = model.Name,
                CourseId = model.CourseId,
                VideoLink = model.VideoLink
            };
            if (model.TxtFile != null)
            {
                using (var ms = new StreamReader(model.TxtFile.InputStream))
                {
                    lesson.Text = ms.ReadToEnd();
                }
            }
            if(model.PdfFile != null)
            {
                lesson.File = new PdfFile()
                {
                    Content = new BinaryReader(model.PdfFile.InputStream).ReadBytes(model.PdfFile.ContentLength),
                    ContentLenght = model.PdfFile.ContentLength,
                    ContentType = model.PdfFile.ContentType,
                    FileName = model.PdfFile.FileName,
                };
                db.PdfFiles.Add(lesson.File);
                db.SaveChanges();
            }
           
            db.Lessons.Add(lesson);
            db.SaveChanges();
            return RedirectToAction("Edit","Course",new {id = lesson.CourseId});
        }

        // GET: Lesson/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lesson/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonID,presentation,text")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lesson);
        }

        // GET: Lesson/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lesson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            db.Lessons.Remove(lesson);
            db.SaveChanges();
            return RedirectToAction("Index");
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
