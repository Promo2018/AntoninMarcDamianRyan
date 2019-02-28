using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppInternet.Models;

namespace WebAppInternet.Controllers
{
    public class AutorisationsController : Controller
    {
        private BoVoyagesADMREntities db = new BoVoyagesADMREntities();

        // GET: Autorisations
        public ActionResult Index()
        {
            return View(db.Autorisations.ToList());
        }

        // GET: Autorisations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorisations autorisations = db.Autorisations.Find(id);
            if (autorisations == null)
            {
                return HttpNotFound();
            }
            return View(autorisations);
        }

        // GET: Autorisations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autorisations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userName,motPasse,niveau")] Autorisations autorisations)
        {
            if (ModelState.IsValid)
            {
                autorisations.niveau = 2;
                db.Autorisations.Add(autorisations);
                db.SaveChanges();

                Session["autorisationId"] = autorisations.id;
                Session["userName"] = autorisations.userName;

                return RedirectToAction("Create", "Clients");
            }

            return View(autorisations);
        }

        // GET: Autorisations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorisations autorisations = db.Autorisations.Find(id);
            if (autorisations == null)
            {
                return HttpNotFound();
            }
            return View(autorisations);
        }

        // POST: Autorisations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userName,motPasse,niveau")] Autorisations autorisations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autorisations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autorisations);
        }

        // GET: Autorisations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorisations autorisations = db.Autorisations.Find(id);
            if (autorisations == null)
            {
                return HttpNotFound();
            }
            return View(autorisations);
        }

        // POST: Autorisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autorisations autorisations = db.Autorisations.Find(id);
            db.Autorisations.Remove(autorisations);
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
