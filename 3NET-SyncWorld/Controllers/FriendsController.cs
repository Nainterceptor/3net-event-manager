using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using _3NET_SyncWorld.Models;

namespace _3NET_SyncWorld.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Friends/
        public ActionResult Index()
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            return View(user.Friends.ToList());
        }

        // GET: /Friends/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: /Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "LookingFor")] AddFriendViewModel addFriendViewModel)
        {
            if (!ModelState.IsValid) return View();
            IQueryable<ApplicationUser> users = from u in db.Users
                where u.UserName.Equals(addFriendViewModel.LookingFor) || u.Name.Equals(addFriendViewModel.LookingFor)
                select u;
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            ApplicationUser user = null;
            if (users.Any())
                user = users.First();
            if (user == null)
            {
                ViewData["error"] = "User not exist";
                return View();
            }
            if (user.Id == currentUser.Id)
            {
                ViewData["error"] = "User can't be yourself";
                return View();
            }
            if (currentUser.Friends.ToList().Any(u => u.Id.Equals(user.Id)))
            {
                ViewData["error"] = "User already in your friends";
                return View();
            }
            currentUser.Friends.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Friends/Remove/5
        public ActionResult Remove(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            ApplicationUser user = db.Users.Find(id.ToString());

            if (user == null || !currentUser.Friends.ToList().Any(u => u.Id.Equals(user.Id)))
            {
                return HttpNotFound();
            }
            currentUser.Friends.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}