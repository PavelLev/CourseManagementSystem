﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagementSystem.Models
{
    public class CheckCourseAnswersViewModel
    {
        public Course Course { get; set; }
        public ICollection<QuestionAnswer> Answers { get; set; }
    }
}