using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseManagementSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        [Display(Name = "Name of course")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CourseThread> Threads { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}
