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
    public class ClientsController : Controller
    {
        private BoVoyagesADMREntities db = new BoVoyagesADMREntities();

        // GET: Clients
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.Autorisations).Include(c => c.Personnes);

            int clientId = (int)Session["clientId"];

            return View(clients.Where(c => c.id == clientId).ToList());
        }
       

       

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.autorisationsId = new SelectList(db.Autorisations, "id", "userName");
            ViewBag.personnesId = new SelectList(db.Personnes, "id", "civilite");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,email,personnesId,autorisationsId")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                clients.personnesId = (int)Session["personneId"];
                clients.autorisationsId = (int)Session["autorisationId"];

                db.Clients.Add(clients);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.autorisationsId = new SelectList(db.Autorisations, "id", "userName", clients.autorisationsId);
            ViewBag.personnesId = new SelectList(db.Personnes, "id", "civilite", clients.personnesId);
            return View(clients);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            ViewBag.autorisationsId = new SelectList(db.Autorisations, "id", "userName", clients.autorisationsId);
            ViewBag.personnesId = new SelectList(db.Personnes, "id", "civilite", clients.personnesId);
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "email, userName, motPasse, civilite, nom, prenom, adresse, telephone, dateNaissance")] Clients clients)
        //public ActionResult Edit([Bind(Include = "id,email,personnesId,autorisationsId")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clients).State = EntityState.Modified;
                /*db.Entry(clients.Autorisations).State = EntityState.Modified;
                db.Entry(clients.Personnes).State = EntityState.Modified;*/
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.autorisationsId = new SelectList(db.Autorisations, "id", "userName", clients.autorisationsId);
            ViewBag.personnesId = new SelectList(db.Personnes, "id", "civilite", clients.personnesId);
            return View(clients);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = db.Clients.Find(id);
            db.Clients.Remove(clients);
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
