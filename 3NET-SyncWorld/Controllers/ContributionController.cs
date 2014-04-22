using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using _3NET_SyncWorld.Models;

namespace _3NET_SyncWorld.Controllers
{
    [Authorize]
    public class ContributionController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Contribution/
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            IQueryable<Contribution> contributions =
                db.Contributions.Where(c => c.UserId.Equals(userId)).Include(c => c.Event).Include(c => c.Type);
            return View(contributions.ToList());
        }

        // GET: /Contribution/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contribution contribution = db.Contributions.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            return View(contribution);
        }

        // GET: /Contribution/Create/1
        public ActionResult Create(int? id)
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

            ViewBag.TypeId = new SelectList(db.ContributionTypes, "Id", "Label");
            return View();
        }

        // POST: /Contribution/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, [Bind(Include = "Id,Amount,TypeId")] Contribution contribution)
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
                contribution.UserId = User.Identity.GetUserId();
                contribution.EventId = (int) id;
                db.Contributions.Add(contribution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", contribution.EventId);
            ViewBag.TypeId = new SelectList(db.ContributionTypes, "Id", "Label", contribution.TypeId);
            return View(contribution);
        }

        // GET: /Contribution/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contribution contribution = db.Contributions.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", contribution.EventId);
            ViewBag.TypeId = new SelectList(db.ContributionTypes, "Id", "Label", contribution.TypeId);
            return View(contribution);
        }

        // POST: /Contribution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount,EventId,UserUserId,TypeId")] Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contribution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", contribution.EventId);
            ViewBag.TypeId = new SelectList(db.ContributionTypes, "Id", "Label", contribution.TypeId);
            return View(contribution);
        }

        // GET: /Contribution/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contribution contribution = db.Contributions.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            return View(contribution);
        }

        // POST: /Contribution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contribution contribution = db.Contributions.Find(id);
            db.Contributions.Remove(contribution);
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