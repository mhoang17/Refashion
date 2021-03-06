﻿using Refashion.Database;
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
        private int woocommerceId;
        private DateTime joinDate;
        private SellerDML sellerDML = new SellerDML();

        // Constructors
        // TODO: Have to discuss how we give a user a tag.

        // TODO: maybe delete this constructor after discussion.
        public Seller(int tag, string name, string email, string address, string city, int zip, string phoneNumber, int woocommerceId)
        {

            this.tag = tag;
            this.name = name;
            this.email = email;
            this.address = address;
            this.city = city;
            this.zip = zip;
            this.phoneNumber = phoneNumber;
            this.WooCommerceId = woocommerceId;

            joinDate = DateTime.Today;
        }

        public Seller(string name, string email, string address, string city, int zip, string phoneNumber, int woocommerceId) {

            // TODO: Have a concrete value and no longer the default value.
            this.tag = 0;
            this.name = name;
            this.email = email;
            this.address = address;
            this.city = city;
            this.zip = zip;
            this.phoneNumber = phoneNumber;
            this.woocommerceId = woocommerceId;

            joinDate = DateTime.Today;
        }

        // Getters and setters
        // You can only retrieve seller tag.
        public int Tag { get { return tag; } }
        public string TagString
        {
            get
            {
                int tagLength = 4;

                return TagManipulator.Instance.tagExtender(tagLength, tag);
            } 
        }
        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string City { get { return city; } set { city = value; } }
        public int ZIP { get { return zip; } set { zip = value; } }
        // TODO: discuss if there should be a check on if the number is a certain length.
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public int WooCommerceId { get { return woocommerceId; } set { woocommerceId = value; } }
        public DateTime JoinDate { get { return joinDate; } set { joinDate = value; } }
        public string JoinDateString { get { return "Oprettelse: " + joinDate; } }

        public void addSellerDB()
        {
            sellerDML.Insert_Single(this);
            Seller newSeller = sellerDML.Select_Single("email:" + email);
            tag = newSeller.tag;

            WooCommerceConnection.Instance.InsertTagWC(this);
        }

        public void updateSellerDB() {
            sellerDML.Update_Single(this);
        }

        public void deleteSellerDB() {
            sellerDML.Delete_Single(this);
            WooCommerceConnection.Instance.DeleteTagWC(this);
        }


        public override string ToString()
        {
            return TagString + " " + name;
        }

        public override bool Equals(object obj)
        {
            return obj is Seller seller &&
                   Name == seller.Name &&
                   Email == seller.Email &&
                   Address == seller.Address &&
                   City == seller.City &&
                   ZIP == seller.ZIP &&
                   PhoneNumber == seller.PhoneNumber;
        }

        public override int GetHashCode()
        {
            int hashCode = 18982924;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(City);
            hashCode = hashCode * -1521134295 + ZIP.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            return hashCode;
        }
    }
}
