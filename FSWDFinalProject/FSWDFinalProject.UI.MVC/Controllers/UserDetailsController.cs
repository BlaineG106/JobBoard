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
    public class UserDetailsController : Controller
    {
        private JobBoardDBEntities db = new JobBoardDBEntities();

        // GET: UserDetails
        //public ActionResult Index()
        //{
        //    return View(db.UserDetails.ToList());
        //}

        // GET: UserDetails/Details/5
        public ActionResult Details(string id)
        {
            string currentUser = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(currentUser);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,ResumeFilename")] UserDetail userDetail, HttpPostedFileBase resumeImg)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                if (resumeImg != null)
                {
                    string imgName = resumeImg.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    string[] goodExts = { ".pdf" };

                    if (goodExts.Contains(ext.ToLower()) && (resumeImg.ContentLength <= 4194304))
                    {
                        imgName = Guid.NewGuid() + ext;

                        resumeImg.SaveAs(Server.MapPath("~/Content/ResumeImg/" + imgName));

                        if (userDetail.ResumeFilename != null && userDetail.ResumeFilename != "noImage.png")
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/ResumeImg/" + userDetail.ResumeFilename));
                        }

                        userDetail.ResumeFilename = imgName;
                    }
                }
                #endregion

                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = User.Identity.GetUserId() });
            }
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetail);
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
