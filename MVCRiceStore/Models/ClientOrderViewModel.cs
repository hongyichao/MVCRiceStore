using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRiceStore.Models
{
    public class ClientOrderViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int RiceId { get; set; }
        public int Kilogram { get; set; }

        public int ClientId { get; set; }
    }
}