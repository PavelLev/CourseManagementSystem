using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseManagementSystem.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public override string Email { get; set; }
        public bool EmailNotifications { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Course> Hostings { get; set; }

        public User()
        {
        }

    }
}