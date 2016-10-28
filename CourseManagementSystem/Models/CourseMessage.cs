using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseManagementSystem.Models
{
    public class CourseMessage
    {
        public int CourseMessageId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int ThreadId { get; set; }
        public CourseThread Thread { get; set; }
    }
}