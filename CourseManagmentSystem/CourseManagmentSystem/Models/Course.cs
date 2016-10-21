using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}