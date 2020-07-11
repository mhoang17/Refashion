using System;
using System.Collections;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SellerPageTest.xaml
    /// </summary>
    public partial class SellerPage : UserControl
    {
        private SellerDML sellerDML;
        private List<Seller> sellers;
        private string defaultSearchPlaceholder;

        public SellerPage()
        {
            InitializeComponent();

            sellerDML = new SellerDML();

            sellers = sellerDML.GetAll();

            sellerListView.ItemsSource = sellers;

            defaultSearchPlaceholder = "Søg...";
        }

        private void newSellerBtn_Click(object sender, EventArgs e)
        {
            newSellerPanel.Visibility = Visibility.Visible;
            sellerInformationPanel.Visibility = Visibility.Collapsed;

            // Clear warnings
            newSellerMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
            newSellerZIPWarningLabel.Visibility = Visibility.Collapsed;

            sellerInfoMissingInfoWarningLabel.Visibility = Visibility.Collapsed;
            sellerInfoZIPWarningLabel.Visibility = Visibility.Collapsed;

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
            string wooCommerceId = "";

            if (triggerInformationWarnings(name, email, address, city, zip, phoneNumber))
                return;

            // This make every first letter in a word uppercase
            name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(address);
            city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);

            // Create a seller and them into the database
            // TODO: this is not an optimal way to get the tag from the database
            Seller newSeller = new Seller(name, email, address, city, int.Parse(zip), phoneNumber, int.Parse(wooCommerceId));
            //newSeller.addSellerDB();
            newSeller = sellerDML.Select_Single("email:" + email);

            // Add the new seller to the local list
            sellers.Add(newSeller);

            // Show all sellers again
            sellerListView.ClearValue(ItemsControl.ItemsSourceProperty);
            sellerListView.ItemsSource = sellers;

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
        }



        // These methods is used when an exisiting seller has been selected and is being edited (including deletion)
        private void sellerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sellerListView.Items.Count > 0 && sellerListView.SelectedItem != null)
                {
                    sellerInformation.DataContext = sellerListView.SelectedItem;

                    // Display seller information
                    newSellerPanel.Visibility = Visibility.Collapsed;
                    sellerInformationPanel.Visibility = Visibility.Visible;

                    // If it so happens that a seller was being edited when the selection was made, then hide the edit buttons and make it non-editable
                    finishEditingSeller();

                    return;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
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
            Seller seller = (Seller)sellerInformation.DataContext;
            // Reset the UI to be that seller's values

            //Reset the information if they have been edited
            sellerNameInfoBox.Text = seller.Name;
            sellerEmailInfoBox.Text = seller.Email;
            sellerAddressInfoBox.Text = seller.Address;
            sellerZIPInfoBox.Text = seller.ZIP.ToString();
            sellerCityInfoBox.Text = seller.City;
            sellerPhoneInfoBox.Text = seller.PhoneNumber.ToString();

            finishEditingSeller();

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

            // TODO: This will most likely be changed when the database is setup
            Seller seller = (Seller)sellerInformation.DataContext;
            int sellerIdx = sellers.IndexOf(seller);

            // This make every first letter in a word uppercase
            // TODO: discuss if this is necessary to have
            name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(address);
            city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);

            // Change the information locally
            seller.Name = name;
            seller.Email = email;
            seller.Address = address;
            seller.City = city;
            seller.ZIP = int.Parse(zip);
            seller.PhoneNumber = phoneNumber;
            //seller.updateSellerDB();

            sellers[sellerIdx] = seller;

            // TODO: Discuss if this edit is needed
            sellerListView.ClearValue(ItemsControl.ItemsSourceProperty);
            sellerListView.ItemsSource = sellers;

            // Mark the selection again
            sellerListView.SelectedItem = seller;

            sellerListSearchBar.Text = defaultSearchPlaceholder;

            finishEditingSeller();

            return;

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

            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Seller chosenSeller = (Seller)sellerInformation.DataContext;

                // If it so happens that a seller was being edited, then hide the edit buttons and make it non-editable
                finishEditingSeller();

                // Change the UI
                sellerInformationPanel.Visibility = Visibility.Collapsed;

                // Remove seller from the backend and frontend
                sellers.Remove(chosenSeller);
                //chosenSeller.deleteSellerDB();

                sellerListView.ClearValue(ItemsControl.ItemsSourceProperty);
                sellerListView.ItemsSource = sellers;

                // Reset the searchbar text to default
                sellerListSearchBar.Text = defaultSearchPlaceholder;
            }
        }




        // Methods for the seller searchbar
        private void sellerListSearchBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: There might need a check so the text only disappears when the default text is in the textfield
            if (sellerListSearchBar.Text.Equals(defaultSearchPlaceholder))
                sellerListSearchBar.Text = "";
        }
        private void sellerListSearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sellerListSearchBar.Text))
            {
                sellerListSearchBar.Text = defaultSearchPlaceholder;
            }
        }

        // TODO: This method needs to be optimized if there's many sellers
        private void sellerListSearchBar_KeyUp(object sender, KeyEventArgs e)
        {
            Seller focusedSellerItem = (Seller)sellerListView.SelectedItem;

            // If nothing stands in the searchbar then show the full list of sellers
            if (string.IsNullOrEmpty(sellerListSearchBar.Text))
            {
                sellerListView.ClearValue(ItemsControl.ItemsSourceProperty);
                sellerListView.ItemsSource = sellers;
            }
            else
            {
                // Find those entries that matches
                sellerListView.ClearValue(ItemsControl.ItemsSourceProperty);
                List<Seller> newSellerList = new List<Seller>();

                foreach (Seller seller in sellers)
                {
                    if (seller.ToString().IndexOf(sellerListSearchBar.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        newSellerList.Add(seller);
                }

                sellerListView.ItemsSource = newSellerList;
            }

            // If a seller had already been chosen, then reselect it on the sellerList
            if (focusedSellerItem != null)
            {
                sellerListView.SelectedItem = focusedSellerItem;
            }
        }

        private void sellerAddressTextBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
