using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class Course
    {
        private string name, description;
        private User lecturer;
        private List<CourseProgress> subscribers;
        private List<Lesson> lessons;
    }
}