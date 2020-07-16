using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion
{
    public class Product
    {
        public Product(int bagId, int woocommerceId) : this(0, bagId, woocommerceId) { }
        public Product(int id, int bagId, int woocommerceId)
        {
            Id = id;
            BagId = bagId;
            WooCommerceId = woocommerceId;
        }
        public int Id { get; set; }
        public int BagId { get; set; }
        public int WooCommerceId { get; set; }
    }
}
