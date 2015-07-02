using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRiceStore.Models
{
    public class OrderViewModel
    {
        public string ClientOrderId { get; set; }
        public string StoreName { get; set; }
        public int StoreId { get; set; }
        public string RiceType { get; set; }
        public int RiceId { get; set; }
        public int Kilogram { get; set; }

    }
}