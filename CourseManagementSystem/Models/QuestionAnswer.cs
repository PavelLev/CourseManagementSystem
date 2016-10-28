using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagementSystem.Models
{
    public class QuestionAnswer

    {
        public int QuestionAnswerId { get; set; }
        public string Answer { get; set; }
        public int? Mark { get; set; }
        public string Question { get; set; }
        public int LessonID { get; set; }
        public Lesson Lesson { get; set; }
        public int EnrollmentID { get; set; }
        public virtual Enrollment Enrollment { get; set; }
        
    }
}