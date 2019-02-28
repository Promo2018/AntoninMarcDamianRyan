using WebAppInternet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BoVoyage_Contract;
using System.ServiceModel;


namespace WebAppInternet.Controllers
{
    public class HomeController : Controller
    {
        private BoVoyagesADMREntities db = new BoVoyagesADMREntities();
        
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inscription()
        {
            
            return View();
        }

        public ActionResult Connexion()
        {
            Autorisations log = new Autorisations();
    
            return View(log);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Connexion(Autorisations newlog)
        {
            Autorisations log = new Autorisations();
            string userName = Request.Form["f_userName"];
            string motPasse = Request.Form["f_motPasse"];
            log.userName = userName;
            log.motPasse = motPasse;

            ChannelFactory<IBVAuthorization> canal = new ChannelFactory<IBVAuthorization>("httpAuth");
            canal.Open();
            IBVAuthorization service = canal.CreateChannel();

            var clients = db.Clients.Include(c => c.Autorisations);
            
            

            int authId = service.login(userName, motPasse);
            int clientId = clients.First(c => c.Autorisations.id == authId).id;

            if (clientId > -1)
            {
                Session["username"] = userName;
                Session["clientId"] = clientId;

                return View("Index");
            }
            else
            {
                ViewData["Error"] = "Ressayez";
                return View("Connexion");
            }
        }


        public ActionResult Deconnexion()
        {

            Session.RemoveAll();
            Session.Clear();
            //Session.Abandon();

            return View("Index");
        }


        public ActionResult MesReservations()
        {

            return View();


        }

        public ActionResult MonCompte()
        {


            return View();


        }



        public ActionResult Voyages()
        {
            return View();
        }

    }
}