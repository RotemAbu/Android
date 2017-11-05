using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW1.Models;

namespace HW1.ViewModel
{
    public class ProductViewModel
    {
        public Product product { get; set; }
        public List<Product> products { get; set; }

        public Order order { get; set; }
        public List<Order> orders { get; set; }
  
    }
}