using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlansApp.Models;
using Postal;
using System.Activities.Statements;

namespace PlansApp.Controllers
{
    public class PlansMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlansMessages
        public ActionResult Index()
        {
            var plansMessages = db.PlansMessages.Include(p => p.Recipient).Include(p => p.RecipientCategory);
            return View(plansMessages.ToList());
        }

        // GET: PlansMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlansMessage plansMessage = db.PlansMessages.Find(id);
            if (plansMessage == null)
            {
                return HttpNotFound();
            }
            return View(plansMessage);
        }

        // GET: PlansMessages/Create
        public ActionResult Create()
        {
            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName");
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName");
            return View();
        }

        // POST: PlansMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "plansMessageId,recipientCategoryId,recipientId,Location,introMessage,closingMessage,planDate")] PlansMessage model)
        {
            if (ModelState.IsValid)
            {
                db.PlansMessages.Add(model);
                db.SaveChanges();

                var emailViewModel = new PlansEmailViewModel()
                {
                    planDate = model.planDate,
                    introMessage = model.introMessage,
                    closingMessage = model.closingMessage,
                    Location = model.Location
                };

                if (model.RecipientCategory != null)
                {
                    //select all recipients with a category id == 
                    var recipientsInCategory = from recipient in db.Recipients
                                               where recipient.recipientCategoryId == model.recipientCategoryId
                                               select recipient;
                    foreach (var recipient in recipientsInCategory)
                    {
                        emailViewModel.recipients.Add(recipient);
                    }

                }
                else
                {
                    //select from recipients where recipientId==Id
                    var singleRecipient =
                        from recipient in db.Recipients
                        where recipient.UserEmail == model.Recipient.UserEmail
                        select recipient;
                    emailViewModel.recipients.Add(singleRecipient.FirstOrDefault());
                }
                for (int i = 0; i < emailViewModel.recipients.Count; i++)
                {
                    var email = new PlansEmail()
                    {
                        recipient=emailViewModel.recipients[i],
                        planDate = model.planDate,
                        introMessage = model.introMessage,
                        closingMessage = model.closingMessage,
                        Location = model.Location
                    };
                    email.Send();
                }
            }


            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName", model.recipientId);
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", model.recipientCategoryId);
            return RedirectToAction("Index");
           
        }

        // GET: PlansMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlansMessage plansMessage = db.PlansMessages.Find(id);
            if (plansMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName", plansMessage.recipientId);
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", plansMessage.recipientCategoryId);
            return View(plansMessage);
        }

        // POST: PlansMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "plansMessageId,recipientCategoryId,recipientId,Location,introMessage,closingMessage,planDate")] PlansMessage plansMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plansMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName", plansMessage.recipientId);
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", plansMessage.recipientCategoryId);
            return View(plansMessage);
        }

        // GET: PlansMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlansMessage plansMessage = db.PlansMessages.Find(id);
            if (plansMessage == null)
            {
                return HttpNotFound();
            }
            return View(plansMessage);
        }

        // POST: PlansMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlansMessage plansMessage = db.PlansMessages.Find(id);
            db.PlansMessages.Remove(plansMessage);
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
