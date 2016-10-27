using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        [Required]
        [Display(Name="Question")]
        public string Text { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

    }
}