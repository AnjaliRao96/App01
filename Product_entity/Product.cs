using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_entity
{
    public class Product

    {
        public int SerialNumber { get; set; } // displays the serial number of product
        public string ProductName { get; set; }// gives the name of the product
        public string Brandname { get; set; } // gives the brand
        public string ProductType { get; set; } // gives the type of product 
        public string ProductDescription { get; set; } // description about the product
        public decimal Price { get; set; } // price of the product
    }
}
