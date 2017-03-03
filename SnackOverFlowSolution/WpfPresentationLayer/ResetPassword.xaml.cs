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
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        IUserManager _userManager;
        public ResetPassword(IUserManager _userManager, String userName)
        {
            this._userManager = _userManager;
            InitializeComponent();
            lblUserNameVal.Content = userName;
            txtPassword.Text = _userManager.NewPassword();
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (1 == _userManager.ResetPassword((String)lblUserNameVal.Content, txtPassword.Text))
                {
                    MessageBox.Show("User Password Changed");
                }
                else
                {
                    MessageBox.Show("Change failed.  Was the user deleted?");
                }
            }
            catch
            {
                ErrorAlert.ShowDatabaseError();
            }
        }
    }
}
