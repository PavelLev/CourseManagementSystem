using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class User
    {
        private string name, password, email;
        private List<Course> hosted;

        private List<Course> subscriptions;
    }
}