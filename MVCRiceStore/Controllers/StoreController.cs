using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(Store s)
        {
            if(ModelState.IsValid)
            {
                db.stores.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(s);
        }
    }
}