using AutoMapper;
using IntroEF.DTOs;
using IntroEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntroEF.Controllers
{
    public class StudentController : Controller
    {
        Sum25_BEntities db = new Sum25_BEntities();
        // GET: Student
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create() { 
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudentDTO s)
        {
            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<>();
            //});

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StudentDTO,Student>().ReverseMap();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<Student>(s);

            db.Students.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Details(int id) {
            var data = db.Students.Find(id);
            return View(data);

            
        }
        [HttpGet]
        public ActionResult Edit(int id) { 
            var student = db.Students.Find(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(Student s)
        {
            var exobj = db.Students.Find(s.Id);
            exobj.Name = s.Name;
            exobj.Cgpa = s.Cgpa;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public ActionResult Delete(int id) {
            var exObj = db.Students.Find(id);
            return View(exObj);
        }
        [HttpPost]
        public ActionResult Delete(int id, string choice) {
            if (choice.Equals("Yes")) {
                var exobj = db.Students.Find(id);
                db.Students.Remove(exobj);
                db.SaveChanges();
                TempData["Msg"] = exobj.Name +" deleted";
            }
            return RedirectToAction("Index");
            
        }
    }
}