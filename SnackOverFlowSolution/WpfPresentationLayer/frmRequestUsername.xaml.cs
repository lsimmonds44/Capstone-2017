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
    /// Interaction logic for frmRequestUsername.xaml
    /// </summary>
    public partial class frmRequestUsername : Window
    {
        IUserManager _userManager;
        IEmailManager _emailManager;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize Request Username Window.
        /// Standaridized method.
        /// </summary>
        public frmRequestUsername()
        {
            InitializeComponent();
            _userManager = new UserManager();
            _emailManager = new EmailManager();
        }

        /// <summary>
        /// Bobby Thorne
        /// 2017/02/04
        /// 
        /// retrieves the username by email and sends that info to the email manager
        /// to send the username to the email given
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestUsername_Click(object sender, RoutedEventArgs e)
        {
            string username = "";
            try
            {
                username = _userManager.RetrieveUsernameByEmail(txtEmail.Text);
                if (username == "")
                {
                    throw new Exception("Unable to find email");
                }
                try
                {
                    _emailManager.sendRequestUsernameEmail(txtEmail.Text, username);
                }
                catch
                {
                    throw new Exception("unable to send Email");
                }
                MessageBox.Show("Check Email for your username.");
                btnRequestUsername.Content = "Try Again";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
