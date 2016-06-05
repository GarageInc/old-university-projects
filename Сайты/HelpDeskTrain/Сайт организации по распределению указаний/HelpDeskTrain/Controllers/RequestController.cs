using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Web;
using System.Web.Mvc;
using HelpDeskTrain.Models;

namespace HelpDeskTrain.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        HelpdeskContext db = new HelpdeskContext();

        [HttpGet]
        public ActionResult Create()
        {
             // получаем текущего пользователя
             User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
             if (user != null)
             {
                    // получаем набор кабинетов для департамента, в котором работает пользователь
                    var cabs = from cab in db.Activs
                               where cab.DepartmentId == user.DepartmentId
                               select cab;
                    ViewBag.Cabs = new SelectList(cabs, "Id", "CabNumber");

                    ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");

                    return View();
              }
              return RedirectToAction("LogOff", "Account");
        }

        // Создание новой заявки
        [HttpPost]
        public ActionResult Create(Request request, HttpPostedFileBase error)
        {
            // получаем текущего пользователя
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if(user==null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            if (ModelState.IsValid)
            {
                // указываем статус Открыта у заявки
                request.Status = (int)RequestStatus.Open;
                //получаем время октрытия
                DateTime current = DateTime.Now;

                //Создаем запись о жизненом цикле заявки
                Lifecycle newLifecycle = new Lifecycle() { Opened = current };
                request.Lifecycle = newLifecycle;

                //Добавляем жизенный цикл заявки
                db.Lifecycles.Add(newLifecycle);

                // указываем пользователя заявки
                request.UserId = user.Id;

               // если получен файл
               if (error != null)
               {
                        
                    // Получаем расширение
                    string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                    // сохраняем файл по определенному пути на сервере
                    string path = current.ToString("dd.mm.yyyy hh:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                    error.SaveAs(Server.MapPath("~/Files/" + path));
                    request.File = path;
                }
                //Добавляем заявку
                db.Requests.Add(request);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
                return View(request);
        }

        public ActionResult Index()
        {
            // получаем текущего пользователя
            User user = db.Users.Where(m=> m.Login==HttpContext.User.Identity.Name).First();
            
            var requests = db.Requests.Where(r=>r.UserId==user.Id) //получаем заявки для текущего пользователя
                                        .Include(r => r.Category)  // добавляем категории
                                        .Include(r => r.Lifecycle)  // добавляем жизненный цикл заявок
                                        .Include(r => r.User)         // добавляем данные о пользователях
                                        .OrderByDescending(r=>r.Lifecycle.Opened); // упорядочиваем по дате по убыванию   
         
            return View(requests.ToList());
        }

        // Удаление заявки
        public ActionResult Delete(int id)
        {
            Request request = db.Requests.Find(id);
            // получаем текущего пользователя
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).First();
            if (request != null && request.UserId==user.Id)
            {
                Lifecycle lifecycle = db.Lifecycles.Find(request.LifecycleId);
                db.Lifecycles.Remove(lifecycle);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Просмотр подробных сведений о заявке
        public ActionResult Details(int id)
        {
            Request request = db.Requests.Find(id);

            if (request != null)
            {
                //получаем кабинет
                var activ = db.Activs.Where(m => m.Id == request.ActivId);
                if (activ.Count()>0)
                {
                    request.Activ = activ.First();
                }
                //получаем категорию
                request.Category = db.Categories.Where(m => m.Id == request.CategoryId).First();
                return PartialView("_Details", request);

            }
            return View("Index");
        }

        // Просмотр исполнителя
        public ActionResult Executor(int id)
        {
            User executor = db.Users.Where(m => m.Id == id).First();

            if (executor != null)
            {
                return PartialView("_Executor", executor);
            }
            return View("Index");
        }

        // Просмотр жизненого цикла заявки
        public ActionResult Lifecycle(int id)
        {
            Lifecycle lifecycle = db.Lifecycles.Where(m => m.Id == id).First();

            if (lifecycle != null)
            {
                return PartialView("_Lifecycle", lifecycle);
            }
            return View("Index");
        }

        // просмотр администратором всех заявок
        [Authorize(Roles="Администратор")]
        public ActionResult RequestList()
        {
            var requests = db.Requests.Include(r => r.Category)  // добавляем категории
                                        .Include(r => r.Lifecycle)  // добавляем жизненный цикл заявок
                                        .Include(r => r.User)         // добавляем данные о пользователях
                                        .OrderByDescending(r => r.Lifecycle.Opened); // упорядочиваем по дате по убыванию   

            return View(requests.ToList());
        }

        // загружаем файл
        public ActionResult Download(int id)
        {
            Request r = db.Requests.Find(id);
            if (r != null)
            {
                string filename = Server.MapPath("~/Files/" + r.File);
                string contentType = "image/jpeg";

                string ext = filename.Substring(filename.LastIndexOf('.'));
                switch (ext)
                {
                    case "txt":
                        contentType = "text/plain";
                        break;
                    case "png":
                        contentType = "image/png";
                        break;
                    case "tiff":
                        contentType = "image/tiff";
                        break;
                }
                return File(filename, contentType, filename);
            }

            return Content("Файл не найден");
        }


        //Заявки для использования диспетчером
        [HttpGet]
        [Authorize(Roles="Модератор")]
        public ActionResult Distribute()
        {
            var requests = db.Requests.Include(r => r.User)
                                    .Include(r=>r.Lifecycle)
                                    .Include(r=>r.Executor)
                                    .Where(r=>r.ExecutorId==null)
                                    .Where(r=>r.Status!=(int)RequestStatus.Closed);
            List<User> executors = db.Users.Include(e=>e.Role).Where(e=>e.Role.Name=="Исполнитель").ToList<User>();
            
            ViewBag.Executors = new SelectList(executors, "Id", "Name");
            return View(requests);
        }

        [HttpPost]
        [Authorize(Roles = "Модератор")]
        public ActionResult Distribute(int? requestId, int? executorId)
        {
            if (requestId == null && executorId == null)
            {
               return RedirectToAction("Distribute");
            }
            Request req = db.Requests.Find(requestId);
            User ex = db.Users.Find(executorId);
            if(req==null && ex==null)
            {
                return RedirectToAction("Distribute");
            }
            req.ExecutorId = executorId;
            
             req.Status = (int)RequestStatus.Distributed;
             Lifecycle lifecycle = db.Lifecycles.Find(req.LifecycleId);
             lifecycle.Distributed = DateTime.Now;
             db.Entry(lifecycle).State = EntityState.Modified;
            
            db.Entry(req).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Distribute");
        }

        //Заявки для изменения статуса исполнителем
        [HttpGet]
        [Authorize(Roles = "Исполнитель")]
        public ActionResult ChangeStatus()
        {
            // получаем текущего пользователя
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).First();
            if(user!=null)
            {
                var requests = db.Requests.Include(r => r.User)
                                    .Include(r => r.Lifecycle)
                                    .Include(r => r.Executor)
                                    .Where(r => r.ExecutorId == user.Id)
                                    .Where(r=>r.Status!=(int)RequestStatus.Closed);
                return View(requests);
            }
            return RedirectToAction("LogOff", "Account");
        }

        [HttpPost]
        [Authorize(Roles = "Исполнитель")]
        public ActionResult ChangeStatus(int requestId, int status)
        {
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).First();
            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            
            Request req = db.Requests.Find(requestId);
            if (req != null)
            {
                req.Status = status;
                Lifecycle lifecycle = db.Lifecycles.Find(req.LifecycleId);
                if (status == (int)RequestStatus.Proccesing)
                {
                    lifecycle.Proccesing = DateTime.Now;
                }
                else if (status == (int)RequestStatus.Checking)
                {
                    lifecycle.Checking = DateTime.Now;
                }
                else if (status == (int)RequestStatus.Closed)
                {
                    lifecycle.Closed = DateTime.Now;
                }
                db.Entry(lifecycle).State = EntityState.Modified;
                db.Entry(req).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ChangeStatus");
        }

    }
}
