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
        private int sellerTag;
        private DateTime registrationDate;

        // Constructors
        // This one is used for databse interaction
        public Bag(int bagID, int sellerTag, DateTime registrationDate)
        {
            this.bagID = bagID;
            this.sellerTag = sellerTag;
            this.registrationDate = registrationDate;
        }
        
        public Bag(int sellerTag)
        {
            bagID = 1;
            this.sellerTag = sellerTag;
            registrationDate = DateTime.Now;
        }

        public int BagID { get { return bagID; } }
        public string BagIDString {
            get
            {
                int tagLength = 5;

                return TagManipulator.Instance.tagExtender(tagLength, bagID);
            }
        }
        public int SellerTag { get { return sellerTag; } set { sellerTag = value; } }
        public DateTime RegistrationDate { get { return registrationDate; }}
        public string RegistrationDateString { get { return "Oprettelse: " + registrationDate; } }
        public override string ToString()
        {
            return BagIDString + " " + RegistrationDateString;
        }
    }
}
