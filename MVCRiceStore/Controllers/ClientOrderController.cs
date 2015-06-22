using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}