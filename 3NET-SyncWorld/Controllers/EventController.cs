using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using _3NET_SyncWorld.Models;

namespace _3NET_SyncWorld.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Event/
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var events = from e in db.Events
                where e.StatusId.Equals(1) && (e.CreatorId.Equals(userId) || e.InvitedUsers.Any(u => u.Id.Equals(userId)))
                select e;
            events = events.Include(e => e.Status).Include(e => e.Type);
            return View(events.ToList());
        }

        // GET: /Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: /Event/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.EventTypes, "Id", "Label");
            return View();
        }

        public ActionResult InviteFriend(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.FriendUserId = new SelectList(db.Users.Find(User.Identity.GetUserId()).Friends, "Id", "UserName");
            return View();
        }

        // POST: /Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InviteFriend(int? id, [Bind(Include = "FriendUserId")] InviteFriendViewModel @friend)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                @event.InvitedUsers.Add(db.Users.Find(@friend.FriendUserId));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FriendUserId = new SelectList(db.Users.Find(User.Identity.GetUserId()).Friends, "Id", "UserName");
            return View(@friend);
        }

        // POST: /Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Address,Date,Summary,TypeId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.CreatorId = User.Identity.GetUserId();
                @event.StatusId = 1; //Set to 2 by default if you have admin page
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.EventTypes, "Id", "Label", @event.TypeId);
            return View(@event);
        }

        // GET: /Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.EventTypes, "Id", "Label", @event.TypeId);
            return View(@event);
        }

        // POST: /Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Address,Date,Summary,TypeId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Entry(@event);
                entity.State = EntityState.Modified;
                entity.Property("StatusId").IsModified = false;
                entity.Property("CreatorId").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.EventTypes, "Id", "Label", @event.TypeId);
            return View(@event);
        }

        // GET: /Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: /Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
