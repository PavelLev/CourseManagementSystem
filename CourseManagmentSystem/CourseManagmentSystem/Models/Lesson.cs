using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public string name { get; set; }
        public string presentation { get; set; }
        public virtual Course Course { get; set; }
        public string text { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}