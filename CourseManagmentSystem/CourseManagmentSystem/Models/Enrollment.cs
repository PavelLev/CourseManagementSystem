using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int UserID { get; set; }
        public int? totalMark { get; set; }
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}