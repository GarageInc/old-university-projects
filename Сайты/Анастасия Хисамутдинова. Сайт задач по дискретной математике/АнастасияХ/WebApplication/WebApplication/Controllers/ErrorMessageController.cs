using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using Microsoft.AspNet.Identity;
using System.Web.WebPages;

namespace WebApplication.Controllers
{
    public class ErrorMessageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ErrorMessage
        public async Task<ActionResult> Index()
        {

            var errorMessages = db.ErrorMessages
                .Include(e => e.Author)
                .Include(r=>r.Document)
                .OrderByDescending(r => r.CreateDate);
            var errorStatus = new[] { new { Id = 0, Name = "Открыто" }, new { Id = 1, Name = "Закрыто" } };
            ViewBag.ErrorStatus = new SelectList(errorStatus, "Id", "Name");

            return View(await errorMessages.ToListAsync());
        }

        // GET: ErrorMessage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorMessage errorMessage = await db.ErrorMessages.FindAsync(id);
            if (errorMessage == null)
            {
                return HttpNotFound();
            }
            return View(errorMessage);
        }
        

        // POST: ErrorMessage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            ErrorMessage errorMessage = await db.ErrorMessages.FindAsync(id);
            db.ErrorMessages.Remove(errorMessage);
            await db.SaveChangesAsync();
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
        
        // Создание новой задачи об ошибке
        [HttpPost]
        public void Create(string forAdm, HttpPostedFileBase error)
        {
            var uploadText = Request.Params["Text"];
            var curId = HttpContext.User.Identity.GetUserId();
            // получаем текущего пользователя
            ApplicationUser user = db.Users.FirstOrDefault(m => m.Id == curId);
            ErrorMessage erM;
            if (user != null)
            {
                erM= new ErrorMessage
                {
                    Author = user,
                    AuthorId = user.Id,
                    CreateDate = DateTime.Now,
                    ErrorStatus = 0
                };
            }
            else
            {
                erM = new ErrorMessage
                {
                    CreateDate = DateTime.Now,
                    ErrorStatus = 0
                };
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
                string path = current.ToString(user.Id.GetHashCode()+"dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                error.SaveAs(Server.MapPath("~/Files/ErrorMessageFiles/" + path));
                doc.Url = path;

                erM.Document = doc;
                db.Documents.Add(doc);
            }
            else
                erM.Document = null;

            if (uploadText == null)
                erM.Text = "";
            else erM.Text = uploadText;

            if (forAdm == "1")
            {
                erM.ForAdministration = true;
                var email = Request.Params["email"].ToString();
                erM.Email = email;
            }
            else
                erM.ForAdministration = false;

            //var cat = db.Categories.Find(request.CategoryId);
            //request.Category = cat;
            //request.Comment = "";

            // Добавляем задачу с возможно приложенными документами
            db.ErrorMessages.Add(erM);
            user.ErrorMessages.Add(erM);
            db.Entry(user).State = EntityState.Modified;
            
            db.SaveChanges();
            Response.Redirect(Request.UrlReferrer.AbsoluteUri);                
        }


        /// <summary>
        /// Редактирование статусов ошибок
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = "Модератор")]
        //[Authorize(Roles = "Administrator")]
        [Authorize]
        public ActionResult ChangeErrorMessageStatus(int? errorMessageId, string errorStatusId)
        {
            if (errorMessageId == null && errorStatusId.IsEmpty())// == null)
            {
                return RedirectToAction("Index");
            }
            ErrorMessage req = db.ErrorMessages.Find(errorMessageId);
            ApplicationUser ex = db.Users.Find(errorStatusId);

            var erSt = int.Parse(errorStatusId);

            if (req == null && ex == null)
            {
                return RedirectToAction("Index");
            }
            req.ErrorStatus = erSt;
            db.Entry(req).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
