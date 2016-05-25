using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using HelpDeskTrain.Models;

namespace HelpDeskTrain.Controllers
{
    [Authorize (Roles="Администратор")]
    public class ServiceController : Controller
    {
        HelpdeskContext db = new HelpdeskContext();
        
        [HttpGet]
        public ActionResult Departments()
        {
            ViewBag.Departments=db.Departments;
            return View();
        }

        //Добавление отдела с последующим их отображением
        [HttpPost]
        public ActionResult Departments(Department depo)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(depo);
                db.SaveChanges();
            }
            ViewBag.Departments = db.Departments;
            return View(depo);
        }
        // Удаление отдела по id
        public ActionResult DeleteDepartment(int id)
        {
            Department depo = db.Departments.Find(id);
            db.Departments.Remove(depo);
            db.SaveChanges();
            return RedirectToAction("Departments");
        }

        //Активы
        [HttpGet]
        public ActionResult Activ()
        {
            ViewBag.Activs = db.Activs.Include(s => s.Department);
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        //Добавляем актив
        [HttpPost]
        public ActionResult Activ(Activ activ)
        {
            if (ModelState.IsValid)
            {
                db.Activs.Add(activ);
                db.SaveChanges();
            }
            ViewBag.Activs = db.Activs.Include(s => s.Department);
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Name");
            return View(activ);
        }

        // Удаление актива по id
        public ActionResult DeleteActiv(int id)
        {
            Activ activ = db.Activs.Find(id);
            db.Activs.Remove(activ);
            db.SaveChanges();
            return RedirectToAction("Activ");
        }

        // отображение категорий
        [HttpGet]
        public ActionResult Categories()
        {
            ViewBag.Categories = db.Categories;
            return View();
        }

        // Добавление категорий
        [HttpPost]
        public ActionResult Categories(Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
            }
            ViewBag.Categories = db.Categories;
            return View(cat);
        }
        // Удаление категории по id
        public ActionResult DeleteCategory(int id)
        {
            Category cat = db.Categories.Find(id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
    }
}
