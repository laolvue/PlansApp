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
    public class CheckInMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CheckInMessages
        public ActionResult Index()
        {
            var checkInMessages = db.CheckInMessages.Include(c => c.Recipient).Include(c => c.RecipientCategory);
            return View(checkInMessages.ToList());
        }

        // GET: CheckInMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckInMessage checkInMessage = db.CheckInMessages.Find(id);
            if (checkInMessage == null)
            {
                return HttpNotFound();
            }
            return View(checkInMessage);
        }

        // GET: CheckInMessages/Create
        public ActionResult Create()
        {
            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName");
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName");
            return View();
        }

        // POST: CheckInMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "checkInMessageId,recipientCategoryId,recipientId,Location,introMessage,closingMessage,deadline")] CheckInMessage checkInMessage)
        {
            if (ModelState.IsValid)
            {
                db.CheckInMessages.Add(checkInMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName", checkInMessage.recipientId);
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", checkInMessage.recipientCategoryId);
            return View(checkInMessage);
        }

        // GET: CheckInMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckInMessage checkInMessage = db.CheckInMessages.Find(id);
            if (checkInMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName", checkInMessage.recipientId);
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", checkInMessage.recipientCategoryId);
            return View(checkInMessage);
        }

        // POST: CheckInMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "checkInMessageId,recipientCategoryId,recipientId,Location,introMessage,closingMessage,deadline")] CheckInMessage checkInMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkInMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName", checkInMessage.recipientId);
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", checkInMessage.recipientCategoryId);
            return View(checkInMessage);
        }

        // GET: CheckInMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckInMessage checkInMessage = db.CheckInMessages.Find(id);
            if (checkInMessage == null)
            {
                return HttpNotFound();
            }
            return View(checkInMessage);
        }

        // POST: CheckInMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckInMessage checkInMessage = db.CheckInMessages.Find(id);
            db.CheckInMessages.Remove(checkInMessage);
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
