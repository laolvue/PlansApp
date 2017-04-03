using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlansApp.Models;

namespace PlansApp.Controllers
{
    public class RecipientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Recipients
        public ActionResult Index()
        {
            var recipients = db.Recipients.Include(r => r.RecipientCategory);
            return View(recipients.ToList());
        }

        // GET: Recipients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipient recipient = db.Recipients.Find(id);
            if (recipient == null)
            {
                return HttpNotFound();
            }
            return View(recipient);
        }

        // GET: Recipients/Create
        public ActionResult Create()
        {
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName");
            return View();
        }

        // POST: Recipients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recipientId,nickName,firstName,lastName,UserEmail,emailAddress,recipientCategoryId")] Recipient recipient)
        {
            if (ModelState.IsValid)
            {
                Recipient recipientf = new Recipient
                {
                    UserEmail = User.Identity.Name,
                    emailAddress = recipient.emailAddress,
                    firstName = recipient.firstName,
                    lastName = recipient.lastName,
                    nickName = recipient.nickName,
                    recipientCategoryId = recipient.recipientCategoryId

                };
                db.Recipients.Add(recipientf);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", recipient.recipientCategoryId);
            return View(recipient);
        }

        // GET: Recipients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipient recipient = db.Recipients.Find(id);
            if (recipient == null)
            {
                return HttpNotFound();
            }
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", recipient.recipientCategoryId);
            return View(recipient);
        }

        // POST: Recipients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recipientId,nickName,firstName,lastName,UserEmail,emailAddress,recipientCategoryId")] Recipient recipient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", recipient.recipientCategoryId);
            return View(recipient);
        }

        // GET: Recipients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipient recipient = db.Recipients.Find(id);
            if (recipient == null)
            {
                return HttpNotFound();
            }
            return View(recipient);
        }

        // POST: Recipients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipient recipient = db.Recipients.Find(id);
            db.Recipients.Remove(recipient);
            db.SaveChanges();
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
    }
}
