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

namespace WebApplication.Controllers
{
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public FileResult DownloadErrorMessageFile(int id)
        {
            var reqDoc = db.Documents.Find(id);//.Document;

            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Files/ErrorMessageFiles/" + reqDoc.Url));
            string fileName = reqDoc.Id + reqDoc.Type;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
