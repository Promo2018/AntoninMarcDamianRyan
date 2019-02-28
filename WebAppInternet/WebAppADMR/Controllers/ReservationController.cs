using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppInternet.Models;

namespace WebAppADMR.Controllers
{
    public class ReservationController : Controller
    {
        private BoVoyagesADMREntities db = new BoVoyagesADMREntities();

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


                return RedirectToAction("InfosParticipants", "Reservation");
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