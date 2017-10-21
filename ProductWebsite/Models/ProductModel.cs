using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductWebsite.Models
{

    public class Products
    {
        public List<Product> listOfProducts { get; set; }
    }
    public class Product
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }

   
}