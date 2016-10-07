using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class CourseProgress
    {
        private User subscriber;
        private List<LessonProgress> lessonProgresses;

        private int totalMark;
    }
}