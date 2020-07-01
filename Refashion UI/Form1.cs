using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Refashion;

namespace Refashion_UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Save the newly registeret seller
        private void saveNewSellerBtn_Click(object sender, EventArgs e)
        {
            // Initialize variables to those the user has written in the client
            string firstName = sellerFirstNameTextBox.Text;
            string lastName = sellerLastNameTextBox.Text;
            string address = sellerAddressTextBox.Text;
            string city = sellerCityTextBox.Text;
            string zip = sellerZIPTextBox.Text;
            string phoneNumber = sellerPhoneTextBox.Text;

            // Check if all information has been filled
            // TODO: Give proper response to that information is missing
            if (string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(zip) ||
                string.IsNullOrEmpty(phoneNumber))
                return;

            // Create a seller
            Seller newSeller = new Seller(firstName, lastName, address, city, (int)Int64.Parse(zip), (int)Int64.Parse(phoneNumber));

            ListViewItem sellerItem = new ListViewItem(newSeller.Tag.ToString());
            sellerItem.SubItems.Add(firstName);
            sellerListView.Items.Add(sellerItem);

            // Clear the textboxes
            sellerFirstNameTextBox.Clear();
            sellerLastNameTextBox.Clear();
            sellerAddressTextBox.Clear();
            sellerCityTextBox.Clear();
            sellerZIPTextBox.Clear();
            sellerPhoneTextBox.Clear();
        }

        // Only allow numbers with length 4 and under to be written in the ZIP textbox.
        private void sellerZIPTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If what's written is not a number, don't show anything
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            // Don't show more than 4 digits
            if(char.IsDigit(e.KeyChar)) {

                if ((sender as TextBox).Text.Count(char.IsDigit) >= 4)
                    e.Handled = true;
            }
        }

        // Only allow numbers to be written in the phonenumber textbox
        private void sellerPhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If what's written is not a number, don't show anything
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
