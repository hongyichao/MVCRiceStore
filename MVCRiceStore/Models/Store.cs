using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRiceStore.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }

        public ICollection<Rice> rices { get; set; }
        public virtual Client client { get; set; }
    }
}