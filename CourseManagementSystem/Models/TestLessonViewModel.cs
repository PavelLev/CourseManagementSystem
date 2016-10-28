using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagementSystem.Models
{
    public class TestLessonViewModel
    {
        public Lesson Lesson { get; set; }
        public int LessonID { get; set; }
        public int EnrollmentID { get; set; }
        public List<string> Questions { get; set; }
    }
}