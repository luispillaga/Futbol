using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using Futbol;
using Futbol.Models;
using Futbol.ModelViews;

namespace Futbol.Controllers
{
    public class GruposController : Controller
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();
        // GET: Grupos
        public ActionResult Index()
        {
            var grupo = db.Grupo.Include(g => g.FaseGrupo);
            return View(grupo.ToList());
        }

        // GET: Grupos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }

            var TorneosEquipos = db.TorneoEquipo;
            List<int> idsTorneosEquipo = new List<int>();
            var participantes = db.FaseEquipo.Where(fe => fe.grupo_id == grupo.grupo_id);
            var allstats = db.Estadistica;
            //var sts = db.Estadistica.Where(e => e.FaseEquipo.grupo_id == grupo.grupo_id);
            List<Estadistica> EquipoEstadistica = new List<Estadistica>();
            if (participantes != null)
            {
                foreach (var item in participantes)
                {
                    EquipoEstadistica.AddRange(allstats.Where(item2 => item.fase_equ_id == item2.fase_equ_id));
                    idsTorneosEquipo.AddRange(from torneoequipo in TorneosEquipos where item.tor_equ_id == torneoequipo.tor_equ_id select torneoequipo.tor_equ_id);
                }

                if (EquipoEstadistica != null)
                {
                    EquipoEstadistica.Sort();
                }
            }

