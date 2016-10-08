using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Course> Hostings { get; set; }

    }
}