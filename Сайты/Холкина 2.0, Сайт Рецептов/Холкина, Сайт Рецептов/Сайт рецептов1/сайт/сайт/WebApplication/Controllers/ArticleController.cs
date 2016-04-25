using System.Web.Helpers;

namespace WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using WebApplication.Models;
    using Microsoft.AspNet.Identity;
    using System.Web.WebPages;

    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // поиск задач по типам
        public ActionResult Index(int? type, int[] ingridient)
        {
            List<ArticleDTO> articles = new List<ArticleDTO>();
            var types = _db.Types.ToList();
            var categories = _db.Categories.Include(x=>x.Ingridients).ToList();
            
            // Добавляются типы
            if (!types.Any())
            {
                return Content("Извините, но пока нет созданных типов блюд");
            }

            if (type == null || type == 0)
            {
                if (ingridient != null)
                {
                    // Ищем все рецепты, включающие в себя заданные ингридиенты
                    var ingr = ingridient.Select(i => _db.Ingridients.Find(i)).ToList();

                    foreach (var i in ingr)
                    {
                        var arts = _db.Articles.Where(x => x.Ingridients.Any(y => y.Id == i.Id)).Select(x => new ArticleDTO
                        {
                            Author = x.Author,
                            AuthorId = x.AuthorId,
                            Name = x.Name,
                            Description = x.Description,
                            Difficulty = x.Difficulty,
                            Type = x.Type,
                            Document = x.Document,
                            Id = x.Id,
                            TypeId = x.Id,
                            DocumentId = x.DocumentId
                        })
                        .ToList();
                        articles.AddRange(arts);
                    }
                    articles.Distinct();
                }
                else
                {
                    articles = _db.Articles.Select(x => new ArticleDTO
                    {
                        Author = x.Author,
                        AuthorId = x.AuthorId,
                        Name = x.Name,
                        Description = x.Description,
                        Difficulty = x.Difficulty,
                        Type = x.Type,
                        Document = x.Document,
                        Id = x.Id,
                        TypeId = x.Id,
                        DocumentId = x.DocumentId
                    }).ToList(); ;
                }
            }
            else
            {
                if (ingridient != null)
                {
                    // Ищем все рецепты, включающие в себя заданные ингридиенты
                    var ingr = new List<Ingridient>();
                    foreach (var i in ingridient)
                    {
                        ingr.Add(_db.Ingridients.Find(i));
                    }

                    foreach (var i in ingr)
                    {
                        var arts = _db.Articles.Where(x => x.Ingridients.Any(y => y.Id == i.Id)).Where(x => x.Type.Id == type).Select(x => new ArticleDTO
                        {
                            Author = x.Author,
                            AuthorId = x.AuthorId,
                            Name = x.Name,
                            Description = x.Description,
                            Difficulty = x.Difficulty,
                            Type = x.Type,
                            Document = x.Document,
                            Id = x.Id,
                            TypeId = x.Id,
                            DocumentId = x.DocumentId
                        }).ToList();

                        if (arts != null)
                        {
                            articles.AddRange(arts);
                        }
                    }

                    articles.Distinct();
                }
                else
                {
                    articles = _db.Articles.Where(x => x.Type.Id == type).Select(x => new ArticleDTO
                    {
                        Author = x.Author,
                        AuthorId = x.AuthorId,
                        Name = x.Name,
                        Description = x.Description,
                        Difficulty = x.Difficulty,
                        Type = x.Type,
                        Document = x.Document,
                        Id = x.Id,
                        TypeId = x.Id,
                        DocumentId = x.DocumentId
                    })
                    .ToList();
                }
            }
            
            ViewBag.Categories = categories;

            types.Insert(0, new Models.Type { Name = "Все", Id = 0 });
            ViewBag.Types = new SelectList(types, "Id", "Name");

            return View(articles.ToList());
        }



        [HttpGet]
        public ActionResult Create()
        {
            var curId = HttpContext.User.Identity.GetUserId();
            // получаем текущего пользователя
            ApplicationUser user = _db.Users.FirstOrDefault(m => m.Id == curId);
            if (user != null)
            {
                // Добавляются категории
                var categories = _db.Categories.Include(x=>x.Ingridients);
                if (!categories.Any())
                {
                    return Content("Извините, но пока нет созданных категорий продуктов");
                }
                ViewBag.Categories = categories;// "Id", "Name";

                ViewBag.CategoriesForView = new SelectList(_db.Categories, "Id", "Name");
                var firstId = categories.First().Id;

                // Ингридиенты
                ViewBag.Ingridients = _db.Ingridients.Where(x => x.Category.Id == firstId);

                // Добавляются типы
                var types = _db.Types;
                if (!types.Any())
                {
                    return Content("Извините, но пока нет созданных типов блюд");
                }
                ViewBag.Types = new SelectList(types, "Id", "Name");

                return View();
            }
            return RedirectToAction("LogOff", "Account");
        }

        /// Получение ингридиентов по заданной категории
        public ActionResult NewIngridients(int id)
        {
            var newId = id;
            ViewBag.Ingridients = _db.Ingridients.Where(x => x.Category.Id == newId);
            return PartialView();
        }

        // Создание новой рецепты
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Difficulty,TypeId")] Article article, int[] ingridient, HttpPostedFileBase error)
        {
            var curId = HttpContext.User.Identity.GetUserId();
            // получаем текущего пользователя
            ApplicationUser user = _db.Users.FirstOrDefault(m => m.Id == curId);

            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (ingridient != null)
                    {
                        //получаем выбранные ингридиенты
                        foreach (var c in ingridient)
                        {
                            var ingr = _db.Ingridients.Find(c);
                            // И добавляем
                            article.Ingridients.Add(ingr);
                        }
                    }

                    // если получен файл
                    if (error != null)
                    {
                        DateTime current = DateTime.Now;
                        Document doc = new Document();
                        doc.Size = error.ContentLength;
                        // Получаем расширение
                        string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                        doc.Type = ext;
                        // сохраняем файл по определенному пути на сервере
                        string path = current.ToString(user.Id.GetHashCode() + "dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                        error.SaveAs(Server.MapPath("~/Files/ArticleFiles/" + path));
                        doc.Url = path;

                        article.Document = doc;
                        _db.Documents.Add(doc);
                    }
                    else
                        article.Document = null;


                    // указываем пользователя рецепты
                    article.Author = user;
                    article.AuthorId = user.Id;
                    article.Type = _db.Types.Find(article.TypeId);

                    var type = _db.Types.Find(article.TypeId);
                    article.Type = type;
                    article.TypeId = type.Id;
                    

                    _db.Articles.Add(article);
                    _db.Entry(user).State = EntityState.Modified;

                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }

                return RedirectToAction("Index");
            }

           

            return View(article);
        }

        /// <summary>
        /// Получение рецептов текущего пользователя
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public ActionResult MyIndex(int? type, int[] ingridient)
        {
            var curId = HttpContext.User.Identity.GetUserId();

            List<ArticleDTO> articles = new List<ArticleDTO>();
            var categories = _db.Categories.Include(x => x.Ingridients).ToList();
            var types = _db.Types.ToList();


            // Добавляются категории
            if (categories.Count() == 0)
            {
                return Content("Извините, но пока нет созданных типов блюд");
            }

            if (type == null || type == 0)
            {
                if (ingridient != null)
                {
                    // Ищем все рецепты, включающие в себя заданные ингридиенты
                    var ingr = ingridient.Select(i => _db.Ingridients.Find(i)).ToList();

                    foreach (var i in ingr)
                    {
                        var arts = _db.Articles.Where(x => x.AuthorId == curId).Where(x => x.Ingridients.Any(y => y.Id == i.Id)).Select(x => new ArticleDTO
                        {
                            Author = x.Author,
                            AuthorId = x.AuthorId,
                            Name = x.Name,
                            Description = x.Description,
                            Difficulty = x.Difficulty,
                            Type = x.Type,
                            Document = x.Document,
                            Id = x.Id,
                            TypeId = x.Id,
                            DocumentId = x.DocumentId
                        })
                        .ToList();
                        articles.AddRange(arts);
                    }
                    articles.Distinct();
                }
                else
                {
                    articles = _db.Articles.Where(x => x.AuthorId == curId).Select(x => new ArticleDTO
                    {
                        Author = x.Author,
                        AuthorId = x.AuthorId,
                        Name = x.Name,
                        Description = x.Description,
                        Difficulty = x.Difficulty,
                        Type = x.Type,
                        Document = x.Document,
                        Id = x.Id,
                        TypeId = x.Id,
                        DocumentId = x.DocumentId
                    }).ToList(); ;
                }                
            }
            else
            {
                if (ingridient != null)
                {
                    // Ищем все рецепты, включающие в себя заданные ингридиенты
                    var ingr = new List<Ingridient>();
                    foreach (var i in ingridient)
                    {
                        ingr.Add(_db.Ingridients.Find(i));
                    }

                    foreach (var i in ingr)
                    {
                        var arts = _db.Articles.Where(x => x.AuthorId == curId).Where(x => x.Ingridients.Any(y => y.Id == i.Id)).Where(x => x.Type.Id == type).Select(x => new ArticleDTO
                        {
                            Author = x.Author,
                            AuthorId = x.AuthorId,
                            Name = x.Name,
                            Description = x.Description,
                            Difficulty = x.Difficulty,
                            Type = x.Type,
                            Document = x.Document,
                            Id = x.Id,
                            TypeId = x.Id,
                            DocumentId = x.DocumentId
                        }).ToList();

                        if (arts != null)
                        {
                            articles.AddRange(arts);
                        }
                    }

                    articles.Distinct();
                }
                else
                {
                    articles = _db.Articles.Where(x => x.AuthorId == curId).Where(x => x.Type.Id == type).Select(x => new ArticleDTO
                    {
                        Author = x.Author,
                        AuthorId = x.AuthorId,
                        Name = x.Name,
                        Description = x.Description,
                        Difficulty = x.Difficulty,
                        Type = x.Type,
                        Document = x.Document,
                        Id = x.Id,
                        TypeId = x.Id,
                        DocumentId = x.DocumentId
                    })
                    .ToList();
                }                
            }

            ViewBag.Categories = categories;

            types.Insert(0, new Models.Type { Name = "Все", Id = 0 });
            ViewBag.Types = new SelectList(types, "Id", "Name");

            return View(articles.ToList());
        }


        /// <summary>
        /// Просмотр подробных сведений о заявке
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Article article = _db.Articles.Where(r => r.Id == id).Include(r => r.Ingridients).First();

            if (article != null)
            {
                //получаем категорию
                article.Type = _db.Types.First(m => m.Id == article.TypeId);
                return PartialView("_Details", article);
            }
            return View("Index");
        }

        //[Authorize]
        public ActionResult Author(string id)
        {
            ApplicationUser executor = _db.Users.Where(m => m.Id == id).First();

            if (executor != null)
            {
                return PartialView("_Author", executor);
            }
            return View("Index");
        }


        // Удаление рецепты
        //[Authorize]
        public ActionResult Delete(int id)
        {
            Article article = _db.Articles.Find(id);
            var curId = HttpContext.User.Identity.GetUserId();
            // получаем текущего пользователя
            ApplicationUser user = _db.Users.First(m => m.Id == curId);
            if (article != null && article.Author.Id == user.Id)
            {
                _db.Articles.Remove(article);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Статистика внизу страницы
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCountOfAllArticles()
        {
            string count = _db.Articles.Count().ToString();
            ViewBag.Message = count;
            return PartialView("Message");
        }

        public ActionResult GetCountOfAllUsers()
        {
            string count = _db.Users.Count().ToString();
            ViewBag.Message = count;
            return PartialView("Message");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
