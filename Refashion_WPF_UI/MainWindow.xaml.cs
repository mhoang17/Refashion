using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Refashion;
using Refashion.Database;

namespace Refashion_WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO: this variable is a placeholder and should be deleted
        private int sellerTags = 1;

        private List<Seller> sellers;
        private List<string> sellerListViewItems;
        private SellerDML sellerDML;

        public MainWindow()
        {
            InitializeComponent();
            sellers = new List<Seller>();
            sellerListViewItems = new List<string>();
            sellerDML = new SellerDML();
        }

        // When the new seller button is clicked
        // Methods used when a new seller has been registered
        // TODO: Discuss whether this functionality is needed later on and maybe put this into its own class file
        private void newSellerBtn_Click(object sender, EventArgs e)
        {
            newSellerPanel.Visibility = Visibility.Visible;
            sellerInformationPanel.Visibility = Visibility.Collapsed;

            // Clear warnings
            newSellerMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
            newSellerZIPWarningLabel.Visibility = Visibility.Collapsed;

            sellerInfoMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
            sellerInfoZIPWarningLabel.Visibility = Visibility.Collapsed;

            sellerListView.SelectedItems.Clear();
        }
        private void sellerNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerInformation();
            }
        }
        private void sellerEmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerInformation();
            }
        }
        private void sellerAddressTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerInformation();
            }
        }

        // TODO: Numberinput from numpad most likely doesn't work with this code
        private void sellerZIPTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If what's written is not a number, don't show anything. If tab has been pressed, then act like tab.
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;

            // Don't write more than 4 digits
            if (sellerZIPTextBox.Text.Count() >= 4 && e.Key != Key.Tab)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter)
            {
                saveSellerInformation();
            }
        }
        private void sellerCityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveSellerInformation();
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
                saveSellerInformation();
            }
        }
        private void saveNewSellerBtn_Click(object sender, RoutedEventArgs e)
        {
            saveSellerInformation();
        }

        // The method that saves the seller
        private void saveSellerInformation()
        {

            // Initialize variables to those the user has written in the client
            // TODO: Discuss if the name should be split into first- and lastname
            string name = sellerNameTextBox.Text;
            string email = sellerEmailTextBox.Text;
            string address = sellerAddressTextBox.Text;
            string city = sellerCityTextBox.Text;
            string zip = sellerZIPTextBox.Text;
            string phoneNumber = sellerPhoneTextBox.Text;

            if (triggerInformationWarnings(name, email, address, city, zip, phoneNumber))
                return;

            // This make every first letter in a word uppercase
            // TODO: discuss if this is necessary to have
            name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(address);
            city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);

            // Create a seller
            Seller newSeller = new Seller(sellerTags, name, email, address, city, int.Parse(zip), phoneNumber);
            sellerDML.Insert_Single(newSeller);

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

            // Focus on the namebox
            // TODO: discuss of the newly added seller should be the thing that is being shown
            sellerNameTextBox.Focus();

            // TODO: delete this when seller has gotten a proper tag
            sellerTags += 1;
        }

        
        
        // These methods is used when an exisiting seller has been selected and is being edited (including deletion)
        private void sellerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sellerListView.Items.Count > 0 && sellerListView.SelectedItem != null)
                {
                    int sellerTag = tagDecreaser(sellerListView.SelectedItem.ToString());

                    // TODO: This will most likely be changed when the database is setup
                    foreach (Seller seller in sellers)
                    {
                        // TODO: This can give ArgumentOutOfRangeException even though it can go into the if statement. Needs to be checked
                        if (seller.Tag == sellerTag)
                        {
                            string sellerTagString = seller.tagExtender();

                            // Display seller information
                            newSellerPanel.Visibility = Visibility.Collapsed;
                            sellerTagInfoLabel.Content = sellerTagString;
                            sellerNameInfoBox.Text = seller.Name;
                            sellerEmailInfoBox.Text = seller.Email;
                            sellerAddressInfoBox.Text = seller.Address;
                            sellerZIPInfoBox.Text = seller.ZIP.ToString();
                            sellerCityInfoBox.Text = seller.City;
                            sellerPhoneInfoBox.Text = seller.PhoneNumber.ToString();
                            sellerJoinDateInfoBox.Content = "Oprettelse: " + seller.JoinDate.ToString("dd/MM-yyyy");
                            sellerInformationPanel.Visibility = Visibility.Visible;

                            // If it so happens that a seller was being edited when the selection was made, then hide the edit buttons and make it non-editable
                            finishEditingSeller();

                            return;
                        }

                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        private int tagDecreaser(string sellerTagString)
        {
            // Tag length (since a tag is always in the format 0000# we know the first 4 characters are numbers.
            int tagLength = 4;

            // Removes #
            string sellerTagPlaceholder = sellerTagString.Substring(0, tagLength);

            // Removes 0's if they lie in the front (eg. 0044 becomes 44)
            if (sellerTagPlaceholder[0].ToString() == "0")
            {
                while (sellerTagPlaceholder[0].ToString() == "0")
                {
                    // Remove the first character
                    sellerTagPlaceholder = sellerTagPlaceholder.Remove(0,1);
                }
            }

            return int.Parse(sellerTagPlaceholder);
        }
        private void editSellerInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            // Make the textboxes editable
            sellerNameInfoBox.IsReadOnly = false;
            sellerAddressInfoBox.IsReadOnly = false;
            sellerZIPInfoBox.IsReadOnly = false;
            sellerCityInfoBox.IsReadOnly = false;
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

            // Look for the specific seller and reset the UI to be that seller's values
            foreach (Seller seller in sellers)
            {
                if (seller.Tag == sellerTag)
                {
                    // Reset the information if they have been edited
                    sellerNameInfoBox.Text = seller.Name;
                    sellerEmailInfoBox.Text = seller.Email;
                    sellerAddressInfoBox.Text = seller.Address;
                    sellerZIPInfoBox.Text = seller.ZIP.ToString();
                    sellerCityInfoBox.Text = seller.City;
                    sellerPhoneInfoBox.Text = seller.PhoneNumber.ToString();

                    finishEditingSeller();

                    return;
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
        private void sellerZIPInfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If what's written is not a number, don't show anything. If tab has been pressed, then act like tab.
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;

            // Don't write more than 4 digits
            if (sellerZIPTextBox.Text.Count() >= 4 && e.Key != Key.Tab)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter)
            {
                saveSellerEdit();
            }
        }
        private void sellerCityInfoBox_KeyDown(object sender, KeyEventArgs e)
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
            string name = sellerNameInfoBox.Text;
            string email = sellerEmailInfoBox.Text;
            string address = sellerAddressInfoBox.Text;
            string zip = sellerZIPInfoBox.Text;
            string city = sellerCityInfoBox.Text;
            string phoneNumber = sellerPhoneInfoBox.Text;

            if (triggerInformationWarnings(name, email, address, city, zip, phoneNumber))
                return;

            int sellerTag = tagDecreaser(sellerTagInfoLabel.Content.ToString());

            // TODO: This will most likely be changed when the database is setup
            foreach (Seller seller in sellers)
            {
                if (seller.Tag == sellerTag)
                {
                    string oldSellerString = seller.ToString();

                    // This make every first letter in a word uppercase
                    // TODO: discuss if this is necessary to have
                    name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
                    address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(address);
                    city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);

                    // Change the information locally
                    // TODO: This has to happen in the database
                    seller.Name = name;
                    seller.Email = email;
                    seller.Address = address;
                    seller.City = city;
                    seller.ZIP = int.Parse(zip);
                    seller.PhoneNumber = phoneNumber;

                    // Change the name in the sellerListView both backend and frontend
                    int sellerItemIdx = sellerListViewItems.IndexOf(oldSellerString);
                    sellerListViewItems[sellerItemIdx] = seller.ToString();

                    // TODO: Talk if this reset is needed
                    sellerListView.Items.Clear();
                    foreach (var item in sellerListViewItems)
                    {
                        sellerListView.Items.Add(item);
                    }

                    // Mark the selection again
                    sellerListView.SelectedItem = seller.ToString();

                    sellerListSearchBar.Text = "Søg...";

                    finishEditingSeller();

                    return;
                }

            }
        }
        private void finishEditingSeller()
        {
            // Make the infoboxes of the seller non-editable
            sellerNameInfoBox.IsReadOnly = true;
            sellerAddressInfoBox.IsReadOnly = true;
            sellerZIPInfoBox.IsReadOnly = true;
            sellerCityInfoBox.IsReadOnly = true;
            sellerPhoneInfoBox.IsReadOnly = true;
            sellerEmailInfoBox.IsReadOnly = true;

            // Change the buttons
            editSellerInfoBtn.Visibility = Visibility.Visible;
            saveSellerInfoBtn.Visibility = Visibility.Collapsed;
            cancelSellerInfoBtn.Visibility = Visibility.Collapsed;

            // Collapse warnings
            newSellerMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
            newSellerZIPWarningLabel.Visibility = Visibility.Collapsed;

            sellerInfoMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
            sellerInfoZIPWarningLabel.Visibility = Visibility.Collapsed;
        }


        private Boolean triggerInformationWarnings(string name, string email, string address, string city, string zip, string phoneNumber)
        {
            // Check if all information has been filled
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(zip) ||
                string.IsNullOrEmpty(phoneNumber))
            {
                newSellerMissingInfoWarningLabel.Visibility = Visibility.Visible;
                newSellerZIPWarningLabel.Visibility = Visibility.Collapsed;

                sellerInfoMissingInfoWarningLabel.Visibility = Visibility.Visible;
                sellerInfoZIPWarningLabel.Visibility = Visibility.Collapsed;
                return true;
            }

            else if (zip.Length != 4)
            {
                newSellerMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
                newSellerZIPWarningLabel.Visibility = Visibility.Visible;

                sellerInfoMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
                sellerInfoZIPWarningLabel.Visibility = Visibility.Visible;
                return true;
            }

            newSellerMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
            newSellerZIPWarningLabel.Visibility = Visibility.Collapsed;

            sellerInfoMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
            sellerInfoZIPWarningLabel.Visibility = Visibility.Collapsed;

            return false;
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
                        break;
                    }
                }

                // If it should happen that the loop doesn't find the correct seller, then stop executing method
                if (chosenSeller == null)
                    return;

                // If it so happens that a seller was being edited, then hide the edit buttons and make it non-editable
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




        // Methods for the seller searchbar
        private void sellerListSearchBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: There might need a check so the text only disappears when "Søg..." is in the textfield
            sellerListSearchBar.Text = "";
        }
        private void sellerListSearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sellerListSearchBar.Text))
            {
                sellerListSearchBar.Text = "Søg...";
            }
        }
        
        // TODO: Dicuss if the autocomplete is necessary. If yes, then this method needs to be optimized if there's many sellers
        private void sellerListSearchBar_KeyUp(object sender, KeyEventArgs e)
        {
            object focusedSellerItem = sellerListView.SelectedItem;

            // If nothing stands in the searchbar then show the full list of sellers
            if (string.IsNullOrEmpty(sellerListSearchBar.Text))
            {
                sellerListView.Items.Clear();
                foreach (var item in sellerListViewItems)
                {
                    sellerListView.Items.Add(item);
                }
            }
            else
            {
                // Find those entries that matches
                sellerListView.Items.Clear();
                foreach (string item in sellerListViewItems)
                {
                    if (item.IndexOf(sellerListSearchBar.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        sellerListView.Items.Add(item);
                }
            }

            // If a seller had already been chosen, then reselect it on the sellerList
            if (focusedSellerItem != null)
            {
                sellerListView.SelectedItem = focusedSellerItem.ToString();
            }
        }

        
    }
}
