using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVCRiceStore.Models;

namespace MVCRiceStore.Controllers
{
    public class ClientOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ClientOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int clientId)
        {
            ViewBag.StoreList = db.stores.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();
            ViewBag.RiceList = db.rices.Select(r => new SelectListItem() { Text = r.Type, Value = r.Id.ToString() }).ToList();

            ClientOrderViewModel clientOrderVM = new ClientOrderViewModel() { ClientId = clientId };
            
            return View(clientOrderVM);
        }

        [HttpPost]
        public ActionResult Create(ClientOrderViewModel clientOrderVM)
        {
            int? clientId = clientOrderVM.ClientId;

            Client aClient = db.clients.Find(clientId);
            
            ClientOrder order = new ClientOrder()
                                 {
                                     Client = aClient,
                                     Kilogram = clientOrderVM.Kilogram,
                                     RiceId = clientOrderVM.RiceId,
                                     StoreId = clientOrderVM.StoreId
                                 };
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();

                return RedirectToAction("Edit", "Client", new { id = clientId });
            }

            ViewBag.StoreList = db.stores.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();
            ViewBag.RiceList = db.rices.Select(r => new SelectListItem() { Text = r.Type, Value = r.Id.ToString() }).ToList();
            
            return View(clientOrderVM);
        }

        public ActionResult Edit(int? id) {
            ClientOrder order = db.Orders.Find(id);

            ViewBag.StoreList = db.stores.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();
            ViewBag.RiceList = db.rices.Select(r => new SelectListItem() { Text = r.Type, Value = r.Id.ToString() }).ToList();
            
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(ClientOrder order)
        {
            int orderId = order.Id;

            ClientOrder currentOrder  = db.Orders.Include("Client").SingleOrDefault(o=>o.Id == order.Id);

            if (currentOrder != null)
            {
                currentOrder.StoreId = order.StoreId;
                currentOrder.RiceId = order.RiceId;
                currentOrder.Kilogram = order.Kilogram;

                db.Entry(currentOrder).State = EntityState.Modified; ;
                db.SaveChanges();

                return RedirectToAction("Edit","Client",new {id= currentOrder.Client.Id});
            }


            return View(order);
        }
    }
}