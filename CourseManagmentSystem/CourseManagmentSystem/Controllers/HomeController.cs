using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseManagmentSystem.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace CourseManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

    }
}