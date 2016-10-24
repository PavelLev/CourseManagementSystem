using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public string Text { get; set; }
        public string VideoLink { get; set; }
        public int? PdfFileId { get; set; }
        public DateTime? TimeOfEdit { get; set; }
        public virtual PdfFile File { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}