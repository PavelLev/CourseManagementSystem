using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string text { get; set; }
        public int LessonID { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

    }
}