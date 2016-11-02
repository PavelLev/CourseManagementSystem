using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Controllers
{
    public class QuestionAnswerController : Controller
    {
        private AppDbContext db = new AppDbContext();
        
        [HttpPost]
        public async Task<ActionResult> Check(QuestionAnswer questionAnswer)
        {
            db.Entry(questionAnswer).State = EntityState.Modified;
            db.SaveChanges();
            var enrollment = db.Enrollments.Find(questionAnswer.EnrollmentID);
            var lesson = db.Lessons.Find(questionAnswer.LessonID);
            if (enrollment.User.EmailNotifications)
            {
                await EmailNotifications.Send(enrollment.User.Email,
                    "New results to check",
                    "Your answers to lesson " + lesson.Name + " at course " + lesson.Course.Name + "have been checked");
            }
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