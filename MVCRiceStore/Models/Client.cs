using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRiceStore.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public ICollection<Store> stores { get; set; }

    }
}