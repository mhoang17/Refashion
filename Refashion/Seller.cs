using Refashion.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Refashion
{
    public class Seller : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        // Constructors
        // TODO: Need dateTime parameter
        public Seller(int tag, string name, string email, string address, string city, int zip, string phoneNumber)
        {

            this.tag = tag;
            this.name = name;
            this.email = email;
            this.address = address;
            this.city = city;
            this.zip = zip;
            this.phoneNumber = phoneNumber;

            // TODO: This should be the correct parameter
            this.joinDate = DateTime.Now;
        }

        public Seller(string name, string email, string address, string city, int zip, string phoneNumber) {

            // TODO: Discuss a better way to give a tag (Execute scalar).
            this.name = name;
            this.email = email;
            this.address = address;
            this.city = city;
            this.zip = zip;
            this.phoneNumber = phoneNumber;
            joinDate = DateTime.Now;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Getters and setters
        // You can only retrieve seller tag.

        public int Tag { get { return tag; } }
        public string Name { get { return name; } set { name = value; NotifyPropertyChanged(); } }
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

        public void addSeller(Seller seller)
        {
            SellerDML sellerDML = new SellerDML();

            sellerDML.Insert_Single(seller);
        }

    }
}
