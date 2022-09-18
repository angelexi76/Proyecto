using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class PatrocinadorsController : Controller
    {
        //Objeto de tipo contexto
        private ProyectoContext db = new ProyectoContext();

        // GET: Patrocinadores
        public ActionResult Index()
        {            
            return View(db.Patrocinadors.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Patrocinadors.Add(patrocinador);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un patrocinador con esta identificacion";
                       
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;                       
                    }

                    return View(patrocinador);

                }

            }
            else
            {                
                return View(patrocinador);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patrocinador patrocinador = db.Patrocinadors.Find(id);
            if (patrocinador.Equals(null))
            {
                return HttpNotFound();
            }

           
            return View(patrocinador);
        }

        [HttpPost]
        public ActionResult Edit(Patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(patrocinador).State = EntityState.Modified;// update
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un patrocinador con esta identificacion";                       

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                       
                    }

                    return View(patrocinador);
                }
            }
            else
            {                
                return View(patrocinador);
            }

        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patrocinador patrocinador = db.Patrocinadors.Find(id);
            if (patrocinador.Equals(null))
            {
                return HttpNotFound();
            }

           
            return View(patrocinador);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patrocinador patrocinador = db.Patrocinadors.Find(id);// select o from Municipios where MunicipioId=id
            if (patrocinador.Equals(null))
            {
                return HttpNotFound();
            }

           
            return View(patrocinador);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Patrocinador patrocinador = db.Patrocinadors.Find(id);
            if (patrocinador.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                db.Patrocinadors.Remove(patrocinador);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

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
