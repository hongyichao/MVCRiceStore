using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCRiceStore.Models;

namespace MVCRiceStore.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Client
        public ActionResult Index()
        {
            return View(db.clients.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid) {
                db.clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);            
        }

        public ActionResult Edit(int? id)
        {
            var test = (from cl in db.Orders
                       join st in db.stores on cl.StoreId equals st.Id
                       join ri in db.rices on cl.RiceId equals ri.Id 
                       where cl.Client.Id == id
                       select new { st.Name, cl.StoreId, ri.Type, cl.RiceId, cl.Kilogram }).ToList();

            ViewBag.TestList = test;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);               
            }

            Client client = db.clients.Include(c => c.ClientOrders).SingleOrDefault(c => c.Id == id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        [HttpPost]
        public ActionResult Edit(Client client) {

            if (ModelState.IsValid) {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(client);
        }

        public ActionResult Delete(int? id) {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.clients.Find(id);

            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        [HttpPost]
        public ActionResult Delete(int id) {

            Client client = db.clients.Find(id);

            db.clients.Remove(client);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}