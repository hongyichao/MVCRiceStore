using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVCRiceStore.Models;
using PagedList;

namespace MVCRiceStore.Controllers
{
    public class ClientOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ClientOrder
        public ActionResult Index(string storeName, string clientName, 
            string storeNameF, string clientNameF, int page=1)
        {
            if (!Request.IsAjaxRequest())
            {
                storeName = storeNameF;
                clientName = clientNameF;
            }

            var orderList = (from o in db.Orders
                            join s in db.stores on o.StoreId equals s.Id
                            join r in db.rices on o.RiceId equals r.Id
                             where (s.Name.Contains(storeName) ||  string.IsNullOrEmpty(storeName))
                              && (o.Client.Name.Contains(clientName) || string.IsNullOrEmpty(clientName))
                            orderby o.Client.Name
                            select new OrderViewModel()
                                   {
                                       ClientOrderId = o.Id.ToString(), 
                                       StoreId = s.Id, StoreName = s.Name, 
                                       RiceId = r.Id, RiceType = r.Type,
                                       ClientId = o.Client.Id, ClientName = o.Client.Name,
                                       Kilogram = o.Kilogram
                                   }).ToPagedList(page, 10);

            var orderListVm = new OrderListViewModel()
            {
                OrderList = orderList,
                StoreName = storeName,
                ClientName = clientName
            };
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("Orders", orderListVm);
            }

            return View(orderListVm);
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
            ClientOrder order = db.Orders.Include("Client").SingleOrDefault(o=>o.Id==id);

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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderViewModel orderInfo = (from co in db.Orders
                            join s in db.stores on co.StoreId equals s.Id
                            join r in db.rices on co.RiceId equals r.Id
                            where co.Id == id
                            select new OrderViewModel()
                            {
                                ClientOrderId = co.Id.ToString(),
                                StoreId = s.Id,
                                StoreName = s.Name,
                                RiceId = r.Id,
                                RiceType = r.Type,
                                Kilogram = co.Kilogram
                            }).SingleOrDefault();


            return View(orderInfo);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ClientOrder co = db.Orders.Include("Client").SingleOrDefault(o => o.Id == id);

            int clientId = co.Client.Id;

            db.Orders.Remove(co);
            db.SaveChanges();
            
            return RedirectToAction("Edit", "Client", new {id=clientId});
        }

        public ActionResult GetStoreList()
        {
            var stores = from s in db.stores
                         select new SelectListItem() { Text = s.Name, Value = s.Id.ToString() };

            return Json(stores);
        }

        public ActionResult GetStoreAvailableRice(int storeId)
        {
            var store = (db.stores.Include(s => s.rices).Where(s => s.Id == storeId)).SingleOrDefault();

            var rices = new List<SelectListItem>();
            if (store != null)
            {
                foreach (Rice r in store.rices)
                {
                    rices.Add(new SelectListItem(){Text = r.Type, Value = r.Id});
                }
            }

            return Json(rices);
        }
    }
}