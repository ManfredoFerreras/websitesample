using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AmazonTaxClaim.Models;
using System.IO;

namespace AmazonTaxClaim.Controllers
{
    public class ReclImpAmazonsController : Controller
    {
        private dbepsContext db = new dbepsContext();

        // GET: ReclImpAmazons
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Reclamos.ToList());
        }

        // GET: ReclImpAmazons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReclImpAmazon reclImpAmazon = db.Reclamos.Find(id);
            if (reclImpAmazon == null)
            {
                return HttpNotFound();
            }
            return View(reclImpAmazon);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/AmazonUploads"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    Session["path"] = path.ToString();
                    Session["Filename"] = Path.GetFileName(file.FileName).ToString();
                    Session["Message"] = "Archivo subido satisfactoriamente.";
                    ViewBag.Message = "Archivo subido satisfactoriamente.";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "No ha seleccionado un archivo.";
            }
            return RedirectToAction("Create", ViewBag);
        }

        private IEnumerable<SelectListItem> PopulateTimes()
        {
            List<SelectListItem> timeList = new List<SelectListItem>();
            for (int count = 2016; count < 2018; count++)
            {
                timeList.Add(new SelectListItem()
                {
                    Value = count.ToString("00"),
                    Text = count.ToString("00"),
                    Selected = count == 9,
                });
            }


         


            return timeList;
        }

        // GET: ReclImpAmazons/Create
        [Authorize]
        public ActionResult Create()
        {
            Models.ReclImpAmazon  archivo = new Models.ReclImpAmazon();

            if (Session["Path"] != null)
                archivo.RUTA = Session["Path"].ToString();

            if (Session["Filename"] != null)
                archivo.ARCHIVO = Session["Filename"].ToString();

            archivo.PERIODO = 2016;


            ViewBag.PERIODO = PopulateTimes();




            return View(archivo);
        }

        // POST: ReclImpAmazons/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ARCHIVO,RUTA,PERIODO")] ReclImpAmazon reclImpAmazon)
        {

            if (reclImpAmazon.PERIODO > 0 && reclImpAmazon.ARCHIVO.ToString() != "")
            {
                reclImpAmazon.CTE_NUMERO_EPS = "E-1939";
                reclImpAmazon.ESTADO = 0;
                reclImpAmazon.FECHA = DateTime.Now;
               
                db.Reclamos.Add(reclImpAmazon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Session["Path"] != null)
                reclImpAmazon.RUTA = Session["Path"].ToString();

            if (Session["Filename"] != null)
                reclImpAmazon.ARCHIVO = Session["Filename"].ToString();

            reclImpAmazon.PERIODO = 2016;

            ViewBag.PERIODO = PopulateTimes();


            return View(reclImpAmazon);
        }

        // GET: ReclImpAmazons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReclImpAmazon reclImpAmazon = db.Reclamos.Find(id);
            if (reclImpAmazon == null)
            {
                return HttpNotFound();
            }
            return View(reclImpAmazon);
        }

        // POST: ReclImpAmazons/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CLAIM_ID,CTE_NUMERO_EPS,FECHA,ESTADO,ARCHIVO,RUTA,PERIODO")] ReclImpAmazon reclImpAmazon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reclImpAmazon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reclImpAmazon);
        }

        // GET: ReclImpAmazons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReclImpAmazon reclImpAmazon = db.Reclamos.Find(id);
            if (reclImpAmazon == null)
            {
                return HttpNotFound();
            }
            return View(reclImpAmazon);
        }

        // POST: ReclImpAmazons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReclImpAmazon reclImpAmazon = db.Reclamos.Find(id);
            db.Reclamos.Remove(reclImpAmazon);
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
