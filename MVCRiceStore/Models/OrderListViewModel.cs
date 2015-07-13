using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace MVCRiceStore.Models
{
    public class OrderListViewModel
    {
        public IPagedList<OrderViewModel> OrderList { get; set; }

        public string StoreName { get; set; }
        public string ClientName { get; set; }
    }
}