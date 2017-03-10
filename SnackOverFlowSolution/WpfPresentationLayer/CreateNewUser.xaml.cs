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
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : Window
    {
        public CreateNewUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bobby Thorne
        /// 2/12/17
        /// Update
        /// Bobby Thorne
        /// 3/10/2017
        /// 
        /// This manages the create new user it checks each field and
        /// indicates what is invalid later update needs a better check
        /// for username if there is a unexpected exception it will 
        /// just say that the username is already used
        /// 
        /// Update
        /// Added warning labelson right of the text field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateNewUser_Click(object sender, RoutedEventArgs e)
        {
            User _user = new User() {
                UserId = 0,
                FirstName = tFFirstName.Text,
                LastName = tFLastName.Text,
                Phone = tFPhone.Text,
                EmailAddress = tFEmailAddress.Text,
                EmailPreferences = (bool)cbEmailPreferences.IsChecked,
                UserName = tFUserName.Text,
                Active = true
            };
            UserManager _userManager = new UserManager();
            string result;

            tFPassword.Background = Brushes.White;
            tfConfirmPassword.Background = Brushes.White;
            tFUserName.Background = Brushes.White;
            tFFirstName.Background = Brushes.White;
            tFLastName.Background = Brushes.White;
            tFPhone.Background = Brushes.White;
            tFEmailAddress.Background = Brushes.White;

            lblConfirmPasswordWarning.Visibility = Visibility.Hidden;
            lblEmailWarning.Visibility = Visibility.Hidden;
            lblUserWarning.Visibility = Visibility.Hidden;
            lblPasswordWarning.Visibility = Visibility.Hidden;
            lblPhoneWarning.Visibility = Visibility.Hidden;
            lblFirstNameWarning.Visibility = Visibility.Hidden;
            lblLastNameWarning.Visibility = Visibility.Hidden;

            result = _userManager.CreateNewUser(_user, tFPassword.Password,tfConfirmPassword.Password);
            if (result.Equals("Created"))
            {
                this.DialogResult = true;
                lblConfirmPasswordWarning.Visibility = Visibility.Hidden;
                lblEmailWarning.Visibility = Visibility.Hidden;
                lblUserWarning.Visibility = Visibility.Hidden;
                lblPasswordWarning.Visibility = Visibility.Hidden;
                lblPhoneWarning.Visibility = Visibility.Hidden;
                lblFirstNameWarning.Visibility = Visibility.Hidden;
                lblLastNameWarning.Visibility = Visibility.Hidden;


            }
            else if (result.Equals("Invalid Username"))
            {
                tFUserName.Background = Brushes.Red;
                lblUserWarning.Content = "Invalid Username";
                lblUserWarning.Visibility = Visibility.Visible;
            }
            else if (result.Equals("Used Username"))
            {
                tFUserName.Background = Brushes.Red;
                lblUserWarning.Content = "Username already used";
                lblUserWarning.Visibility = Visibility.Visible;
            }
            else if (result.Equals("Invalid Password"))
            {
                tFPassword.Background = Brushes.Red;
                lblPasswordWarning.Content = "Invalid Password";
                lblPasswordWarning.Visibility = Visibility.Visible;
            }
            else if (result.Equals("Password No Match"))
            {
                tFPassword.Background = Brushes.Red;
                tfConfirmPassword.Background = Brushes.Red;
                lblConfirmPasswordWarning.Content = "Passwords do not match";
                lblConfirmPasswordWarning.Visibility = Visibility.Visible;
            }

            else if (result.Equals("Invalid FirstName"))
            {
                tFFirstName.Background = Brushes.Red;
                lblFirstNameWarning.Content = "Must be at between 2 and 50 letters long";
                lblFirstNameWarning.Visibility = Visibility.Visible;
            }
            else if (result.Equals("Invalid LastName"))
            {
                tFLastName.Background = Brushes.Red;
                lblLastNameWarning.Content = "Must be at between 2 and 50 letters long";
                lblLastNameWarning.Visibility = Visibility.Visible;
            }
            else if (result.Equals("Invalid Phone"))
            {
                tFPhone.Background = Brushes.Red;
                lblPhoneWarning.Content = "ex. 1234567890";
                lblPhoneWarning.Visibility = Visibility.Visible;
            }
            else if (result.Equals("Used Email"))
            {
                tFEmailAddress.Background = Brushes.Red;
                lblEmailWarning.Content = "Email already used";
                lblEmailWarning.Visibility = Visibility.Visible;
            }
            else if (result.Equals("Invalid Email"))
            {
                tFEmailAddress.Background = Brushes.Red;
                lblEmailWarning.Content = "Invalid Email";
                lblEmailWarning.Visibility = Visibility.Visible;
            }

        }


        private void btnCancelCreateNewUser_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
