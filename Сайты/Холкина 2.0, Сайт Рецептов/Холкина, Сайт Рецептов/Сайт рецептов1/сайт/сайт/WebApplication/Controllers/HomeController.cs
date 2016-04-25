using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult T()
        {
            // Добавляются категории
            var categories = db.Categories;
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            var firstId = categories.First().Id;
            ViewBag.Ingridients = db.Ingridients.Where(x => x.Category.Id == firstId);
            return View();
        }

        [HttpGet]
        public ActionResult NewIngridients(int id)
        {
            var newId = id;
            ViewBag.Ingridients = db.Ingridients.Where(x => x.Category.Id == newId);
            return Json("123",JsonRequestBehavior.AllowGet);
        }

        /// Получение ингридиентов по заданной категории
        public JsonResult NewIngridients2(string id)
        {
            var newId = 2;
            ViewBag.Ingridients = db.Ingridients.Where(x => x.Category.Id == newId);
            return Json("321");
        }
    }
}