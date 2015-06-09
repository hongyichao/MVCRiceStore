using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCRiceStore.Models;

namespace MVCRiceStore.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Store
        public ActionResult Index()
        {
            return View(db.stores.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.RiceList = db.rices.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(StoreViewModel svm)
        {
            Store s = new Store()
                      {
                          Name = svm.Name,
                          Address = svm.Address,
                          Suburb = svm.Suburb,
                          State = svm.State,
                          Postcode = svm.Postcode
                      };

            foreach (string riceId in svm.rices)
            {
                Rice r = db.rices.Find(Convert.ToInt16(riceId));

                if (r != null)
                {
                    if (s.rices == null)
                    {
                        s.rices = new List<Rice>();
                    }

                    s.rices.Add(r);
                }
            }

            if(ModelState.IsValid)
            {
                db.stores.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(svm);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Store store = db.stores.Find(id);

            if (store == null)
            {
                return HttpNotFound();
            }

            return View(store);
        }

        [HttpPost]
        public ActionResult Edit(Store s)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(s);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Store s = db.stores.Find(id);

            if(s==null)
            {
                return HttpNotFound();
            }

            return View(s);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Store store = db.stores.Find(id);

            if (store == null)
            {
                return HttpNotFound();
            }

            db.stores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}