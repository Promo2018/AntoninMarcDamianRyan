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
    public class DossiersController : Controller
    {
        private BoVoyagesADMREntities db = new BoVoyagesADMREntities();

        // GET: Dossiers
        public ActionResult Index()
        {
            var dossiers = db.Dossiers.Include(d => d.Clients).Include(d => d.Voyages).Include(d => d.Personnes)/*.Include(d => d.Assurances)*/;

            int clientId = (int)Session["clientId"];
            //return View(dossiers.Where(dossier => dossier.Clients.id == 6020).ToList());
            return View(dossiers.Where(dossier => dossier.Clients.id == clientId).ToList());
        }


        // GET: Dossiers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossiers dossiers = db.Dossiers.Find(id);
            if (dossiers == null)
            {
                return HttpNotFound();
            }
            return View(dossiers);
        }

        // GET: Dossiers/Create
        public ActionResult Create()
        {


            ViewBag.clientsId = new SelectList(db.Clients, "id", "email");
            ViewBag.voyagesId = new SelectList(db.Voyages, "id", "id");
            return View();
        }

        [HttpPost]
        // GET: Dossiers/Create
        public ActionResult Create(Clients client)
        {
            /*
            string username = Request.Form["f_username"];
            string password = Request.Form["f_password"];
            string typeCompte = Request.Form["RadioType"].ToString();

            client.Username = username;
            client.Password = password;
            client.TypeCompte = typeCompte;
            */
            return View();
        }


        // POST: Dossiers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,etat,raisonAnnulation,numeroCB,clientsId,voyagesId")] Dossiers dossiers)
        {
            if (ModelState.IsValid)
            {
                db.Dossiers.Add(dossiers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clientsId = new SelectList(db.Clients, "id", "email", dossiers.clientsId);
            ViewBag.voyagesId = new SelectList(db.Voyages, "id", "id", dossiers.voyagesId);
            return View(dossiers);
        }

        // GET: Dossiers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossiers dossiers = db.Dossiers.Find(id);
            if (dossiers == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientsId = new SelectList(db.Clients, "id", "email", dossiers.clientsId);
            ViewBag.voyagesId = new SelectList(db.Voyages, "id", "id", dossiers.voyagesId);
            return View(dossiers);
        }

        // POST: Dossiers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,etat,raisonAnnulation,numeroCB,clientsId,voyagesId")] Dossiers dossiers)
        {
            if (ModelState.IsValid)
            {
                dossiers.raisonAnnulation = "CLIENT";
                db.Entry(dossiers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clientsId = new SelectList(db.Clients, "id", "email", dossiers.clientsId);
            ViewBag.voyagesId = new SelectList(db.Voyages, "id", "id", dossiers.voyagesId);
            return View(dossiers);
        }

        // GET: Dossiers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossiers dossiers = db.Dossiers.Find(id);
            if (dossiers == null)
            {
                return HttpNotFound();
            }
            return View(dossiers);
        }

        // POST: Dossiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dossiers dossiers = db.Dossiers.Find(id);
            db.Dossiers.Remove(dossiers);
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
