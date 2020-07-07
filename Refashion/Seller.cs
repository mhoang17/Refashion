using System;
using System.Collections.Generic;
using System.Text;

namespace Refashion
{
    public class Seller
    {
        // Field
        private int tag;
        private string name;
        private string email;
        private string address;
        private string city;
        private int zip;
        private string phoneNumber;
        private DateTime joinDate;

        // Constructors
        // TODO: Have to discuss how we give a user a tag.

        // TODO: maybe delete this constructor after discussion.
        public Seller(int tag, string name, string email, string address, string city, int zip, string phoneNumber)
        {

            this.tag = tag;
            this.name = name;
            this.email = email;
            this.address = address;
            this.city = city;
            this.zip = zip;
            this.phoneNumber = phoneNumber;

            joinDate = DateTime.Today;
        }

        public Seller(string name, string email, string address, string city, int zip, string phoneNumber) {

            // TODO: Have a concrete value and no longer the default value.
            this.tag = 1;
            this.name = name;
            this.email = email;
            this.address = address;
            this.city = city;
            this.zip = zip;
            this.phoneNumber = phoneNumber;

            joinDate = DateTime.Today;
        }

        // Getters and setters
        // You can only retrieve seller tag.
        public int Tag { get { return tag; } }
        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string City { get { return city; } set { city = value; } }
        public int ZIP { get { return zip; } set { zip = value; } }
        // TODO: discuss if there should be a check on if the number is a certain length.
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public DateTime JoinDate { get { return joinDate; } set { joinDate = value; } }

        public override string ToString()
        {
            string tagString = tagExtender();
            
            return tagString + " " + name;
        }

        public string tagExtender()
        {
            int tagLength = 4;
            string sellerTagString = tag.ToString();


            // Check if it has the correct length
            if (sellerTagString.Length < tagLength)
            {

                int count = sellerTagString.Length;
                string placeholder = "";

                while (count < tagLength)
                {
                    placeholder += "0";
                    count++;
                }

                sellerTagString = placeholder + sellerTagString;
            }

            sellerTagString += "#";

            return sellerTagString;
        }

    }
}
