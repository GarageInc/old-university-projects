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

    public class RequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            IEnumerable<MathTask> allReqs = null;
            
                allReqs = db.Requests
                    //.Where(x=>x.Checked)
                    ;

            return View(allReqs.ToList());
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        //[Authorize]
        public ActionResult Create()
        {
            var curId = HttpContext.User.Identity.GetUserId();
            // получаем текущего пользователя
            ApplicationUser user = db.Users.Where(m => m.Id == curId).FirstOrDefault();
            if (user != null)
            {
                return View();
            }
            return RedirectToAction("LogOff", "Account");
        }

        // Создание новой задачи
        [HttpPost]
        //[Authorize]
        public ActionResult Create(MathTask request, HttpPostedFileBase error)
        {
            var curId = HttpContext.User.Identity.GetUserId();
            // получаем текущего пользователя
            ApplicationUser user = db.Users.FirstOrDefault(m => m.Id == curId);
            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            if (ModelState.IsValid)
            {
                // указываем статус Открыта у задачи
                request.Status = (int)RequestStatus.Open;

                //получаем время открытия
                DateTime current = DateTime.Now;
                
                // указываем пользователя задачи
                request.Author = user;
                request.AuthorId = user.Id;


                // если получен файл
                if (error != null)
                {
                    Document doc = new Document();
                    doc.Size = error.ContentLength;
                    // Получаем расширение
                    string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                    doc.Type = ext;
                    // сохраняем файл по определенному пути на сервере
                    string path = current.ToString(user.Id.GetHashCode()+"dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                    error.SaveAs(Server.MapPath("~/Files/RequestFiles/" + path));
                    doc.Url = path;

                    request.Document = doc;
                    db.Documents.Add(doc);
                }
                else
                    request.Document = null;
                
                request.Status = (int)RequestStatus.Open;

                //Добавляем задачу с возможно приложенными документами
                db.Requests.Add(request);
                user.Requests.Add(request);
                db.Entry(user).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    return Content(e.Message);
                }

                return RedirectToAction("Index");
            }
            return View(request);
        }

        /// <summary>
        /// Получение задач текущего пользователя
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public ActionResult MyIndex()
        {
            var currentId = HttpContext.User.Identity.GetUserId();
            // получаем текущего пользователя
            ApplicationUser user = db.Users.Where(m => m.Id == currentId).FirstOrDefault();
            IEnumerable<MathTask> allReqs = null;
                allReqs = db.Requests
                    .Where(r => r.Author.Id == user.Id) //получаем задачи для текущего пользователя
                    .Include(r => r.Author)// добавляем данные о пользователях
                    .Include(r=>r.RightMathTaskSolution)
                    .ToList();         
            
            return View(allReqs);
        }


        /// <summary>
        /// Просмотр подробных сведений о задаче
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MyDetails(int id)
        {
            MathTask request = db.Requests.Find(id);

            if (request != null)
            {
                //получаем категорию
                return PartialView("_Details", request);
            }
            return View("MyIndex");
        }

        //[Authorize]
        public ActionResult MyExecutor(string id)
        {
            ApplicationUser executor = db.Users.First(m => m.Id == id);

            if (executor != null)
            {
                return PartialView("_Executor", executor);
            }
            return View("MyIndex");
        }
        
        // Удаление задачи
        //[Authorize]
        public ActionResult MyDelete(int id)
        {
            MathTask request = db.Requests.Find(id);
            // получаем текущего пользователя
            var curId = HttpContext.User.Identity.GetUserId();
            ApplicationUser user = db.Users.First(m => m.Id == curId);
            if (request != null && request.Author.Id == user.Id)
            {
                db.SaveChanges();
            }
            return RedirectToAction("MyIndex");
        }

        

        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <returns></returns>
        public FileResult Download(int id)
        {
            var req = db.Requests.Find(id);
            var reqDoc = req.Document;
            
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Files/RequestFiles/" + reqDoc.Url));
                string fileName = req.Id + reqDoc.Type;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        
        /// <summary>
        /// Просмотр подробных сведений о задаче
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            MathTask request = db.Requests.Find(id);

            if (request != null)
            {
                //получаем категорию
                return PartialView("_Details", request);
            }
            return View("Index");
        }

        //[Authorize]
        public ActionResult Author(string id)
        {
            ApplicationUser executor = db.Users.Where(m => m.Id == id).First();

            if (executor != null)
            {
                return PartialView("_Executor", executor);
            }
            return View("Index");
        }

        //[Authorize]
        public ActionResult Executor(string id)
        {
            ApplicationUser executor = db.Users.Where(m => m.Id == id).First();

            if (executor != null)
            {
                return PartialView("_Executor", executor);
            }
            return View("Index");
        }

        //[Authorize]
        public ActionResult Lifecycle(int id)
        {
            return View("Index");
        }

        // Удаление задачи
        //[Authorize]
        public ActionResult Delete(int id)
        {
            MathTask request = db.Requests.Find(id);
            var curId = HttpContext.User.Identity.GetUserId();
            // получаем текущего пользователя
            ApplicationUser user = db.Users.First(m => m.Id == curId);
            if (request != null && request.Author.Id == user.Id)
            {
                db.Requests.Remove(request);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Статистика
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCountOfAllRequests()
        {
            string count = db.Requests.Count().ToString();
            ViewBag.Message = count;
            return PartialView("Message");
        }

        public ActionResult GetCountOfAllUsers()
        {
            string count = db.Users.Count().ToString();
            ViewBag.Message = count;
            return PartialView("Message");
        }
        
        [HttpPost]
        //[Authorize(Roles = "Модератор")]
        //[Authorize(Roles = "Administrator")]
        //[Authorize]
        public ActionResult MySelfDistribute(int? requestId, string executorId)
        {
            if (requestId == null && executorId.IsEmpty())// == null)
            {
                return RedirectToAction("MyIndex");
            }
            MathTask req = db.Requests.Find(requestId);
            ApplicationUser ex = db.Users.Find(executorId);
            if (req == null && ex == null)
            {
                return RedirectToAction("MyIndex");
            }

            req.Status = (int)RequestStatus.Distributed;

            db.Entry(req).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("MyIndex");
        }
    }



}
