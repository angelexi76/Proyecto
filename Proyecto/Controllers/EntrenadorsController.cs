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
    public class EntrenadorsController : Controller
    {
        //Objeto de tipo contexto
        private ProyectoContext db = new ProyectoContext();

        // GET: Torneos
        public ActionResult Index()
        {
            return View(db.Entrenadors.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Entrenador entrenador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entrenadors.Add(entrenador);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un entrenador con esta identificacion";
                        
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        
                    }

                    return View(entrenador);

                }

            }
            else
            {
               
                return View(entrenador);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrenador entrenador = db.Entrenadors.Find(id);
            if (entrenador.Equals(null))
            {
                return HttpNotFound();
            }

           
            return View(entrenador);
        }

        [HttpPost]
        public ActionResult Edit(Entrenador entrenador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(entrenador).State = EntityState.Modified;// update
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un entrenador con esta identificacion";
                       

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                       
                    }

                    return View(entrenador);
                }
            }
            else
            {
                
                return View(entrenador);
            }

        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrenador entrenador= db.Entrenadors.Find(id);
            if (entrenador.Equals(null))
            {
                return HttpNotFound();
            }

           
            return View(entrenador);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrenador entrenador = db.Entrenadors.Find(id);// select o from Municipios where MunicipioId=id
            if (entrenador.Equals(null))
            {
                return HttpNotFound();
            }

          
            return View(entrenador);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Entrenador entrenador = db.Entrenadors.Find(id);
            if (entrenador.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    db.Entrenadors.Remove(entrenador);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        ViewBag.Error = "No se permite la eliminación de registros con integridad referencial";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(entrenador);
                }

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