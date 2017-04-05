using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlansApp.Models;
using Hangfire;
using Postal;


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
        public ActionResult Create([Bind(Include = "checkInMessageId,recipientCategoryId,recipientId,Location,introMessage,closingMessage,deadline")] CheckInMessage model)
        {
            if (ModelState.IsValid)
            {
                //hard-coding some data to test email sending functionality
                model.closingMessage = "bye";
                model.introMessage = "hi";
                model.deadline = DateTime.Today;
                model.Location = "Madrid";
                model.planDate = DateTime.Today;
                Recipient testingRecipient = new Recipient();
                model.Recipient = testingRecipient;
                model.Recipient.recipientId = 1;
                model.Recipient.emailAddress = "PotassiumSubscriber1@gmail.com";
                model.Recipient.UserEmail = "ian.stew12 @gmail.com";
                model.Recipient.nickName = "LilRecipient";
                

                db.CheckInMessages.Add(model);
                db.SaveChanges();

                var emailViewModel = new CheckInEmailViewModel()
                {
                    planDate = model.planDate,
                    introMessage = model.introMessage,
                    closingMessage = model.closingMessage,
                    Location = model.Location
                };

                if (model.RecipientCategory != null)
                {
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
                    var singleRecipient = testingRecipient;     //delete after = to undo hard-coding
                    //from recipient in db.Recipients
                    //where recipient.UserEmail == model.Recipient.UserEmail
                    //select recipient;
                    emailViewModel.recipients = new List<Recipient>();
                    emailViewModel.recipients.Add(singleRecipient);//.FirstOrDefault());
                }
                for (int i = 0; i < emailViewModel.recipients.Count; i++)
                {
                    var email = new CheckInEmail()
                    {
                        recipient = emailViewModel.recipients[i],
                        planDate = model.planDate,
                        introMessage = model.introMessage,
                        closingMessage = model.closingMessage,
                        Location = model.Location,
                        deadline= model.deadline
                    };
                    //var jobId = BackgroundJob.Schedule(() => email.Send(), (email.deadline - DateTime.Now));
                    email.Send();
                }

                

                return RedirectToAction("Index");
            }

            ViewBag.recipientId = new SelectList(db.Recipients, "recipientId", "nickName", model.recipientId);
            ViewBag.recipientCategoryId = new SelectList(db.RecipientCategories, "recipientCategoryId", "categoryName", model.recipientCategoryId);
            return View(model);
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


        //public ActionResult CheckIn()
        //{
        //    //Get User Id, Delete any pending Check-in messasges
        //}
    }
}
