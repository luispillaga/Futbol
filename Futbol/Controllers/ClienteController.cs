using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Futbol.Models;

namespace Futbol.Controllers
{
    public class ClienteController : Controller
    {
        static List<Torneo> listorneo = new List<Torneo>();
        private FutbolEntities db = new FutbolEntities();

        public ActionResult TorneoIndex()
        {
            
            ViewBag.listatorneo = db.Torneo.ToList();
                
            return View();
          
          
        }


        public ActionResult EquipoJugador(int? id, List<Torneo> listatorneos)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneo.Find(id);
            ViewBag.mitorneo = torneo;
            if (torneo== null)
            {
                return HttpNotFound();
            }
            ViewBag.listatorneo = listatorneos;
            return View(torneo);
            
            
        }

       
    }
}
