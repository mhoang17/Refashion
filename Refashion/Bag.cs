using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion
{
    public class Bag
    {
        // Field
        private int bagID;
        private Seller seller;
        private DateTime registrationDate;

        // Constructors
        // This one is used for databse interaction
        public Bag(int bagID, Seller seller, DateTime registrationDate)
        {
            this.bagID = bagID;
            this.seller = seller;
            this.registrationDate = registrationDate;
        }
        
        public Bag(Seller seller)
        {
            bagID = 1;
            this.seller = seller;
            registrationDate = DateTime.Now;
        }

        public int BagID { get { return bagID; } }
        public string BagIDString {
            get
            {
                int tagLength = 3;

                return TagManipulator.Instance.tagExtender(tagLength, bagID);
            }
        }
        public Seller Seller { get { return seller; } set { seller = value; } }
        public DateTime RegistrationDate { get { return registrationDate; }}
        public string RegistrationDateString { get { return "Oprettelse: " + registrationDate; } }
        public override string ToString()
        {
            return BagIDString + " " + RegistrationDateString;
        }
    }
}
