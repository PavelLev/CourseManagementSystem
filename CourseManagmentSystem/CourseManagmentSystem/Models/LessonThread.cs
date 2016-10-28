using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class LessonThread
    {
        public int LessonThreadId { get; set; }
        public string Name { get; set; }
        public DateTime LastChangeDateTime { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<LessonMessage> Messages { get; set; }
    }
}