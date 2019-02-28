using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebAppInternet.Models;

namespace WebAppInternet.Controllers
{
    public class VoyagesController : Controller
    {
        private BoVoyagesADMREntities db = new BoVoyagesADMREntities();

        // GET: Voyages
        public ActionResult Index()
        {
            var voyages = db.Voyages.Include(v => v.AgencesVoyages).Include(v => v.Destinations);
            return View(voyages.ToList());
        }

        //Outil de recherche (exclusive, plus de paramètres = recherche plus précise)
        [HttpPost]
        public ViewResult Index(string destination)
        {

            //Instanciation d'un objet LINQ porteur de commande pseudo SQL
            var voyages = from voyage in db.Voyages.Include(v => v.Destinations) select voyage;

            if (!String.IsNullOrEmpty(destination))
            {
                voyages = voyages.Where(voyage => voyage.Destinations.pays.Contains(destination));
            }

            return View(voyages.ToList());
        }

        // GET: Voyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyages voyages = db.Voyages.Find(id);
            if (voyages == null)
            {
                return HttpNotFound();
            }
            return View(voyages);
        }

        // GET: Voyages/Reservation
        public ActionResult InfosParticipants(string region, int? Participants)
        {
            Session["region"] = region;
            ViewData["nombreParticipants"] = Participants;
            return View();
        }

        // POST: Voyages/Reservation
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult InfosParticipants([Bind(Include = "id,civilite,nom,prenom,adresse,telephone,dateNaissance")] Personnes personnes, int? participantNumber)
        {
            //Autorisations autorisations = new Autorisations();

            if (ModelState.IsValid)
            {

                db.Personnes.Add(personnes);
                db.SaveChanges();


                List<int> listOfParticipants = Session["list"] as List<int>;
                if (listOfParticipants == null)
                {
                    listOfParticipants = new List<int>();
                }
                listOfParticipants.Add(personnes.id);
                Session["list"] = listOfParticipants;


                return RedirectToAction("InfosParticipants", "Voyages");
            }
            return View(personnes);
        }

        public ActionResult Paiement()
        {
            return View();
        }

        public ActionResult Confirmation()
        {
            return View();
        }
    }
}
