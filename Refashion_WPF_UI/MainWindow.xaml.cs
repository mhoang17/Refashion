using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Refashion;

namespace Refashion_WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO: this variable is a placeholder and should be deleted
        private int sellerTags = 1;

        // TODO: This list should be deleted, when we have access to the real database.
        private List<Seller> sellers;
        private List<string> sellerListViewItems;

        public MainWindow()
        {
            InitializeComponent();
            sellers = new List<Seller>();
            sellerListViewItems = new List<string>();
        }

        // When the new seller button is clicked
        // Methods used when a new seller has been registered
        // TODO: Discuss whether this functionality is needed later on and maybe put this into its own class file
        private void newSellerBtn_Click(object sender, EventArgs e)
        {
            newSellerPanel.Visibility = Visibility.Visible;
            sellerInformationPanel.Visibility = Visibility.Collapsed;
            sellerListView.SelectedItems.Clear();
        }
        private void sellerNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerInformation(e);
            }
        }
        private void sellerEmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerInformation(e);
            }
        }
        private void sellerAddressTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerInformation(e);
            }
        }

        // TODO: Numberinput from numpad most likely doesn't work with this code
        private void sellerZIPTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If what's written is not a number, don't show anything
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;

            // Don't show more than 4 digits
            if (sellerZIPTextBox.Text.Count() >= 4)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter)
            {
                saveSellerInformation(e);
            }
        }
        private void sellerCityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerInformation(e);
            }
        }

        // TODO: Numberinput from numpad most likely doesn't work with this code
        private void sellerPhoneTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If what's written is not a number, don't show anything
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;

            if (e.Key == Key.Enter)
            {
                saveSellerInformation(e);
            }
        }
        private void saveNewSellerBtn_Click(object sender, RoutedEventArgs e)
        {
            saveSellerInformation(e);
        }

        // The method that saves the seller
        private void saveSellerInformation(EventArgs e)
        {

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

            // Add the new seller to the local list
            sellers.Add(newSeller);

            // Add the seller tag and name to the sellerlist in UI
            sellerListView.Items.Add(newSeller.ToString());

            // Save the new listview locally
            sellerListViewItems.Clear();
            sellerListViewItems.AddRange(sellerListView.Items.Cast<string>());

            // Show all sellers again
            sellerListView.Items.Clear();
            foreach (var item in sellerListViewItems)
            {
                sellerListView.Items.Add(item);
            }

            // Clear the textboxes
            sellerNameTextBox.Clear();
            sellerEmailTextBox.Clear();
            sellerAddressTextBox.Clear();
            sellerCityTextBox.Clear();
            sellerZIPTextBox.Clear();
            sellerPhoneTextBox.Clear();

            // TODO: delete this when seller has gotten a proper tag
            sellerTags += 1;
        }





        // These methods is used when an exisiting seller has been selected and is being edited (including deletion)
        private void sellerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sellerListView.Items.Count > 0)
                {
                    // Removes #
                    int sellerTag = tagDecreaser(sellerListView.SelectedItems[0].ToString());

                    // TODO: This will most likely be changed when the database is setup
                    foreach (Seller seller in sellers)
                    {
                        // TODO: This gives ArgumentOutOfRangeException even though it can go into the if statement. Needs to be checked
                        if (seller.Tag == sellerTag)
                        {
                            string sellerTagString = tagExtender(seller.Tag);

                            // Display seller information
                            newSellerPanel.Visibility = Visibility.Collapsed;
                            sellerTagInfoLabel.Content = sellerTagString;
                            sellerNameInfoBox.Text = seller.Name;
                            sellerEmailInfoBox.Text = seller.Email;
                            sellerAddressInfoBox.Text = seller.Address;
                            sellerZIPCityInfoBox.Text = seller.ZIP + " " + seller.City;
                            sellerPhoneInfoBox.Text = seller.PhoneNumber.ToString();
                            sellerJoinDateInfoBox.Content = "Oprettelse: " + seller.JoinDate.ToString("dd/MM-yyyy");
                            sellerInformationPanel.Visibility = Visibility.Visible;
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
        private int tagDecreaser(string sellerTagString)
        {
            // Removes #
            string sellerTagPlaceholder = sellerTagString.Substring(0, 4);

            // Removes 0's if they lie in the front
            if (sellerTagPlaceholder[0].ToString() == "0")
            {
                while (sellerTagPlaceholder[0].ToString() == "0")
                {
                    sellerTagPlaceholder = sellerTagPlaceholder.Remove(0, 1);
                }
            }

            return (int)Int64.Parse(sellerTagPlaceholder);
        }
        private void editSellerInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            // Make the textboxes editable
            sellerNameInfoBox.IsReadOnly = false;
            sellerAddressInfoBox.IsReadOnly = false;
            sellerZIPCityInfoBox.IsReadOnly = false;
            sellerPhoneInfoBox.IsReadOnly = false;
            sellerEmailInfoBox.IsReadOnly = false;

            sellerNameInfoBox.Focus();

            // Change the buttons
            editSellerInfoBtn.Visibility = Visibility.Collapsed;
            saveSellerInfoBtn.Visibility = Visibility.Visible;
            cancelSellerInfoBtn.Visibility = Visibility.Visible;
        }
        private void cancelSellerInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            int sellerTag = tagDecreaser(sellerTagInfoLabel.Content.ToString());

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

                    finishEditingSeller();
                }

            }
        }
        private void saveSellerInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            saveSellerEdit();
        }
        private void sellerNameInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerEdit();
            }
        }
        private void sellerAddressInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerEdit();
            }
        }
        private void sellerZIPCityInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerEdit();
            }
        }
        private void sellerPhoneInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerEdit();
            }
        }
        private void sellerEmailInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerEdit();
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

            int sellerTag = tagDecreaser(sellerTagInfoLabel.Content.ToString());

            // TODO: This will most likely be changed when the database is setup
            foreach (Seller seller in sellers)
            {
                if (seller.Tag == sellerTag)
                {
                    // Change the information locally
                    // TODO: This has to happen in the database
                    seller.Name = name;
                    seller.Address = address;
                    seller.City = city;
                    seller.ZIP = (int)Int64.Parse(zip);
                    seller.PhoneNumber = (int)Int64.Parse(phoneNumber);

                    // Change the name in the sellers list both backend and frontend
                    int sellerItemIdx = sellerListViewItems.IndexOf(sellerListView.SelectedItems[0].ToString());
                    sellerListViewItems[sellerItemIdx] = seller.ToString();

                    sellerListView.SelectedItems[0] = seller.ToString();

                    // If the user has made a search then reset the seller list to the original
                    sellerListView.Items.Clear();
                    foreach (var item in sellerListViewItems)
                    {
                        sellerListView.Items.Add(item);
                    }

                    // Mark the selection again
                    sellerListView.SelectedItem = seller.ToString();

                    sellerListSearchBar.Text = "Søg...";

                    finishEditingSeller();
                }

            }
        }
        private void finishEditingSeller()
        {
            // Make the textboxes non-editable
            sellerNameInfoBox.IsReadOnly = true;
            sellerAddressInfoBox.IsReadOnly = true;
            sellerZIPCityInfoBox.IsReadOnly = true;
            sellerPhoneInfoBox.IsReadOnly = true;
            sellerEmailInfoBox.IsReadOnly = true;

            // Change the buttons
            editSellerInfoBtn.Visibility = Visibility.Visible;
            saveSellerInfoBtn.Visibility = Visibility.Collapsed;
            cancelSellerInfoBtn.Visibility = Visibility.Collapsed;
        }




        // Delete the user from backend and frontend
        private void deleteSellerBtn_Click(object sender, RoutedEventArgs e)
        {
            string message = "Vil du gerne slette denne sælger? Du kan ikke fortryde senere.";
            string caption = "Slet Sælger?";

            MessageBoxResult result = MessageBox.Show(message,caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                int sellerTag = tagDecreaser(sellerTagInfoLabel.Content.ToString());
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

                finishEditingSeller();

                // Change the UI
                sellerInformationPanel.Visibility = Visibility.Collapsed;

                // Remove seller from the backend and frontend
                sellers.Remove(chosenSeller);

                sellerListViewItems.Remove(sellerListView.SelectedItems[0].ToString());
                sellerListView.SelectedItems.Remove(sellerListView.SelectedItems[0].ToString());

                sellerListView.Items.Clear();
                foreach (var item in sellerListViewItems)
                {
                    sellerListView.Items.Add(item);
                }

                // Reset the searchbar text to default
                sellerListSearchBar.Text = "Søg...";
            }
        }




    }
}
