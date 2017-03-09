using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/01/31
    /// 
    /// Interaction logic for AddSupplier.xaml
    /// </summary>
    public partial class frmAddSupplier : Window
    {

        User _currentUser;
        Supplier _supplierToEdit;
        IUserManager _userManager;
        ISupplierManager _supplierManager;
        IProductManager _productManager;
        IAgreementManager _agreementManager;
        bool supplierFound = false;
        string _type;
        List<Product> _notAgreedProducts;
        List<Product> _agreedProducts = new List<Product>();
        List<Agreement> _agreements;

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/01/31
        /// 
        /// Constructor for the form
        /// </summary>
        /// <param name="currentEmp">The current User</param>
        /// <param name="userManager">Something implementing the IUserManager interface</param>
        /// <param name="supplierManager">Something implementing the ISupplierManager interface</param>
        /// <remarks>Last modified by Christian Lopez 2017/03/08</remarks>
        public frmAddSupplier(User currentUser, IUserManager userManager, ISupplierManager supplierManager, 
                IProductManager productManager, IAgreementManager agreementManager, string type = "Adding", Supplier supplierToEdit = null)
        {
            _currentUser = currentUser;
            _supplierToEdit = supplierToEdit;
            _userManager = userManager;
            _supplierManager = supplierManager;
            _productManager = productManager;
            _agreementManager = agreementManager;
            if (null == _supplierToEdit)
            {
                _type = type;
            }
            else
            {
                _type = "Edit";
                supplierFound = true;
                btnLookup.IsEnabled = false;
            }
            
            InitializeComponent();
        }

        private void btnLookup_Click(object sender, RoutedEventArgs e)
        {
            // Set up variables to use
            User supplierUser = null;
            resetForm();

            if (txtUsername.Text.Length != 0)
            {
                try
                {
                    supplierUser = _userManager.RetrieveUserByUserName(txtUsername.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Unable to access DB! ERROR: " + ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter a username");
                txtUsername.Focus();
                return;
            }

            // Check whether or not we actually got a user
            if (supplierUser == null)
            {
                MessageBox.Show("Unable to find user: " + txtUsername.Text);
            }
            else
            {
                supplierFound = true;
                UserAddress userAddress = null;
                // Try to get the user's preferred address
                try
                {
                    userAddress = _userManager.RetrieveUserAddress(supplierUser.PreferredAddressId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to get user address. ERROR: " + ex.Message);
                }

                // Fill in tables with the suppliers information that will carry over from user
                // (name, phone for double checking) and assume the user's address
                // will be the same as the farm address, but that can be changed
                txtName.Text = supplierUser.FirstName + " " + supplierUser.LastName;
                if (userAddress == null)
                {
                    txtFarmAddress.Text = "";
                    txtFarmCity.Text = "";
                    cboFarmState.SelectedIndex = 0;
                }
                else
                {
                    txtFarmAddress.Text = userAddress.AddressLineOne + " " + userAddress.AddressLineTwo;
                    txtFarmCity.Text = userAddress.City;

                    // Need to get index to populate the correct drop down with
                    // the user's state
                    if (getDropdown(userAddress.State) != -1)
                    {
                        cboFarmState.SelectedIndex = getDropdown(userAddress.State);
                    }
                    else
                    {
                        MessageBox.Show("Cannot find user's state!");
                        cboFarmState.SelectedIndex = 0;
                    }

                }
                // Let the employee modify the form
                txtPhone.Text = supplierUser.Phone;
                txtFarmAddress.IsEnabled = true;
                txtFarmCity.IsEnabled = true;
                txtFarmName.IsEnabled = true;
                txtFarmTaxId.IsEnabled = true;
                cboFarmState.IsEnabled = true;
                productSection.IsEnabled = true;
                foreach (Product p in _agreedProducts)
                {
                    _notAgreedProducts.Remove(p);
                }
                dgAvailableProducts.ItemsSource = _notAgreedProducts;
                dgApprovedProducts.ItemsSource = _agreedProducts;

            }

        }

        private void resetForm()
        {
            txtName.Text = "";
            txtFarmAddress.Text = "";
            txtFarmAddress.IsEnabled = false;
            txtFarmCity.Text = "";
            txtFarmCity.IsEnabled = false;
            txtPhone.Text = "";
            txtFarmName.Text = "";
            txtFarmName.IsEnabled = false;
            txtFarmTaxId.Text = "";
            txtFarmTaxId.IsEnabled = false;
            cboFarmState.SelectedIndex = 0;
            cboFarmState.IsEnabled = false;
            supplierFound = false;
            productSection.IsEnabled = false;
            dgAvailableProducts.ItemsSource = null;
        }

        private int getDropdown(string stateAbr)
        {
            // The way to get the index for the drop down combo box

            string[] states = {"AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "IA", "ID", "IL",
                              "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND",
                              "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN",
                              "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY"};


            return binarySearchStates(stateAbr, 0, states.Length, states);
        }

        private int binarySearchStates(string wantedState, int first, int arrayLength, string[] states)
        {
            int mid = (arrayLength / 2) + first;
            if (arrayLength == 0)
            {
                return -1;
            }
            else
            {
                if (states[mid].CompareTo(wantedState) < 0)
                {
                    mid = binarySearchStates(wantedState, mid + 1, (arrayLength - 1) / 2, states);
                }
                else if (states[mid].CompareTo(wantedState) > 0)
                {
                    mid = binarySearchStates(wantedState, first, arrayLength / 2, states);
                }
                return mid;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // The process of submitting information to make a supplier in the DB

            // See if we even have a user found for the supplier
            if (!supplierFound)
            {
                MessageBox.Show("Please look up the supplier by the username.");
            }
            else
            {
                if (_type.Equals("Adding"))
                {
                    try
                    {
                        validateInputs();
                        User supplierUser = _userManager.RetrieveUserByUserName(txtUsername.Text);

                        // Actually try to create the supplier
                        if (_supplierManager.CreateNewSupplier(supplierUser.UserId, true, _currentUser.UserId, txtFarmName.Text,
                            txtFarmCity.Text, cboFarmState.Text, txtFarmTaxId.Text))
                        {
                            //this.DialogResult = true;
                            try
                            {
                                addAgreedProducts(supplierUser);
                                this.DialogResult = true;
                            }
                            catch (Exception ex)
                            {
                                
                                MessageBox.Show(ex.Message);
                            }
                            
                        }
                        else
                        {
                            // If an error was thrown, should go to catch block. This would occur if the number of rows
                            // affected were not one
                            MessageBox.Show("There was an error, where more than one row was affected! Please " +
                                "contact your Database Admin.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (_type.Equals("Applying"))
                {
                    try
                    {
                        validateInputs();
                        User supplierUser = _userManager.RetrieveUserByUserName(txtUsername.Text);

                        // Actually try to create the supplier
                        if (_supplierManager.ApplyForSupplierAccount(supplierUser.UserId, txtFarmName.Text, txtFarmCity.Text, 
                            cboFarmState.Text,txtFarmTaxId.Text))
                        {
                            //this.DialogResult = true;
                            try
                            {
                                addAgreedProducts(supplierUser);
                                this.DialogResult = true;
                            }
                            catch (Exception ex)
                            {
                                
                                MessageBox.Show(ex.Message);
                            }
                            
                        }
                        else
                        {
                            // If an error was thrown, should go to catch block. This would occur if the number of rows
                            // affected were not one
                            MessageBox.Show("There was an error, where more than one row was affected! Please " +
                                "contact your Database Admin.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void addAgreedProducts(User supplierUser)
        {
            foreach (Product p in _agreedProducts)
            {
                
                try
                {
                    if (_type.Equals("Adding"))
                    {
                        _agreementManager.CreateAgreementsForSupplier(_supplierManager.RetrieveSupplierByUserId(supplierUser.UserId), p, _currentUser.UserId);
                    }
                    else if (_type.Equals("Applying"))
                    {
                        _agreementManager.CreateAgreementsForSupplier(_supplierManager.RetrieveSupplierByUserId(supplierUser.UserId), p);
                    }
                    
                }
                catch (Exception ex)
                {

                    throw new ApplicationException("Could not store " + p.Name + " as an agreement. Error: " + ex.Message);
                }
            }
        }

        private void validateInputs()
        {
            if (txtFarmName.Text.Length == 0)
            {
                throw new ApplicationException("Please enter a farm name.");
            }
            if (txtFarmAddress.Text.Length == 0)
            {
                throw new ApplicationException("Please enter a farm address.");
            }
            if (txtFarmCity.Text.Length == 0)
            {
                throw new ApplicationException("Please enter a farm city.");
            }
            if (txtFarmTaxId.Text.Length == 0)
            {
                throw new ApplicationException("Please enter a farm tax ID.");
            }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If the username is changed, cannot guarantee that the information is
            // correct. Reset by clicking the lookup button.
            supplierFound = false;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Sets the screen depending on the type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_type.Equals("Adding"))
            {
                btnSubmit.Content = "Submit";
                this.Title = "Create Supplier";
            }
            else if (_type.Equals("Applying"))
            {
                btnSubmit.Content = "Apply";
                this.Title = "Apply for Account";
            }
            try
            {
                _notAgreedProducts = _productManager.RetrieveProducts();
                if (_type.Equals("Adding") || _type.Equals("Applying"))
                {
                    //If adding or applying for an account, then there cannot be any already approved products.
                    _agreedProducts = new List<Product>();
                }
                else if (null != _supplierToEdit)
                {
                    _agreements = _agreementManager.RetrieveAgreementsBySupplierId(_supplierToEdit.SupplierID);
                    foreach (Agreement a in _agreements)
                    {
                        _agreedProducts.Add(_productManager.RetrieveProductById(a.ProductId));
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error: " + ex.Message);
            }
            

        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Logic to take an product from the not approved list to the approved list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToApproved_Click(object sender, RoutedEventArgs e)
        {
            if (0 > dgAvailableProducts.SelectedIndex)
            {
                MessageBox.Show("Please select a Product to add!");
            }
            else
            {
                _agreedProducts.Add((Product)(dgAvailableProducts.SelectedItem));
                _notAgreedProducts.Remove((Product)(dgAvailableProducts.SelectedItem));
                refreshTables();
            }
        }

        

        private void btnRemoveFromApproved_Click(object sender, RoutedEventArgs e)
        {
            if (0 > dgApprovedProducts.SelectedIndex)
            {
                MessageBox.Show("Please select a Product to remove!");
            }
            else
            {
                _agreedProducts.Remove((Product)(dgApprovedProducts.SelectedItem));
                _notAgreedProducts.Add((Product)(dgApprovedProducts.SelectedItem));
                refreshTables();
            }
        }
        
        private void refreshTables()
        {
            dgAvailableProducts.ItemsSource = null;
            dgApprovedProducts.ItemsSource = null;
            dgAvailableProducts.ItemsSource = _notAgreedProducts;
            dgApprovedProducts.ItemsSource = _agreedProducts;
        }

    }
}
