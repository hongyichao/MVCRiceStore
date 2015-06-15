using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRiceStore.Controllers
{
    public class ClientOrderController : Controller
    {
        // GET: ClientOrder
        public ActionResult Index()
        {
            return View();
        }
    }
}