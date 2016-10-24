using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class LessonViewModel
    {
        [Required]
        [Display(Name = "Lesson name")]
        public string Name { get; set; }
        public int CourseId { get; set; }
        //[Required]
        //[Display(Name="Text of lecture")]
        public string Text { get; set; }
        [Display(Name = "Video link")]
        public string VideoLink { get; set; }
        [Required, FileExtensions(Extensions = ".txt", ErrorMessage = "Please select an txt/md file")]
        [Display(Name = "TXT/MD file")]
        public HttpPostedFileBase TxtFile { get; set; }
        [Required, FileExtensions(Extensions = ".pdf", ErrorMessage = "Please select an pdf file")]
        [Display(Name = "PDF file")]
        public HttpPostedFileBase PdfFile { get; set; }
    }
}