using System;
using System.Collections.Generic;
using System.Text;

namespace Refashion
{
    public class Seller
    {
        // Field
        private int tag;
        private string firstName;
        private string lastName;
        private string email;
        private string address;
        private string city;
        private int zip;
        private int phoneNumber;
        private DateTime joinDate;

        // Constructors
        // TODO: Have to discuss how we give a user a tag.

        // TODO: maybe delete this constructor after discussion.
        public Seller(int tag, string firstName, string lastName, string email, string address, string city, int zip, int phonenumber)
        {

            this.tag = tag;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.address = address;
            this.city = city;
            this.zip = zip;
            this.phoneNumber = phonenumber;

            joinDate = DateTime.Today;
        }

        public Seller(string firstName, string lastName, string email, string address, string city, int zip, int phonenumber) {

            // TODO: Have a concrete value and no longer the default value.
            this.tag = 1;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.address = address;
            this.city = city;
            this.zip = zip;
            this.phoneNumber = phonenumber;

            joinDate = DateTime.Today;
        }

        // Getters and setters
        // You can only retrieve seller tag.
        public int Tag { get { return tag; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string City { get { return city; } set { city = value; } }
        public int ZIP { get { return zip; } set { zip = value; } }
        // TODO: discuss if there should be a check on if the number is a certain length.
        public int PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public DateTime JoinDate { get { return joinDate; } set { joinDate = value; } }



    }
}
