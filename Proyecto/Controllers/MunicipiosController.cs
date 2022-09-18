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
    public class MunicipiosController : Controller
    {
        //Objeto de tipo contexto
        private ProyectoContext db = new ProyectoContext();

        // GET: Municipios
        public ActionResult Index()
        {
            return View(db.Municipios.ToList()); // select * from Municipios
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();//*retorna a una vista que permita crear el munucipio//
        }

        [HttpPost]
        public ActionResult Create(Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Municipios.Add(municipio); // insert into Municipio values(....)
                    db.SaveChanges(); // Ejecuta la transaccion sobre la base de datos
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un municipio con este nombre";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    
                    return View(municipio);

                }
                
            }
            else 
            {
                return View(municipio);
            }
            
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipios.Find(id);// select o from Municipios where MunicipioId=id
            if (municipio.Equals(null))
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        [HttpPost]
        public ActionResult Edit(Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(municipio).State = EntityState.Modified;// update
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un municipio con este nombre";

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }

                    return View(municipio);
                }
            }
            else
            {
                return View(municipio);
            }
            
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipios.Find(id);// select o from Municipios where MunicipioId=id
            if (municipio.Equals(null))
            {
                return HttpNotFound();
            }
            return View(municipio);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipios.Find(id);// select o from Municipios where MunicipioId=id
            if (municipio.Equals(null))
            {
                return HttpNotFound();
            }
            return View(municipio);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Municipio municipio = db.Municipios.Find(id);
            if (municipio.Equals(null))
            {
                return HttpNotFound();
            }
            else 
            {
                try
                {
                    db.Municipios.Remove(municipio);
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
                    return View(municipio);
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