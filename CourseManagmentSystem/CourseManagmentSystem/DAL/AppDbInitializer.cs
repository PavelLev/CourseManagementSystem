using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CourseManagmentSystem.Models;

namespace CourseManagmentSystem.DAL
{
    public class AppDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var users = new List<User>
            {
            new User{UserID=1, name="Carson",password="Alexander",email="myemail@mail.ru"},
            new User{UserID=2, name="Meredith",password="Alonso",email="myemail@mail.ru"},
            new User{UserID=3, name="Arturo",password="Anand",email="myemail@mail.ru"},
            new User{UserID=4, name="Gytis",password="Barzdukas",email="myemail@mail.ru"},
            new User{UserID=5, name="Yan",password="Li",email="myemail@mail.ru"},
            new User{UserID=6, name="Peggy",password="Justice",email="myemail@mail.ru"},
            new User{UserID=7, name="Laura",password="Norman",email="myemail@mail.ru"},
            new User{UserID=8, name="Nino",password="Olivetto",email="myemail@mail.ru"}
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
            new Course{CourseID=1050,name="Chemistry",description="dbnfbdf,gbdfjgf"},
            new Course{CourseID=4022,name="Microeconomics",description="dbnfbdf,gbdfjgf"},
            new Course{CourseID=4041,name="Macroeconomics",description="dbnfbdf,gbdfjgf"},
            new Course{CourseID=1045,name="Calculus",description="dbnfbdf,gbdfjgf"},
            new Course{CourseID=3141,name="Trigonometry",description="dbnfbdf,gbdfjgf"},
            new Course{CourseID=2021,name="Composition",description="dbnfbdf,gbdfjgf"},
            new Course{CourseID=2042,name="Literature",description="dbnfbdf,gbdfjgf"}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var enrollments = new List<Enrollment>
            {
            new Enrollment{EnrollmentID=1, UserID=1,CourseID=1050,totalMark=90},
            new Enrollment{EnrollmentID=2, UserID=1,CourseID=4022,totalMark=74},
            new Enrollment{EnrollmentID=3, UserID=1,CourseID=4041,totalMark=89},
            new Enrollment{EnrollmentID=4, UserID=2,CourseID=1045},
            new Enrollment{EnrollmentID=5, UserID=2,CourseID=3141},
            new Enrollment{EnrollmentID=6, UserID=2,CourseID=2021},
            new Enrollment{EnrollmentID=7, UserID=3,CourseID=1050},
            new Enrollment{EnrollmentID=8, UserID=4,CourseID=1050,totalMark=75},
            new Enrollment{EnrollmentID=9, UserID=4,CourseID=4022,totalMark=65},
            new Enrollment{EnrollmentID=10, UserID=5,CourseID=4041,totalMark=100},
            new Enrollment{EnrollmentID=11, UserID=6,CourseID=1045},
            new Enrollment{EnrollmentID=12, UserID=7,CourseID=3141,totalMark=65},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
            var lessons = new List<Lesson>
            {
            new Lesson{LessonID=1034, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1035, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1036, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1037, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1038, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1039, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1040, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1041, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1042, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1043, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1044, presentation="sdsfdgd.pdf",text="dfgssss.txt"},
            new Lesson{LessonID=1045, presentation="sdsfdgd.pdf",text="dfgssss.txt"}
            };
            lessons.ForEach(s => context.Lessons.Add(s));
            context.SaveChanges();

            var questions = new List<Question>
            {
            new Question{QuestionID=1, text="dfgssss?", maxMark=4},
            new Question{QuestionID=2, text="aasa sss?", maxMark=3},
            new Question{QuestionID=3, text="dfgssss?", maxMark=4},
            new Question{QuestionID=4, text="dfdfg fdss?", maxMark=4},
            new Question{QuestionID=5, text="dfgssss?", maxMark=5},
            new Question{QuestionID=6, text="dfgssss?", maxMark=4},
            new Question{QuestionID=7, text="sd tfyg ffgsss?", maxMark=4},
            new Question{QuestionID=8, text="df gssss?", maxMark=1},
            new Question{QuestionID=9, text="dfgssss?", maxMark=4},
            new Question{QuestionID=10, text="drtytsss?", maxMark=2},
            new Question{QuestionID=11, text="dfg ssss?", maxMark=4},
            new Question{QuestionID=12, text="dfsss?", maxMark=4},
            };
            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();
            var questionAnswers = new List<QuestionAnswer>
            {
            new QuestionAnswer{QuestionAnswerID=2, EnrollmentID=3, answer="sdfhbb sdffg j gfdfgdhjh", mark=3},
            new QuestionAnswer{QuestionAnswerID=1, EnrollmentID=2, answer="sdfhbb hbj gfghjh", mark=3},
            new QuestionAnswer{QuestionAnswerID=2, EnrollmentID=3, answer="sdfhbb hbg dfgg hjh", mark=1},
            new QuestionAnswer{QuestionAnswerID=5, EnrollmentID=9, answer="sfdgfg bj gfghjh", mark=5},
            new QuestionAnswer{QuestionAnswerID=3, EnrollmentID=4, answer="sddfgg  dfg jh", mark=3},
            new QuestionAnswer{QuestionAnswerID=8, EnrollmentID=3, answer="sdfhbb hbj gfghjh", mark=3},
            new QuestionAnswer{QuestionAnswerID=2, EnrollmentID=7, answer="sdfhbb hbj gfghjh", mark=2},
            new QuestionAnswer{QuestionAnswerID=2, EnrollmentID=10, answer="sdfhbb", mark=3},
            new QuestionAnswer{QuestionAnswerID=5, EnrollmentID=8, answer="sdfhbb hbj gfghjh", mark=3},
            new QuestionAnswer{QuestionAnswerID=2, EnrollmentID=5, answer="fdfg hbj gfghjh", mark=3},
            new QuestionAnswer{QuestionAnswerID=7, EnrollmentID=3, answer="sdfdfgfgf dd bb hbj gfghjh", mark=0},
            new QuestionAnswer{QuestionAnswerID=3, EnrollmentID=1, answer="sdfhbb hbj gfghjh", mark=3},
            };
            questionAnswers.ForEach(s => context.QuestionAnswers.Add(s));
            context.SaveChanges();
        }
    }
}