using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class QuestionAnswer

    {
        public int QuestionAnswerID { get; set; }
        public int EnrollmentID { get; set; }
        public int QuestionID { get; set; }
        public string answer { get; set; }
        public int mark { get; set; }
        public virtual Question Question { get; set; }
        public virtual Enrollment Enrollment { get; set; }
        
    }
}