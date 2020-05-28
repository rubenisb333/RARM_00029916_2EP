using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
    public class Product
    {
        public int idProduct { get; set; }
        public string productName { get; set; }
        public int businessId { get; set; }

        public Product()
        {
            idProduct = 0;
            productName = "";
            businessId = 0;
        }
    }
}
