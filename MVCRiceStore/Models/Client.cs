using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCRiceStore.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Client Name is required.")]
        public string Name { get; set; }
        public string Note { get; set; }

        public ICollection<ClientOrder> ClientOrders { get; set; }

    }
}