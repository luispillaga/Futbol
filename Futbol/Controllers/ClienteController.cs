using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Futbol.Models;
using System.Data.Entity;


namespace Futbol.Controllers
{
    public class ClienteController : Controller
    {
        static List<Torneo> listorneo = new List<Torneo>();
        private FutbolEntities db = new FutbolEntities();

        public ActionResult TorneoIndex()
        {
            var conf = ConfiguracionSingleton.GetInstance();
            conf.configuracion.Torneos = db.Torneo.ToList(); 
            ViewBag.listatorneo = conf.configuracion.Torneos;
            ViewBag.IdTorneoCliente = conf.configuracion.IdTorneoCliente;
            var noticia = db.Noticia.Include(t => t.Torneo);
            return View(noticia.ToList());
        }
        public ActionResult Noticias(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var conf = ConfiguracionSingleton.GetInstance();
            conf.configuracion.Torneos = db.Torneo.ToList();
            conf.configuracion.IdTorneoCliente = id;
            ViewBag.listatorneo = conf.configuracion.Torneos;
            ViewBag.IdTorneoCliente = conf.configuracion.IdTorneoCliente;
            var noticia = db.Noticia.Where(n => n.torneo_id == conf.configuracion.IdTorneoCliente);
            return View(noticia.ToList());
        }



        public ActionResult EquipoJugador(int? id)
        {




            Torneo torneo = db.Torneo.Find(id);
            ViewBag.mitorneo = torneo;
            if (torneo== null)
            {
                return HttpNotFound();
            }
            return View(torneo);
            
            
        }

        public ActionResult Equipo()
        {


            return View();
        }
       
    }
}
