using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace Proyecto.Controllers
{
    [Authorize]
    public class TorneosController : Controller
    {
        //Objeto de tipo contexto
        private ProyectoContext db = new ProyectoContext();

        // GET: Torneos
        public ActionResult Index()
        {
            //Recuperar la relacion entre torneo y municipio
            var torneo = db.Torneos.Include(mun => mun.Municipio);
            return View(db.Torneos.ToList()); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Llenar un DropDownList con la información de los Municipios
            ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Torneos.Add(torneo); 
                    db.SaveChanges(); 
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un torneo con este nombre";
                        ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre"
                            ,torneo.MunicipioId);
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre"
                           , torneo.MunicipioId);
                    }

                    return View(torneo);

                }

            }
            else
            {
                ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre"
                           , torneo.MunicipioId);
                return View(torneo);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneos.Find(id);
            if (torneo.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre");
            return View(torneo);
        }

        [HttpPost]
        public ActionResult Edit(Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(torneo).State = EntityState.Modified;// update
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un torneo con este nombre";
                        ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre"
                          , torneo.MunicipioId);

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre"
                          , torneo.MunicipioId);
                    }

                    return View(torneo);
                }
            }
            else
            {
                ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre"
                          , torneo.MunicipioId);
                return View(torneo);
            }

        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneos.Find(id);
            if (torneo.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre");
            Municipio m = db.Municipios.Find(torneo.MunicipioId);
            //return View(torneo);
            var detTorneo = new TorneoDetailsView
            {
                TorneoId = torneo.TorneoId,
                Nombre = torneo.Nombre,
                TorneoEquipos = torneo.TorneoEquipos.ToList(),
                Categoria= torneo.Categoria,
                Municipio= m.Nombre,

            };
            return View(detTorneo);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneos.Find(id);// select o from Municipios where MunicipioId=id
            if (torneo.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre");
            return View(torneo);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Torneo torneo = db.Torneos.Find(id);
            if (torneo.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                db.Torneos.Remove(torneo);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Inscribir(int torneoId)
        {
            ViewBag.EquipoId = new SelectList(db.Equipos.OrderBy(e => e.Nombre),
                                            "EquipoId", "Nombre");
            var torneoEq = new TorneoEquipoView
            {
                TorneoId = torneoId,
            };
            return View(torneoEq);
        }

        [HttpPost]
        public ActionResult Inscribir(TorneoEquipoView torneoEquipoView)
        {
            ViewBag.EquipoId = new SelectList(db.Equipos.OrderBy(e => e.Nombre),
                                            "EquipoId", "Nombre");

            var torneoEquipo = db.TorneoEquipos.Where(
                              te => te.TorneoId == torneoEquipoView.TorneoId && te.EquipoId == torneoEquipoView.EquipoId)
                .FirstOrDefault();
            if (torneoEquipo != null)
            {
                ViewBag.Error = "El equipo ya se encuentra inscrito en el Torneo";
            }
            else {
                torneoEquipo = new TorneoEquipo
                {
                    TorneoId = torneoEquipoView.TorneoId,
                    EquipoId = torneoEquipoView.EquipoId,
                };

                try
                {
                    db.TorneoEquipos.Add(torneoEquipo);
                    db.SaveChanges();
                    return RedirectToAction(string.Format("Details/{0}", torneoEquipoView.TorneoId));
                }
                catch (Exception e)
                {
                    return View(torneoEquipoView);   
                }
            }
            return View(torneoEquipoView);
        }

        public ActionResult RetirarEquipo(int id)
        {
            var equ = db.TorneoEquipos.Find(id);
            if (equ != null)
            {
                db.TorneoEquipos.Remove(equ);
                db.SaveChanges();
            }
            return RedirectToAction(string.Format("Details/{0}", equ.TorneoId));
        }

        //Metodo para cerrar la conexion con la base de datos

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