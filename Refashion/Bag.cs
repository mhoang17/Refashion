using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion
{
    public class Bag
    {
        public Bag(int sellerId) : this(0, sellerId, DateTime.Now) { }

        public Bag(int id, int sellerId, DateTime addedDate)
        {
            Id = id;
            SellerId = sellerId;
            AddedDate = addedDate;
        }

        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public int SellerId { get; set; }

        // TODO: If needed: add method that fetches all products with relation to bag
        public List<Product> Products { get; set; }
    }
}
