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
        IUserManager _uMgr;
        ISupplierManager _sMgr;
        bool supplierFound = false;

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/01/31
        /// 
        /// Constructor for the form
        /// </summary>
        /// <param name="currentEmp">The current User</param>
        /// <param name="uMgr">Something implementing the IUserManager interface</param>
        /// <param name="sMgr">Something implementing the ISupplierManager interface</param>
        public frmAddSupplier(User currentUser, IUserManager uMgr, ISupplierManager sMgr)
        {
            _currentUser = currentUser;
            _uMgr = uMgr;
            _sMgr = sMgr;
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
                    supplierUser = _uMgr.RetrieveUserByUserName(txtUsername.Text);
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
                UserAddress ua = null;
                // Try to get the user's preferred address
                try
                {
                    ua = _uMgr.RetrieveUserAddress(supplierUser.PreferredAddressId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to get user address. ERROR: " + ex.Message);
                }

                // Fill in tables with the suppliers information that will carry over from user
                // (name, phone for double checking) and assume the user's address
                // will be the same as the farm address, but that can be changed
                txtName.Text = supplierUser.FirstName + " " + supplierUser.LastName;
                if (ua == null)
                {
                    txtFarmAddress.Text = "";
                    txtFarmCity.Text = "";
                    cboFarmState.SelectedIndex = 0;
                }
                else
                {
                    txtFarmAddress.Text = ua.AddressLineOne + " " + ua.AddressLineTwo;
                    txtFarmCity.Text = ua.City;

                    // Need to get index to populate the correct drop down with
                    // the user's state
                    if (getDropdown(ua.State) != -1)
                    {
                        cboFarmState.SelectedIndex = getDropdown(ua.State);
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
                try
                {
                    validateInputs();
                    User supplierUser = _uMgr.RetrieveUserByUserName(txtUsername.Text);

                    // Actually try to create the supplier
                    if (_sMgr.CreateNewSupplier(supplierUser.UserId, true, _currentUser.UserId, txtFarmName.Text,
                        txtFarmCity.Text, cboFarmState.Text, txtFarmTaxId.Text))
                    {
                        this.DialogResult = true;
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


    }
}
