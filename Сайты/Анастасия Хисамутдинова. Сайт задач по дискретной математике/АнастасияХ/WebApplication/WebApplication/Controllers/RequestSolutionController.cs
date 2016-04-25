namespace WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using WebApplication.Models;
    using Microsoft.AspNet.Identity;

    public class RequestSolutionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Получаем вообще все решения к моим задачам (администратор-преподаватель)
        public async Task<ActionResult> Index()
        {
            var curId = this.User.Identity.GetUserId();

            var all = db.RequestSolutions.Count();

            var requestSolutions = db.RequestSolutions
                .Include(r => r.Author)
                .Include(r => r.Document)
                .Include(r => r.Request)
                .Where(r=>r.Request.AuthorId==curId)
                .Where(r=>!r.IsRightSolution);// Не выводим те правильные решения, которые загрузил этот пользователь
            return View(await requestSolutions.ToListAsync());
        }
        
        // Получаем решения текущего пользователя (студент-преподаватель)
        public async Task<ActionResult> MyIndex()
        {
            var curId = this.User.Identity.GetUserId();

            var requestSolutions = db.RequestSolutions
                .Include(r => r.Author)
                .Include(r => r.Document)
                .Include(r => r.Request)//                .Where(r => !r.IsRightSolution)
                .Where(r=>r.Author.Id== curId);
            return View(await requestSolutions.ToListAsync());
        }

        //
        public ActionResult MyRightSolutions()
        {
            var curId = this.User.Identity.GetUserId();
            var requestSolutions = db.RequestSolutions
                .Include(r => r.Author)
                .Include(r => r.Document)
                .Include(r => r.Request).ToList();
            var res=requestSolutions
                .Where(r => r.Author.Id == curId);

            return View(res);
        }

        // GET: RequestSolution/Create
        public ActionResult Create()
        {
            var curId = this.User.Identity.GetUserId();

            // Выбираем те задачи, которые данные пользователь ещё не решал
            ViewBag.Requests = new SelectList(db.Requests.Where(x => x.Executors.Count(y=>y.Id== curId)==0), "Id", "Name");
            return View();
        }

        // POST: RequestSolution/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Comment,RequestId")] RequestSolution requestSolution, HttpPostedFileBase error)
        {
            var curId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                // если получен файл
                var current = DateTime.Now;
                var user = db.Users.Find(curId);

                if (error != null)
                {
                    Document doc = new Document();
                    doc.Size = error.ContentLength;
                    // Получаем расширение
                    string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                    doc.Type = ext;
                    // сохраняем файл по определенному пути на сервере
                    string path = current.ToString(user.Id.GetHashCode() + "dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                    error.SaveAs(Server.MapPath("~/Files/RequestSolutionFiles/" + path));
                    doc.Url = path;

                    requestSolution.Document = doc;
                    db.Documents.Add(doc);
                }
                else
                    requestSolution.Document = null;

                var req = db.Requests.Find(requestSolution.RequestId);
                requestSolution.Request = req;
                requestSolution.Author = user;
                requestSolution.AuthorId = user.Id;
                requestSolution.Right = false;
                requestSolution.Date = DateTime.Now;

                // Отметим, что решение относится к этой задаче
                req.RequestSolutions.Add(requestSolution);
                req.Executors.Add(user);
                db.Entry(req).State = EntityState.Modified;

                db.RequestSolutions.Add(requestSolution);

                db.SaveChanges();
                return RedirectToAction("MyIndex");
            }

            ViewBag.Requests = new SelectList(db.Requests.Where(x => x.Executors.Count(y => y.Id == curId) == 0), "Id", "Name");
            return View(requestSolution);
        }
        
        // GET: RequestSolution/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            RequestSolution requestSolution = await db.RequestSolutions.FindAsync(id);

            // Нельзя удалить уже проверенное решение 
            if (requestSolution.IsChecked)
            {
                return RedirectToAction("MyIndex");
            }
            // Удалим информацию, что данный пользователь уже решал данную задачу.
            var req = db.Requests.Find(requestSolution.RequestId);
            var curId = this.User.Identity.GetUserId();
            var user = db.Users.Find(curId);
            req.Executors.Remove(user);
            req.RequestSolutions.Remove(requestSolution);

            db.Entry(req).State = EntityState.Modified;;
            db.RequestSolutions.Remove(requestSolution);
            await db.SaveChangesAsync();
            return RedirectToAction("MyIndex");
        }
        
        [HttpPost]
        //[Authorize]
        public ActionResult DistributeChangeStatus(int requestSolutionId, int status)
        {
            var curId = HttpContext.User.Identity.GetUserId();
            ApplicationUser user = db.Users.First(m => m.Id == curId);

            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }

            RequestSolution req = db.RequestSolutions.Find(requestSolutionId);
            switch (status)
            {
                case 0:
                    {
                        req.IsChecked = false;
                        req.Right = false;
                        break;
                    }
                case 1:
                    {
                        req.IsChecked = true;
                        req.Right = true;
                        break;
                    }
                case 2:
                    {
                        req.IsChecked = true;
                        req.Right = false;
                        break;
                    }
            }


            if (req != null)
            {
                db.Entry(req).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Загрузка правильного решения

        // GET: RequestSolution/Create
        public ActionResult CreateRightSolution(string id)
        {
            // Выбираем те задачи, для которых ещё не загружено правильное решение
            var curReqId = int.Parse(id);
            ViewBag.Requests = new SelectList(db.Requests.Where(x=>x.Id== curReqId), "Id", "Name");
            return PartialView("_CreateRightSolution");
        }

        // POST: RequestSolution/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRightSolution([Bind(Include = "Id,Name,Comment,RequestId")] RequestSolution requestSolution, HttpPostedFileBase error)
        {
            var curId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                // если получен файл
                var current = DateTime.Now;
                var user = db.Users.Find(curId);

                if (error != null)
                {
                    Document doc = new Document();
                    doc.Size = error.ContentLength;
                    // Получаем расширение
                    string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                    doc.Type = ext;
                    // сохраняем файл по определенному пути на сервере
                    string path = current.ToString(user.Id.GetHashCode() + "dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                    error.SaveAs(Server.MapPath("~/Files/RequestSolutionFiles/" + path));
                    doc.Url = path;

                    requestSolution.Document = doc;
                    db.Documents.Add(doc);
                }
                else
                    requestSolution.Document = null;

                var req = db.Requests.Find(requestSolution.RequestId);
                requestSolution.Request = req;
                requestSolution.Author = user;
                requestSolution.AuthorId = user.Id;
                requestSolution.Right = false;
                requestSolution.Date = DateTime.Now;
                requestSolution.IsRightSolution = true;

                // Отметим, что решение относится к этой задаче
                req.RequestSolutions.Add(requestSolution);
                req.Executors.Add(user);

                // Поставим как правильное решение
                req.RightRequestSolution = requestSolution;
                req.RightRequestSolutionId = requestSolution.Id;

                db.Entry(req).State = EntityState.Modified;

                db.RequestSolutions.Add(requestSolution);

                db.SaveChanges();
                return RedirectToAction("MyRightSolutions");
            }


            return RedirectToAction("MyRightSolutions");
        }

        public async Task<ActionResult> DeleteRightSolution(int? id)
        {
            RequestSolution requestSolution = await db.RequestSolutions.FindAsync(id);

            // Нельзя удалить уже проверенное решение 
            if (requestSolution.IsChecked)
            {
                return RedirectToAction("MyRightSolutions");
            }
            // Удалим информацию, что данный пользователь уже решал данную задачу.
            var req = db.Requests.Find(requestSolution.RequestId);
            var curId = this.User.Identity.GetUserId();
            var user = db.Users.Find(curId);

            req.Executors.Remove(user);
            req.RequestSolutions.Remove(requestSolution);

            req.RightRequestSolution = null;

            db.Entry(req).State = EntityState.Modified; ;
            db.RequestSolutions.Remove(requestSolution);
            await db.SaveChangesAsync();
            return RedirectToAction("MyRightSolutions");
        }

        public ActionResult ShowRightSolution(string id)
        {
            var curId = int.Parse(id);
            var reqSol = db.RequestSolutions.Find(curId);
            var idReq = reqSol.Request.Id;
            var model = db.Requests.Find(idReq).RightRequestSolution;

            return PartialView("_ShowRightSolution", model);
        }
    }
}
