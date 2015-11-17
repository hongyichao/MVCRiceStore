using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCRiceStore.Models;

namespace MVCRiceStore.Controllers
{
    [Authorize]
    public class RiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rice
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.rices.ToList());
        }

        // GET: Rice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rice rice = db.rices.Find(id);
            if (rice == null)
            {
                return HttpNotFound();
            }
            return View(rice);
        }

        // GET: Rice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Type,Origin,price")] Rice rice)
        {
            if (ModelState.IsValid)
            {
                db.rices.Add(rice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rice);
        }

        // GET: Rice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rice rice = db.rices.Find(id);
            if (rice == null)
            {
                return HttpNotFound();
            }
            return View(rice);
        }

        // POST: Rice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Type,Origin,price")] Rice rice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rice);
        }

        // GET: Rice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rice rice = db.rices.Find(id);
            if (rice == null)
            {
                return HttpNotFound();
            }
            return View(rice);
        }

        // POST: Rice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rice rice = db.rices.Find(id);
            db.rices.Remove(rice);
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
