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
    public class ApplicationsController : Controller
    {
        private JobBoardDBEntities db = new JobBoardDBEntities();

        // GET: Applications
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.ApplicationStatus1).Include(a => a.OpenPosition).Include(a => a.UserDetail);
            string currentUser = User.Identity.GetUserId();
            //Admin
            if (User.IsInRole("Admin"))
            {
                return View(applications.ToList());
            }
            //Manager
            if (User.IsInRole("Manager"))
            {
                var manApplications = db.Applications.Where(a => a.OpenPosition.Location.ManagerId == currentUser);
                return View(manApplications);
            }
            //Employee
            if (User.IsInRole("Employee"))
            {
                var empApplications = db.Applications.Where(a => a.UserId == currentUser);
                return View(empApplications);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Applications/Details/5
        [Authorize(Roles = "Admin, Manager, Employee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatuses, "ApplicationStatusId", "StatusName");
            ViewBag.OpenPositionId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId");
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create([Bind(Include = "ApplicationId,OpenPositionId,UserId,ApplicationDate,ManagerNotes,ApplicationStatus,ResumeFilename")] Application application, HttpPostedFileBase resumeImg)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatuses, "ApplicationStatusId", "StatusName", application.ApplicationStatus);
            ViewBag.OpenPositionId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.OpenPositionId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatuses, "ApplicationStatusId", "StatusName", application.ApplicationStatus);
            ViewBag.OpenPositionId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.OpenPositionId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit([Bind(Include = "ApplicationId,OpenPositionId,UserId,ApplicationDate,ManagerNotes,ApplicationStatus,ResumeFilename")] Application application, HttpPostedFileBase resumeImg)
        {
                if (ModelState.IsValid)
                {
                    db.Entry(application).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatuses, "ApplicationStatusId", "StatusName", application.ApplicationStatus);
                ViewBag.OpenPositionId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.OpenPositionId);
                ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
                return View(application);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
