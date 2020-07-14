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

        public override bool Equals(object obj)
        {
            return obj is Bag bag &&
                   AddedDate.ToString() == bag.AddedDate.ToString() &&
                   SellerId == bag.SellerId;
        }

        public override int GetHashCode()
        {
            int hashCode = -593057844;
            hashCode = hashCode * -1521134295 + AddedDate.GetHashCode();
            hashCode = hashCode * -1521134295 + SellerId.GetHashCode();
            return hashCode;
        }
    }
}
