using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagementSystem.Models
{
    public class LessonMessage
    {
        public int LessonMessageId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int ThreadId { get; set; }
        public LessonThread Thread { get; set; }
    }
}