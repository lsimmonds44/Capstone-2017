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
    /// Interaction logic for CreateCommercialCustomerWindow.xaml
    /// </summary>
    public partial class CreateCommercialCustomerWindow : Window
    {
        UserManager _userMngr = new UserManager();
        CustomerManager _customerMngr = new CustomerManager();
        User _userToUpdate = null;
        CommercialCustomer _commercialCustomer = null;
        public CreateCommercialCustomerWindow(int employeeId)
        {
            InitializeComponent();
            tFApprovedBy.Text = employeeId.ToString();
        }

        private void findUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (tFUserName.Text.Length > 0)
            {// retrieves user from database by username
                _userToUpdate = _userMngr.RetrieveUserByUserName(tFUserName.Text);
                if (_userToUpdate != null)
                { // populates data for employee to verify they have the correct user information to the customer on the phone.
                    tFName.Text = _userToUpdate.FirstName + " " + _userToUpdate.LastName;
                    tFPhone.Text = _userToUpdate.Phone;
                    tFUserId.Text = _userToUpdate.UserId.ToString();
                    btnCreate.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("User not found!");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        { // cancels and closes the window
            this.DialogResult = false;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (allDataSupplied())
            {
                _commercialCustomer = new CommercialCustomer();
                _commercialCustomer.ApprovedBy = parseToInt(tFApprovedBy.Text);
                _commercialCustomer.IsApproved = (bool)cbIsApproved.IsChecked;
                _commercialCustomer.Active = (bool)cbActive.IsChecked;
                _commercialCustomer.User_Id = parseToInt(tFUserId.Text);
                _commercialCustomer.FedTaxId = parseToInt(tFFedTaxId.Text);
            }
            if (_commercialCustomer != null)
            {
                try
                {
                    _customerMngr.CreateCommercialAccount(_commercialCustomer);
                    MessageBox.Show(_userToUpdate.UserName + "'s Commercial Customer account created.");
                }
                catch (Exception)
                {
                    MessageBox.Show("Creating Commercial Customer account for " + _userToUpdate.UserName + " failed.");
                }
            }
        } // end of btnCreate_Click

        private bool allDataSupplied() // This method checks CreateCommercialCustomer Window making sure all the data needed to create a commercial customer is supplied.
        {
            bool valid = true;
            if (tFFedTaxId.Text.Length != 9)
            {
                MessageBox.Show("Federal Tax Id must be 9 digits do not include the hyphen");
                valid = false;
            }
            else if (parseToInt(tFFedTaxId.Text) == 0)
            {
                MessageBox.Show("Federal Tax Id must be 9 digits.");
                valid = false;
            }
            if(cbIsApproved.IsChecked == false)
            {
                cbIsApproved.IsChecked = true;
            }
            if (cbActive.IsChecked == false)
            {
                cbActive.IsChecked = true;
            }


            return valid;
        }

        private int parseToInt(String input)
        {
            int result;

            int.TryParse(input,out result);
            return result;
        }


    } // end class
} // end namespace
