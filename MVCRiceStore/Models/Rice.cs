using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRiceStore.Models
{
    public class Rice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Origin { get; set; }
        public int price { get; set; }

        public ICollection<Store> stores { get; set; }
    }
}