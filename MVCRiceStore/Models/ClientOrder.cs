using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRiceStore.Models
{
    public class ClientOrder
    {
        public int Id { get; set; }        
        public int StoreId { get; set; }
        public int RiceId {get;set;}
        public int Kilogram { get; set; }

        public Client Client { get; set; }
    }
}