using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RecallMessageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            using (db)
            {
                var comms = db.RecallMessages
                        .Where(x => !x.IsDeleted)
                        .Include(x => x.Author);

                RecallMessageList model = new RecallMessageList()
                {
                    RecallMessages = comms.ToArray()
                };
                
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Add(int? parentId, string Text)
        {
            using (db)
            {
                var curId = this.User.Identity.GetUserId();
                var user = db.Users.Find(curId);

                var newRecallMessages = new RecallMessage()
                {
                    ParentId = parentId,
                    Text = Text,
                    Author = user,
                    AuthorId = user.Id,
                    AddDateTime = DateTime.Now,
                    Karma = 0,
                    IsDeleted = false,
                    AboutSite=true
                };
                
                db.RecallMessages.Add(newRecallMessages);
                db.SaveChanges();
            }

            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public void Move(int nodeId, int? newParentId)
        {
            if (nodeId == newParentId)
            {
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            using (db)
            {
                if (newParentId.HasValue && ContainsChilds(db, nodeId, newParentId.Value))
                {
                    Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                }
                var node = db.RecallMessages.Where(x => x.Id == nodeId).Single();
                node.ParentId = newParentId;
                db.SaveChanges();
            }
            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        private bool ContainsChilds(ApplicationDbContext db, int parentId, int id)
        {
            bool result = false;
            var inner = db.RecallMessages.Where(x => x.ParentId == parentId && !x.IsDeleted).ToArray();
            foreach (var node in inner)
            {
                if (node.Id == id && node.ParentId == parentId)
                {
                    return true;
                }
                result = ContainsChilds(db, node.Id, id);
            }
            return result;
        }

        public void Delete(string id)
        {
            using (db)
            {
                DeleteNodes(db, int.Parse(id));
                db.SaveChanges();
            }

            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        private void DeleteNodes(ApplicationDbContext db, int id)
        {
            var inner = db.RecallMessages.Where(x => x.ParentId == id && !x.IsDeleted).ToArray();
            foreach (var node in inner)
            {
                node.IsDeleted = true;
                DeleteNodes(db, node.Id);
            }
            var deleted = db.RecallMessages.Where(x => x.Id == id && !x.IsDeleted).Single();
            deleted.IsDeleted = true;
        }

        public void UpRecallMessage(int id)
        {
            var curId = this.User.Identity.GetUserId();
            var user = db.Users.Find(curId);

            var com = db.RecallMessages.Find(id);

            if (!user.UpRecalls.Where(x => x.Id == id).Any())
            {
                com.Karma++;

                user.UpRecalls.Add(com);
                user.DownRecalls.Remove(com);

                db.Entry(user).State = EntityState.Modified; ;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();

            }

            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public void DownRecallMessage(int id)
        {
            var curId = this.User.Identity.GetUserId();
            var user = db.Users.Find(curId);

            var com = db.RecallMessages.Find(id);

            if (!user.DownRecalls.Where(x => x.Id == id).Any())
            {
                com.Karma--;

                user.DownRecalls.Add(com);
                user.UpRecalls.Remove(com);

                db.Entry(user).State = EntityState.Modified; ;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();

            }

            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
