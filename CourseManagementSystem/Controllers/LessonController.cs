using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using CourseManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using WebGrease.Css.Extensions;
using static System.Int32;

namespace CourseManagementSystem.Controllers
{
    public class LessonController : Controller
    {
        private AppDbContext db = new AppDbContext();

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
            if (!User.Identity.IsAuthenticated || (User.Identity.GetUserId() != lesson.Course.UserId &&
                lesson.Course.Enrollments.All(item => User.Identity.GetUserId() != item.UserId)))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(lesson);
        }


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
                CourseId = lesson.CourseId ?? 0,
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
                }

            }
            db.Entry(lesson).State = EntityState.Modified;
            db.SaveChanges();

            

            return PartialView("~/Views/Lesson/Edit.cshtml", new LessonViewModel()
            {
                Name = lesson.Name,
                VideoLink = lesson.VideoLink == "" ? "" : "https://youtu.be/" + lesson.VideoLink,
                CourseId = lesson.CourseId ?? 0,
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

        public ActionResult Test(int lessonId)
        {
            var lesson = db.Lessons.Find(lessonId);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated || (User.Identity.GetUserId() != lesson.Course.UserId &&
                lesson.Course.Enrollments.All(item => User.Identity.GetUserId() != item.UserId)))
            {
                return RedirectToAction("Index", "Home");
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            var enrollmentId =
                user.Enrollments.First((enrollment) => enrollment.CourseId == lesson.CourseId).EnrollmentID;
            var model = new TestLessonViewModel
            {
                Lesson = lesson,
                LessonID = lesson.LessonID,
                EnrollmentID = enrollmentId,
                Questions = lesson.Questions.Select(question => question.Text).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Test()
        {
            var lessonId = int.Parse(Request.Form["LessonID"]);
            var enrollmentId = int.Parse(Request.Form["EnrollmentID"]);
            var answerCount = int.Parse(Request.Form["AnswerCount"]);
            for (var i = 0; i < answerCount; i++)
            {
                var answer = Request.Form["Answer" + i];
                db.QuestionAnswers.Add(new QuestionAnswer
                {
                    LessonID = lessonId,
                    EnrollmentID = enrollmentId,
                    Question = Request.Form["Question" + i],
                    Answer = answer,
                    Mark = answer == "" ? 0 as int? : null
                });
            }
            var lesson = db.Lessons.Find(lessonId);
            var enrollment = db.Enrollments.Find(enrollmentId);

            await EmailNotifications.Send(lesson.Course.User.Email, 
                "New answers to check", 
                enrollment.User.Name + " answered questions to lesson " + lesson.Name + " at course " + lesson.Course.Name);

            db.SaveChanges();
            return RedirectToAction("Details", "Lesson", new {lessonId});
        }

        public ActionResult Forum(int lessonId)
        {
            var lesson = db.Lessons.Find(lessonId);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated || (User.Identity.GetUserId() != lesson.Course.UserId &&
                lesson.Course.Enrollments.All(item => User.Identity.GetUserId() != item.UserId)))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(lesson);
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
