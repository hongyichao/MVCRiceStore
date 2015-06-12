using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mime;
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

            if (svm.rices != null) {
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

            Store store = db.stores.Include(s=>s.rices).SingleOrDefault(s=>s.Id==id);

            if (store == null)
            {
                return HttpNotFound();
            }
            
            var myList = new List<SelectListItem>();

            if (db.rices != null)
            {
                foreach (Rice r in db.rices)
                {
                    myList.Add(new SelectListItem() { Text = r.Type, Value = r.Id.ToString() });
                }
            }

            ViewBag.MyList = myList;
            
            StoreViewModel svm = new StoreViewModel()
                                 {
                                     Id=store.Id,
                                     Name = store.Name,
                                     Address = store.Address,
                                     Suburb = store.Suburb,
                                     State = store.State,
                                     Postcode = store.Postcode
                                 };

            svm.rices = store.rices.Select(s=>s.Id.ToString()).ToArray();

            return View(svm);
        }

        [HttpPost]
        public ActionResult Edit(StoreViewModel svm)
        {
            if (ModelState.IsValid)
            {

                Store s = db.stores.Include(x=>x.rices).SingleOrDefault(x=>x.Id==svm.Id);
                if (s == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                s.rices = null;

                if (svm.rices != null)
                {
                    foreach (string riceId in svm.rices)
                    {
                        Rice r = db.rices.Find(Convert.ToInt32(riceId));

                        if (s.rices == null)
                        {
                            s.rices = new List<Rice>();
                        }

                        if (r != null)
                        {
                            s.rices.Add(r);
                        }
                    }
                }

                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.RiceList = db.rices.ToList();
            var myList = new List<SelectListItem>();

            if (ViewBag.RiceList != null)
            {
                foreach (Rice r in ViewBag.RiceList)
                {
                    myList.Add(new SelectListItem() { Text = r.Type, Value = r.Id.ToString() });
                }
            }

            ViewBag.MyList = myList;

            return View(svm);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Store store = db.stores.Include(s=>s.rices).SingleOrDefault(s=>s.Id==id);

            if(store==null)
            {
                return HttpNotFound();
            }

            StoreViewModel svm = new StoreViewModel()
                                 {
                                     Id = store.Id,
                                     Name = store.Name,
                                     Address = store.Address,
                                     Suburb = store.Suburb,
                                     State =store.State,
                                     Postcode = store.Postcode
                                 };


            svm.rices = store.rices.Select(r => r.Id.ToString()).ToArray();

            var riceList = new List<SelectListItem>();

            foreach (Rice rice in db.rices)
            {
                riceList.Add(new SelectListItem(){Text=rice.Type, Value = rice.Id.ToString()});
            }

            ViewBag.RiceList = riceList;

            return View(svm);
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