using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntroMVC.Models;

namespace IntroMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.MTitle = "List";
            ViewBag.Name = "Tanvir";
            ViewBag.Name2 = "Rahim";

            Student s1 = new Student() { 
                Id = 1,
                Name = "S1",
                Email = "s.email@g.c",
                Address = "Dhaka"
            };
            Student s2 = new Student() { 
                Id = 2,
                Name = "S2",
                Email = "s.email2@g.c",
                Address = "Dhaka"
            };
            List<Student> students = new List<Student>();
            students.Add(s1); 
            students.Add(s2);

            return View(students);

        }
        public ActionResult Create()
        {
            ViewBag.MTitle = "Create";
            TempData["Msg"] = "Redirected From Create";
            return RedirectToAction("Index","Home");
        }
    }
}