using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MVC_tarea.Models;
using static System.Net.WebRequestMethods;

namespace MVC_tarea.Controllers
{
    public class PeliculasController : Controller
    {
        private PeliculasEntities1 db = new PeliculasEntities1();

        // GET: Peliculas'

        public ActionResult Index(int? par)
        {
            if(par == 1)
            {
                return View(db.Peliculas.OrderBy(modelitem => modelitem.Titulo).ToList());
            }else if (par == 2)
            {
                return View(db.Peliculas.OrderBy(modelitem => modelitem.Calificacion).ToList());
            }else if(par== 3)
            {
                return View(db.Peliculas.OrderBy(modelitem => modelitem.Genero).ToList());
            }
            return View(db.Peliculas.ToList());

        }

        public ActionResult Estadisticas()
        {
            return View(db.Peliculas.OrderByDescending(modelitem => modelitem.Calificacion).Take(5).ToList());
        }

        // GET: Peliculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return HttpNotFound();
            }
            return View(peliculas);
        }

        public ActionResult Acerca()
        {
            return View();
        }

        // GET: Peliculas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Peliculas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PeliculaId,Titulo,Sinopsis,Director,Genero,Calificacion")] Peliculas peliculas)
        {
            HttpPostedFileBase http = Request.Files[0];
            WebImage webImage = new WebImage(http.InputStream);
            peliculas.Poster = webImage.GetBytes();

            if (ModelState.IsValid)
            {
                db.Peliculas.Add(peliculas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(peliculas);
        }

        // GET: Peliculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return HttpNotFound();
            }
            return View(peliculas);
        }

       

        // POST: Peliculas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PeliculaId,Titulo,Sinopsis,Director,Genero,Calificacion")] Peliculas peliculas)
        {
            
            byte[] ImagenActual = null;
            
            HttpPostedFileBase http = Request.Files[0];
            if(http == null)
            {
                ImagenActual = db.Peliculas.SingleOrDefault(t=>t.PeliculaId == peliculas.PeliculaId).Poster;
            }
            else
            {
                WebImage webImage = new WebImage(http.InputStream);
                peliculas.Poster = webImage.GetBytes();
            } 

            if (ModelState.IsValid)
            {
                db.Entry(peliculas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(peliculas);
        }

        public ActionResult EditCal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return HttpNotFound();
            }
            return View(peliculas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCal([Bind(Include = "PeliculaId,Titulo,Sinopsis,Director,Genero,Calificacion")] Peliculas peliculas)
        {
            byte[] ImagenActual = null;

            HttpPostedFileBase http = Request.Files[0];
            if (http == null)
            {
                ImagenActual = db.Peliculas.SingleOrDefault(t => t.PeliculaId == peliculas.PeliculaId).Poster;
            }
            else
            {
                WebImage webImage = new WebImage(http.InputStream);
                peliculas.Poster = webImage.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Entry(peliculas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(peliculas);
        }


        // GET: Peliculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return HttpNotFound();
            }
            return View(peliculas);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Peliculas peliculas = db.Peliculas.Find(id);
            db.Peliculas.Remove(peliculas);
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

        public ActionResult getPoster(int id)
        {
            Peliculas peliculassk = db.Peliculas.Find(id);
            byte[] bytePoster = peliculassk.Poster;

            MemoryStream memoryStream = new MemoryStream(bytePoster);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream,"image/jpg");

        }
    }
}
