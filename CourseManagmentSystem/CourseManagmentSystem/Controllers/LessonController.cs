using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using CourseManagmentSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using WebGrease.Css.Extensions;
using static System.Int32;

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

        public ActionResult Details(int? lessonId)
        {
            if (lessonId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var lesson = db.Lessons.Find(lessonId);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }
        // GET: Lesson/Create


        // POST: Lesson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        public ActionResult Create()
        {

            if (Request.Files["TxtFile"].ContentLength == 0 && Request.Files["PdfFile"].ContentLength == 0)
            {
                return PartialView("~/Views/Lesson/Error.cshtml", "no files");
            }
             if ((Request.Files["TxtFile"].ContentLength > 0 && Request.Files["TxtFile"].ContentType != "text/plain") ||
                    (Request.Files["PdfFile"].ContentLength > 0 && Request.Files["PdfFile"].ContentType != "application/pdf"))
                {
                    return PartialView("~/Views/Lesson/Error.cshtml", "wrong extensions");
                }

            var lesson = new Lesson
            {
                Name = Request.Form["Name"],
                TimeOfEdit = null,
                CourseId = Parse(Request.Form["CourseId"]),
                Text = "",
                VideoLink =  YoutubeLink.GetVideoId(Request.Form["VideoLink"])
            };


            if (Request.Form["Name"] == "")
            {
                lesson.Name = Request.Files["TxtFile"].ContentLength > 0 ? Request.Files["TxtFile"].FileName : Request.Files["PdfFile"].FileName;
            }

            if (Request.Files["TxtFile"].ContentLength > 0)
            {
                using (var ms = new StreamReader(Request.Files["TxtFile"].InputStream))
                {
                    lesson.Text = ms.ReadToEnd();
                }
            }

            if (Request.Files["PdfFile"].ContentLength > 0)
            {
                lesson.File = new PdfFile()
                {
                    Content = new BinaryReader(Request.Files["PdfFile"].InputStream).ReadBytes(Request.Files["PdfFile"].ContentLength),
                    ContentLenght = Request.Files["PdfFile"].ContentLength,
                    ContentType = Request.Files["PdfFile"].ContentType,
                    FileName = Request.Files["PdfFile"].FileName,
                };

                db.PdfFiles.Add(lesson.File);
                db.SaveChanges();
            }

            db.Lessons.Add(lesson);
            db.SaveChanges();

            return PartialView("~/Views/Lesson/Edit.cshtml", new LessonViewModel()
            {
                Name = lesson.Name,
                VideoLink = lesson.VideoLink == "" ? "" : "https://youtu.be/" + lesson.VideoLink,
                CourseId = lesson.CourseId,
                Text = lesson.Text,
                LessonId = lesson.LessonID,
                PdfFileId = lesson.PdfFileId,
                TimeOfEdit = lesson.TimeOfEdit

            });
        }

       

        [HttpPost]
        public ActionResult Edit()
        {
            if ((Request.Files["TxtFile"].ContentLength > 0 && Request.Files["TxtFile"].ContentType != "text/plain") ||
                   (Request.Files["PdfFile"].ContentLength > 0 && Request.Files["PdfFile"].ContentType != "application/pdf"))
            {
                return PartialView("~/Views/Lesson/Error.cshtml", "wrong extensions");
            }

            var lesson = db.Lessons.Find(Parse(Request.Form["LessonId"]));

            lesson.Name = Request.Form["Name"];
            lesson.VideoLink = YoutubeLink.GetVideoId(Request.Form["VideoLink"]);
            lesson.TimeOfEdit = DateTime.Now;

            if (Request.Files["TxtFile"].ContentLength > 0)
            {
                using (var ms = new StreamReader(Request.Files["TxtFile"].InputStream))
                {
                    lesson.Text = ms.ReadToEnd();
                }
            }

            if (Request.Files["PdfFile"].ContentLength > 0)
            {
                if (lesson.PdfFileId == null)
                {
                    lesson.File = new PdfFile()
                    {
                        Content =
                            new BinaryReader(Request.Files["PdfFile"].InputStream).ReadBytes(
                                Request.Files["PdfFile"].ContentLength),
                        ContentLenght = Request.Files["PdfFile"].ContentLength,
                        ContentType = Request.Files["PdfFile"].ContentType,
                        FileName = Request.Files["PdfFile"].FileName,
                    };
                    db.PdfFiles.Add(lesson.File);
                    db.SaveChanges();
                }
                else
                {
                    lesson.File.Content =
                        new BinaryReader(Request.Files["PdfFile"].InputStream).ReadBytes(
                            Request.Files["PdfFile"].ContentLength);
                    lesson.File.ContentLenght = Request.Files["PdfFile"].ContentLength;
                    lesson.File.ContentType = Request.Files["PdfFile"].ContentType;
                    lesson.File.FileName = Request.Files["PdfFile"].FileName;
                    db.Entry(lesson.File).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            

            return PartialView("~/Views/Lesson/Edit.cshtml", new LessonViewModel()
            {
                Name = lesson.Name,
                VideoLink = lesson.VideoLink == "" ? "" : "https://youtu.be/" + lesson.VideoLink,
                CourseId = lesson.CourseId,
                Text = lesson.Text,
                LessonId = lesson.LessonID,
                PdfFile = lesson.File,
                PdfFileId = lesson.PdfFileId,
                TimeOfEdit = lesson.TimeOfEdit

            });
        }



        [HttpPost]
        public ActionResult Delete(int id)
        {
            var lesson = db.Lessons.Find(id);
            if (lesson.File != null)
            {
             db.PdfFiles.Remove(lesson.File);   
            }
            if (lesson.Questions.Count != 0)
            {
                var count = lesson.Questions.Count;
                for (var i = 0; i < count; i++)
                {
                    db.Questions.Remove(lesson.Questions.AsEnumerable().ElementAt(0));
                    db.SaveChanges();
                }
            }
            db.Lessons.Remove(lesson);
            db.SaveChanges();
            return new EmptyResult();
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
