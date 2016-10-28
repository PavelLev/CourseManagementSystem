using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int? totalMark { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}