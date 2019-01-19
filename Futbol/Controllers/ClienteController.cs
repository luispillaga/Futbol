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
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();

        public ActionResult TorneoIndex()
        {
            conf.configuracion.Torneos = db.Torneo.ToList(); 
            ViewBag.listatorneo = conf.configuracion.Torneos;
            ViewBag.IdTorneoCliente = conf.configuracion.IdTorneoCliente;
            conf.configuracion.IsTorneoActive = false;
            ViewBag.IsTorneoActive = conf.configuracion.IsTorneoActive;
            var noticia = db.Noticia.Include(t => t.Torneo);
            return View(noticia.ToList());
        }
        public ActionResult Noticias(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conf.configuracion.Torneos = db.Torneo.ToList();
            conf.configuracion.IdTorneoCliente = id;
            conf.configuracion.IsTorneoActive = true;
            ViewBag.listatorneo = conf.configuracion.Torneos;
            ViewBag.IsTorneoActive = conf.configuracion.IsTorneoActive;
            ViewBag.IdTorneoCliente = conf.configuracion.IdTorneoCliente;
            var noticia = db.Noticia.Where(n => n.torneo_id == conf.configuracion.IdTorneoCliente);
            return View(noticia.ToList());
        }

        public ActionResult Equipo()
        {
            conf.configuracion.Torneos = db.Torneo.ToList();
            ViewBag.listatorneo = conf.configuracion.Torneos;
            var equipos= db.TorneoEquipo.Where(e => e.torneo_id == conf.configuracion.IdTorneoCliente);
            return View(equipos);
        }

        public ActionResult EquipoJugador(int? id)
        {
<<<<<<< HEAD

            Torneo torneo = db.Torneo.Find(id);
            ViewBag.mitorneo = torneo;
            if (torneo== null)
            {
                return HttpNotFound();
            }
            return View(torneo);
=======
            conf.configuracion.Torneos = db.Torneo.ToList();
            ViewBag.listatorneo = conf.configuracion.Torneos;
            var jugadores = db.Jugador.Where(j => j.equipo_id == id);
            return View(jugadores.ToList());
>>>>>>> b29306426b425db06bd4badfa6e85c1406715f46
            
            
        }

        
       
    }
}
