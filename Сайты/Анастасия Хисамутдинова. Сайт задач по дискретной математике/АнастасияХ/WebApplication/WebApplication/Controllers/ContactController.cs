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
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // POST: Contacts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ContactAdress")] Contact contact)
        {
            if (ModelState.IsValid)
            {

                var curId = this.User.Identity.GetUserId();
                var user = db.Users.Find(curId);
                contact.Author = user;
                contact.AuthorId = user.Id;

                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Manage");
            }

            return RedirectToAction("Index","Manage");
        }
       
        
        // POST: Contacts/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Contact contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Manage");
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
