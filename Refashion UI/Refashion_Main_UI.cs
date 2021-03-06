﻿using System;
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
    public partial class Refashion_Main_UI : Form
    {

        //TODO: this variable is a placeholder and should be deleted
        private int sellerTags = 1;

        // TODO: This list should be deleted, when we have access to the real database.
        private List<Seller> sellers = new List<Seller>();
        private AutoCompleteStringCollection sellerNames = new AutoCompleteStringCollection();
        private List<ListViewItem> sellerListViewItems = new List<ListViewItem>();

        public Refashion_Main_UI()
        {
            InitializeComponent();
        }

        // When the new seller button is clicked
        // TODO: Discuss whether this functionality is needed later on
        private void newSellerBtn_Click(object sender, EventArgs e)
        {
            newSellerGroupBox.Visible = true;
            sellerInformationGroupBox.Visible = false;
        }

        // Only allow numbers with length 4 and under to be written in the ZIP textbox.
        private void sellerZIPTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If what's written is not a number, don't show anything
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            // Don't show more than 4 digits
            if (char.IsDigit(e.KeyChar))
            {

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

        // Save seller button
        private void saveNewSellerBtn_Click(object sender, EventArgs e)
        {
            saveSellerInformation(e);
        }

        // If enter is pressed in any of the textboxes, then behave just like if the save button has been pressed
        private void sellerNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) {

                saveSellerInformation(e);
                e.SuppressKeyPress = true;
            }
        }

        private void sellerEmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerInformation(e);
                e.SuppressKeyPress = true;
            }
        }

        private void sellerAddressTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerInformation(e);
                e.SuppressKeyPress = true;
            }
        }

        private void sellerCityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerInformation(e);
                e.SuppressKeyPress = true;
            }
        }

        private void sellerZIPTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerInformation(e);
                e.SuppressKeyPress = true;
            }
        }

        private void sellerPhoneTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerInformation(e);
                e.SuppressKeyPress = true;
            }
        }

        // The method that saves the seller
        private void saveSellerInformation(EventArgs e) {

            // Initialize variables to those the user has written in the client
            // TODO: Discuss if the name should be split into first- and lastname
            string name = sellerNameTextBox.Text;
            string email = sellerEmailTextBox.Text;
            string address = sellerAddressTextBox.Text;
            string city = sellerCityTextBox.Text;
            string zip = sellerZIPTextBox.Text;
            string phoneNumber = sellerPhoneTextBox.Text;

            // Check if all information has been filled
            // TODO: Give proper response to that information is missing
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(zip) || zip.Length != 4 ||
                string.IsNullOrEmpty(phoneNumber))
                return;

            // Create a seller
            Seller newSeller = new Seller(sellerTags, name, email, address, city, (int)Int64.Parse(zip), (int)Int64.Parse(phoneNumber));

            // TODO: Delete this when we have the real database
            sellers.Add(newSeller);
            sellerNames.Add(newSeller.ToString());

            // Add the seller tag and name to the sellerlist in UI and the searchbar
            sellerListSearchBar.AutoCompleteCustomSource = sellerNames;

            string listItemString = newSeller.ToString();
            ListViewItem sellerItem = new ListViewItem(listItemString);


            // If a person has searched for something, the listview might be smaller, therefore we should reinsert the original contents into the view again 
            if (sellerListView.Items.Count != sellerListViewItems.Count)
            {
                sellerListView.Items.Clear();
                sellerListView.Items.AddRange(sellerListViewItems.ToArray());
            }
                
            sellerListView.Items.Add(sellerItem);

            // Save the new listview
            sellerListViewItems.Clear();
            sellerListViewItems.AddRange(sellerListView.Items.Cast<ListViewItem>());

            sellerListView.Items.Clear();
            sellerListView.Items.AddRange(sellerListViewItems.ToArray());

            // Clear the textboxes
            sellerNameTextBox.Clear();
            sellerEmailTextBox.Clear();
            sellerAddressTextBox.Clear();
            sellerCityTextBox.Clear();
            sellerZIPTextBox.Clear();
            sellerPhoneTextBox.Clear();

            // TODO: delete this when seller has gotten a proper tag
            sellerTags += 1;

            // Display newly added seller
            
        }

        private void Refashion_Main_UI_Load(object sender, EventArgs e)
        {

        }


        // Change the information displayed when seller is clicked
        private void sellerListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (sellerListView.Items.Count > 0)
                {
                    // Removes #
                    int sellerTag = tagDecreaser(sellerListView.SelectedItems[0].Text);

                    // TODO: This will most likely be changed when the database is setup
                    foreach (Seller seller in sellers)
                    {
                        // TODO: This gives ArgumentOutOfRangeException even though it can go into the if statement. Needs to be checked
                        if (seller.Tag == sellerTag)
                        {
                            string sellerTagString = tagExtender(seller.Tag);

                            // Display seller information
                            newSellerGroupBox.Visible = false;
                            sellerTagInfoLabel.Text = sellerTagString;
                            sellerNameInfoBox.Text = seller.Name;
                            sellerEmailInfoBox.Text = seller.Email;
                            sellerAddressInfoBox.Text = seller.Address;
                            sellerZIPCityInfoBox.Text = seller.ZIP + " " + seller.City;
                            sellerPhoneInfoBox.Text = seller.PhoneNumber.ToString();
                            sellerJoinDateInfoBox.Text = "Oprettelse: " + seller.JoinDate.ToString("dd/MM-yyyy");
                            sellerInformationGroupBox.Visible = true;
                        }

                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }


        // Put 0's infront of the tag number
        private string tagExtender(int sellerTag)
        {
            int tagLength = 4;
            string sellerTagString = sellerTag.ToString();


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

        private int tagDecreaser(string sellerTagString) {

            // Removes #
            string sellerTagPlaceholder = sellerTagString.Substring(0,4);

            // Removes 0's if they lie in the front
            if (sellerTagPlaceholder[0].ToString() == "0")
            {
                while (sellerTagPlaceholder[0].ToString() == "0")
                {
                    sellerTagPlaceholder = sellerTagPlaceholder.Remove(0, 1);
                }
            }

            return (int) Int64.Parse(sellerTagPlaceholder);
        }


        // Methods used when editing the seller's informations
        private void editSellerInfoBtn_Click(object sender, EventArgs e)
        {
            // Make the textboxes editable
            sellerNameInfoBox.ReadOnly = false;
            sellerAddressInfoBox.ReadOnly = false;
            sellerZIPCityInfoBox.ReadOnly = false;
            sellerPhoneInfoBox.ReadOnly = false;
            sellerEmailInfoBox.ReadOnly = false;

            sellerNameInfoBox.Focus();

            // Change the buttons
            editSellerInfoBtn.Visible = false;
            saveSellerInfoBtn.Visible = true;
            cancelSellerInfoBtn.Visible = true;
        }

        private void cancelSellerInfoBtn_Click(object sender, EventArgs e)
        {
            int sellerTag = tagDecreaser(sellerTagInfoLabel.Text);

            foreach (Seller seller in sellers)
            {
                if (seller.Tag == sellerTag)
                {
                    // Reset the information if they have been edited
                    sellerNameInfoBox.Text = seller.Name;
                    sellerEmailInfoBox.Text = seller.Email;
                    sellerAddressInfoBox.Text = seller.Address;
                    sellerZIPCityInfoBox.Text = seller.ZIP + " " + seller.City;
                    sellerPhoneInfoBox.Text = seller.PhoneNumber.ToString();

                    // Make the textboxes non-editable
                    sellerNameInfoBox.ReadOnly = true;
                    sellerAddressInfoBox.ReadOnly = true;
                    sellerZIPCityInfoBox.ReadOnly = true;
                    sellerPhoneInfoBox.ReadOnly = true;
                    sellerEmailInfoBox.ReadOnly = true;

                    // Change the buttons
                    editSellerInfoBtn.Visible = true;
                    saveSellerInfoBtn.Visible = false;
                    cancelSellerInfoBtn.Visible = false;
                }

            }
        }

        private void saveSellerInfoBtn_Click(object sender, EventArgs e)
        {
            saveSellerEdit();
        }

        private void sellerNameInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerEdit();
                e.SuppressKeyPress = true;
            }
        }

        private void sellerAddressInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerEdit();
                e.SuppressKeyPress = true;
            }
        }

        private void sellerZIPCityInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerEdit();
                e.SuppressKeyPress = true;
            }
        }

        private void sellerPhoneInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerEdit();
                e.SuppressKeyPress = true;
            }
        }

        private void sellerEmailInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                saveSellerEdit();
                e.SuppressKeyPress = true;
            }
        }

        private void saveSellerEdit()
        {
            int zipLength = 4;
            int cityStartIdx = 5;

            string name = sellerNameInfoBox.Text;
            string email = sellerEmailInfoBox.Text;
            string address = sellerAddressInfoBox.Text;
            string zip = sellerZIPCityInfoBox.Text.Substring(0, zipLength);
            string city = sellerZIPCityInfoBox.Text.Substring(cityStartIdx, sellerZIPCityInfoBox.Text.Length - cityStartIdx);
            string phoneNumber = sellerPhoneInfoBox.Text;

            // Check if all information has been filled
            // TODO: Give proper response to that information is missing
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(zip) || zip.Length != 4 ||
                string.IsNullOrEmpty(phoneNumber))
                return;

            int sellerTag = tagDecreaser(sellerTagInfoLabel.Text);

            // TODO: This will most likely be changed when the database is setup
            foreach (Seller seller in sellers)
            {
                if (seller.Tag == sellerTag)
                {
                    // Remove the old name of the seller from the autocomplete
                    sellerNames.Remove(seller.ToString());

                    // Change the information locally
                    // TODO: This has to happen in the database
                    seller.Name = name;
                    seller.Address = address;
                    seller.City = city;
                    seller.ZIP = (int)Int64.Parse(zip);
                    seller.PhoneNumber = (int)Int64.Parse(phoneNumber);

                    // Change the name in te sellers list and search-autocomplete
                    sellerNames.Add(seller.ToString());
                    sellerListSearchBar.AutoCompleteCustomSource = sellerNames;

                    int sellerItemIdx = sellerListViewItems.IndexOf(sellerListView.SelectedItems[0]);
                    sellerListViewItems[sellerItemIdx].Text = seller.ToString();

                    sellerListView.SelectedItems[0].Text = seller.ToString();

                    sellerListView.Items.Clear();
                    sellerListView.Items.AddRange(sellerListViewItems.ToArray());
                    sellerListSearchBar.Text = "Søg...";

                    // Make the textboxes non-editable
                    sellerNameInfoBox.ReadOnly = true;
                    sellerAddressInfoBox.ReadOnly = true;
                    sellerZIPCityInfoBox.ReadOnly = true;
                    sellerPhoneInfoBox.ReadOnly = true;
                    sellerEmailInfoBox.ReadOnly = true;

                    // Change the buttons
                    editSellerInfoBtn.Visible = true;
                    saveSellerInfoBtn.Visible = false;
                    cancelSellerInfoBtn.Visible = false;
                }

            }
        }

        private void deleteSellerBtn_Click(object sender, EventArgs e)
        {
            string message = "Vil du gerne slette denne sælger? Du kan ikke fortryde senere.";
            string caption = "Slet Sælger?";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            
            result = MessageBox.Show(message, caption, buttons);

            if (result == DialogResult.Yes)
            {
                int sellerTag = tagDecreaser(sellerTagInfoLabel.Text);
                Seller chosenSeller = null;

                foreach (Seller seller in sellers)
                {
                    if (seller.Tag == sellerTag)
                    {
                        // TODO: This should happen in the database
                        chosenSeller = seller;
                    }

                }

                // If it should happen that the loop doesn't find the correct seller, then stop executing method
                if (chosenSeller == null)
                    return;

                sellers.Remove(chosenSeller);
                sellerNames.Remove(chosenSeller.ToString());

                // Make the textboxes non-editable
                sellerNameInfoBox.ReadOnly = true;
                sellerAddressInfoBox.ReadOnly = true;
                sellerZIPCityInfoBox.ReadOnly = true;
                sellerPhoneInfoBox.ReadOnly = true;
                sellerEmailInfoBox.ReadOnly = true;

                // Change the buttons
                editSellerInfoBtn.Visible = true;
                saveSellerInfoBtn.Visible = false;
                cancelSellerInfoBtn.Visible = false;

                // Change the UI
                sellerInformationGroupBox.Visible = false;

                sellerListViewItems.Remove(sellerListView.SelectedItems[0]);
                sellerListView.SelectedItems[0].Remove();

                sellerListView.Items.Clear();
                sellerListView.Items.AddRange(sellerListViewItems.ToArray());

                // Reset the searchbar text to default
                sellerListSearchBar.Text = "Søg...";
            }
                
        }

        private void sellerListSearchBar_Click(object sender, EventArgs e)
        {
            // TODO: There might need a check so the text only disappears when "Søg..." is in the textfield
            sellerListSearchBar.Text = "";
        }

        private void sellerListSearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // If nothing stands in the searchbar then the original listview
                if(string.IsNullOrEmpty(sellerListSearchBar.Text))
                {
                    sellerListView.Items.Clear();
                    sellerListView.Items.AddRange(sellerListViewItems.ToArray());
                    sellerListSearchBar.Text = "Søg...";
                    e.SuppressKeyPress = true;
                    return;
                }

                // Find those entries that matches
                // TODO: Make it not case sensitive
                var list = sellerListViewItems.Cast<ListViewItem>()
                                   .Where(x => x.SubItems
                                                .Cast<ListViewItem.ListViewSubItem>()
                                                .Any(y => y.Text.Contains(sellerListSearchBar.Text)))
                                   .ToArray();

                sellerListView.Items.Clear();
                sellerListView.Items.AddRange(list);
            }
        }

    }
}
