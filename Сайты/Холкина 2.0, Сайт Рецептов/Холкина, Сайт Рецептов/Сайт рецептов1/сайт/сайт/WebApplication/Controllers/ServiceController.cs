using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// отображение категорий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Categories()
        {
            ViewBag.Categories = _db.Categories;
            return View();
        }


        /// <summary>
        /// Добавление категорий
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Categories(Category cat)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(cat);
                _db.SaveChanges();
            }
            ViewBag.Categories = _db.Categories;
            return View(cat);
        }


        /// <summary>
        /// Удаление категории по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCategory(int id)
        {
            Category cat = _db.Categories.Find(id);
            _db.Categories.Remove(cat);

            var ingridients = _db.Ingridients.Where(x => x.Category.Id == cat.Id);
            _db.Ingridients.RemoveRange(ingridients);


            _db.SaveChanges();
            return RedirectToAction("Categories");
        }

        


        /// <summary>
        /// отображение ингридиентов
        /// </summary>
        /// <returns></returns>
        // GET: Ingridients/Create
        public ActionResult Ingridients()
        {
            var res = _db.Ingridients.Include(x => x.Category).ToList();
            ViewBag.Ingridients = res;
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            return View();
        }

        // POST: Ingridients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Ingridients([Bind(Include = "Id,Name,CategoryId")] Ingridient ingridient)
        {
            if (ModelState.IsValid)
            {
                var cat = _db.Categories.Find(ingridient.CategoryId);
                ingridient.Category = cat;

                //cat.Ingridients.Add(ingridient);

                //_db.Entry(cat).State = EntityState.Modified;

                _db.Ingridients.Add(ingridient);
                await _db.SaveChangesAsync();
            }

            ViewBag.Ingridients = _db.Ingridients.Include(x => x.Category);
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            return View("Ingridients", ingridient);
        }

        public ActionResult DeleteIngridient(int id)
        {
            Ingridient cat = _db.Ingridients.Find(id);
            _db.Ingridients.Remove(cat);
            _db.SaveChanges();

            ViewBag.Ingridients = _db.Ingridients.Include(x => x.Category);
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            return RedirectToAction("Ingridients");
        }

        /// <summary>
        /// ТИПЫ БЛЮД
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Types()
        {
            ViewBag.Types = _db.Types;
            return View();
        }

        /// <summary>
        /// Тип блюда
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Types(Type cat)
        {
            if (ModelState.IsValid)
            {
                _db.Types.Add(cat);
                _db.SaveChanges();
            }
            ViewBag.Types = _db.Types;
            return View(cat);
        }


        public ActionResult DeleteType(int id)
        {
            Type cat = _db.Types.Find(id);
            _db.Types.Remove(cat);

            var articles = _db.Articles.Where(x => x.Type.Id == cat.Id);
            _db.Articles.RemoveRange(articles);

            _db.SaveChanges();
            return RedirectToAction("Types");
        }

    }
}