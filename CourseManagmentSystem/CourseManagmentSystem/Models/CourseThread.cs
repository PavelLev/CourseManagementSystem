using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class CourseThread
    {
        public int CourseThreadId { get; set; }
        public string Name { get; set; }
        public DateTime LastChangeDateTime { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<CourseMessage> Messages { get; set; }
    }
}