            var Equipos = db.TorneoEquipo.Where(te => idsTorneosEquipo.Contains(te.tor_equ_id));
            var ViewModel = new GrupoViewModel()
            {
                Grupo = grupo,
                EquiposEstadisticas = EquipoEstadistica,
                Jornadas = db.Jornada.Where(j => j.grupo_id == grupo.grupo_id),
                grupo_id = grupo.grupo_id,
                TorneosEquipos = Equipos
            };
            var torneos_equipos = db.TorneoEquipo.Where(te => te.torneo_id == conf.configuracion.IdTorneo);
            ViewBag.tor_equ_id = new SelectList(torneos_equipos, "tor_equ_id", "Equipo.equipo_nombre");
            return View(ViewModel);
        }

        [HttpPost]
        public JsonResult GenerarCalendario(int? id)
        {
            bool respuesta = false;
            if (id == null)
            {
                return Json(respuesta);
            }
            else
            {
                var TorneosEquipos = db.TorneoEquipo;
                var equipos = db.FaseEquipo.Where(fe => fe.grupo_id == id).Include(fe=> fe.TorneoEquipo);
                Grupo grupo = db.Grupo.Find(id);
                int numero_equipos = equipos.Count();
                var idsEquipo = new List<int?>();
                List<int> idsTorneosEquipo = new List<int>();
                if (equipos != null)
                {
                    foreach (var item in equipos)
                    {
                        idsTorneosEquipo.AddRange(from torneoequipo in TorneosEquipos where item.tor_equ_id == torneoequipo.tor_equ_id select torneoequipo.tor_equ_id);
                    }
                    
                }

                var EquiposParticipantes = db.TorneoEquipo.Where(te => idsTorneosEquipo.Contains(te.tor_equ_id));
                foreach (var equipo in EquiposParticipantes)
                {
                    idsEquipo.Add(equipo.equipo_id);
                }
                int numero_partidos = (numero_equipos * (numero_equipos - 1)) / 2;
                int numero_jornadas = 0;
                int partidos_simultaneos = 0;
                int[,] locales = new int[0,0];
                int[,] visitantes = new int[0, 0];
                if ((numero_equipos % 2) == 0)
                {
                    numero_jornadas = numero_equipos - 1;
                    partidos_simultaneos = numero_equipos / 2;
                    int[,] equiposlocales = new int[numero_jornadas, partidos_simultaneos];
                    int[,] equiposvisitantes = new int[numero_jornadas, partidos_simultaneos];
                    int valor = 1;
                    int indiceequipo = 0;
                    int imparAlto = numero_equipos - 1;
                    for (int i = 0; i < numero_jornadas; i++)
                    {
                        for (int j = 0; j < partidos_simultaneos; j++)
                        {
                            if (indiceequipo == (numero_equipos - 1))
                            {
                                indiceequipo = 0;
                            }
                            if (j == 0 && (valor % 2) != 0)
                            {
                                equiposlocales[i, j] = (int) idsEquipo[indiceequipo];
                                equiposvisitantes[i, j] = (int) idsEquipo[numero_equipos - 1];

                            }
                            else if (j == 0 && (valor % 2) == 0)
                            {
                                equiposvisitantes[i, j] = (int) idsEquipo[indiceequipo];
                                equiposlocales[i, j] = (int) idsEquipo[numero_equipos - 1];

                            }
                            else
                            {
                                if (imparAlto == 0)
                                {
                                    imparAlto = numero_equipos - 1;
                                }
                                equiposlocales[i, j] = (int) idsEquipo[indiceequipo];
                                equiposvisitantes[i, j] = (int) idsEquipo[(imparAlto - 1)];
                                imparAlto--;
                            }

                            indiceequipo++;
                        }

                        valor++;
                    }

                    locales = new int[numero_jornadas, partidos_simultaneos];
                    locales = equiposlocales;
                    visitantes = new int[numero_jornadas, partidos_simultaneos];
                    visitantes = equiposvisitantes;
                }
                else
                {
                    numero_jornadas = numero_equipos;
                    partidos_simultaneos = ((numero_equipos - 1) / 2)+1;

                    int[,] equiposlocales = new int[numero_jornadas, partidos_simultaneos];
                    int[,] equiposvisitantes = new int[numero_jornadas, partidos_simultaneos];
                    int valor = 1;
                    int indiceequipo = 0;
                    int imparAlto = numero_equipos - 1;

                    for (int i = 0; i < numero_jornadas; i++)
                    {
                        for (int j = 0; j < partidos_simultaneos; j++)
                        {
                            if (indiceequipo == numero_equipos)
                            {
                                indiceequipo = 0;
                            }
                            if (j == 0)
                            {
                                equiposlocales[i, j] = (int) idsEquipo[indiceequipo];
                                equiposvisitantes[i, j] = 0;

                            }
                            else
                            {
                                if (imparAlto < 0)
                                {
                                    imparAlto = numero_equipos - 1;
                                }
                                equiposlocales[i, j] = (int) idsEquipo[indiceequipo];
                                equiposvisitantes[i, j] = (int) idsEquipo[imparAlto];
                                imparAlto--;
                            }

                            indiceequipo++;
                        }

                        valor++;
                    }

                    locales = new int[numero_jornadas, partidos_simultaneos];
                    locales = equiposlocales;
                    visitantes = new int[numero_jornadas, partidos_simultaneos];
                    visitantes = equiposvisitantes;
                }
                
                for (int i = 0; i < grupo.FaseGrupo.faseg_al_mejor_de; i++)
                {
                    for (int j = 0; j < numero_jornadas; j++)
                    {
                        Jornada NuevaJornada = new Jornada()
                        {
                            grupo_id = id,
                            jornada_fecha_inicio = DateTime.Now
                        };
                        db.Jornada.Add(NuevaJornada);
                        for (int k = 0; k < partidos_simultaneos; k++)
                        {
                            Partido NuevoPartido = new Partido()
                            {
                                Jornada = NuevaJornada,
                                partido_estado = "Configurando",
                                partido_equipo_local = locales[j,k],
                                partido_equipo_visitante = visitantes[j,k]
                            };
                            db.Partido.Add(NuevoPartido);
                        }
                    }

                    int[,] auxiliar = new int[numero_jornadas, partidos_simultaneos];
                    auxiliar = locales;
                    locales = visitantes;
                    visitantes = auxiliar;
                }

                respuesta = true;
                db.SaveChanges();
            }

            return Json(respuesta);
        }

        // GET: Grupos/Create
        public ActionResult Create()
        {
            ViewBag.faseg_id = new SelectList(db.FaseGrupo, "faseg_id", "faseg_nombre");
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "grupo_id,grupo_nombre,faseg_id")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Grupo.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.faseg_id = new SelectList(db.FaseGrupo, "faseg_id", "faseg_nombre", grupo.faseg_id);
            return View(grupo);
        }

        // GET: Grupos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            ViewBag.faseg_id = new SelectList(db.FaseGrupo, "faseg_id", "faseg_nombre", grupo.faseg_id);
            return View(grupo);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "grupo_id,grupo_nombre,faseg_id")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.faseg_id = new SelectList(db.FaseGrupo, "faseg_id", "faseg_nombre", grupo.faseg_id);
            return View(grupo);
        }

        // GET: Grupos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupo grupo = db.Grupo.Find(id);
            db.Grupo.Remove(grupo);
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
