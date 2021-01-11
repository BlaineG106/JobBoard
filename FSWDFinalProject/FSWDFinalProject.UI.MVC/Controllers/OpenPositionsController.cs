using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWDFinalProject.DATA.EF;
using Microsoft.AspNet.Identity;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class OpenPositionsController : Controller
    {
        private JobBoardDBEntities db = new JobBoardDBEntities();
        // GET: OpenPositions
        public ActionResult Index()
        {
            if (User.IsInRole("Employee") || User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                var openPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);
                return View(openPositions.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }



        // GET: OpenPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (User.IsInRole("Employee") || User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OpenPosition openPosition = db.OpenPositions.Find(id);
                if (openPosition == null)
                {
                    return HttpNotFound();
                }
                return View(openPosition);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: OpenPositions/Create
        public ActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber");
                ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title");
                return View();
            }
            if (User.IsInRole("Manager"))
            {
                string currentUserID = User.Identity.GetUserId();
                ViewBag.LocationId = new SelectList(db.Locations.Where(l => l.ManagerId == currentUserID), "LocationId", "StoreNumber");
                ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // POST: OpenPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenPositionId,PositionId,LocationId")] OpenPosition openPosition)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (ModelState.IsValid)
                {
                    db.OpenPositions.Add(openPosition);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
                ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
                return View(openPosition);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: OpenPositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OpenPosition openPosition = db.OpenPositions.Find(id);
                if (openPosition == null)
                {
                    return HttpNotFound();
                }
                ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
                ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
                return View(openPosition);
            }
            if (User.IsInRole("Manager"))
            {
                string currentUserId = User.Identity.GetUserId();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OpenPosition openPosition = db.OpenPositions.Find(id);
                if (openPosition == null)
                {
                    return HttpNotFound();
                }
                ViewBag.LocationId = new SelectList(db.Locations.Where(l => l.ManagerId == currentUserId), "LocationId", "StoreNumber", openPosition.LocationId);
                ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
                return View(openPosition);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: OpenPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpenPositionId,PositionId,LocationId")] OpenPosition openPosition)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(openPosition).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
                ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
                return View(openPosition);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: OpenPositions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OpenPosition openPosition = db.OpenPositions.Find(id);
                if (openPosition == null)
                {
                    return HttpNotFound();
                }
                return View(openPosition);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: OpenPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                OpenPosition openPosition = db.OpenPositions.Find(id);
                db.OpenPositions.Remove(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //One Click Apply
        [Authorize(Roles = "Employee")]
        public ActionResult Apply(int id)
        {
            Application app = new Application();
            var appEmpId = User.Identity.GetUserId();
            var appUserDeets = (from a in db.UserDetails
                                where a.UserId == appEmpId
                                select a).First();

            app.OpenPositionId = id;
            app.UserId = User.Identity.GetUserId();
            app.ApplicationStatus = 2;
            app.ResumeFilename = appUserDeets.ResumeFilename;
            app.ApplicationDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Applications.Add(app);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
