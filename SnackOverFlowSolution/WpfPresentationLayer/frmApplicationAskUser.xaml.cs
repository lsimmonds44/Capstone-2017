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
    /// Interaction logic for frmApplicationAskUser.xaml
    /// </summary>
    public partial class frmApplicationAskUser : Window
    {
        User _user;
        private string use = null;
        private AgreementManager _agreementManager;
        private UserManager _userManager;
        private SupplierManager _supplierManager;
        private ProductManager _productManager;
        private CharityManager _charityManager;
        private CustomerManager _customerManager;

        /// <summary>
        /// Bobby Thorne
        /// 3/24/2017
        /// 
        /// Customizes window to supplier application request
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userManager"></param>
        /// <param name="supplierManager"></param>
        /// <param name="_productManager"></param>
        /// <param name="agreementManager"></param>
        public frmApplicationAskUser(User user, IUserManager userManager, ISupplierManager supplierManager, IProductManager productManager, IAgreementManager agreementManager)
        {
            InitializeComponent();
            use = "supplier";
            _user = user;
            _userManager = (UserManager)userManager;
            _supplierManager = (SupplierManager)supplierManager;
            _productManager = (ProductManager)productManager;
            _agreementManager = (AgreementManager)agreementManager;
            lblWindowTitle.Content = "Would you like to apply to be a supplier?";
        }

        /// <summary>
        /// Bobby Thorne
        /// 3/24/2017
        /// 
        /// Customizes window to Charity application request
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userManager"></param>
        /// <param name="_charityManager"></param>
        public frmApplicationAskUser(User user, IUserManager userManager, ICharityManager charityManager)
        {
            InitializeComponent();
            use = "charity";
            _user = user;
            _userManager = (UserManager)userManager;
            _charityManager = (CharityManager)charityManager;
            lblWindowTitle.Content = "Would you like to apply a Charity?";
        }

        /// <summary>
        /// Bobby Thorne
        /// 3/24/2017
        /// 
        /// Customizes window to Commercial application request
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userManager"></param>
        /// <param name="customerManager"></param>
        public frmApplicationAskUser(User user, IUserManager userManager, ICustomerManager customerManager)
        {
            InitializeComponent();
            use = "Commercial";
            _user = user;
            _userManager = (UserManager)userManager;
            _customerManager = (CustomerManager)customerManager;
            lblWindowTitle.Content = "Would you like to apply to be a Commercial Customer?";
        }

        public frmApplicationAskUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bobby Thorne
        /// 3/24/2017
        /// 
        /// Depending on Init setup of the window
        /// it decides what form to use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (use == "supplier")
            {
                var addSupplierFrm = new frmAddSupplier(_user, _userManager, _supplierManager, _productManager, _agreementManager, "Applying");
                var addSupplierResult = addSupplierFrm.ShowDialog();
                if (addSupplierResult == true)
                {
                    MessageBox.Show("Application Submitted!");
                    //tabSupplier_Selected(sender, e);
                }

                this.Close();
            }
            else if (use == "charity")
            {
                var applyCharityFrm = new frmCharityView(_user, _charityManager);
                var applyCharityResult = applyCharityFrm.ShowDialog();
                if (applyCharityResult == true)
                {
                    MessageBox.Show("Application Submitted!");
                }
                this.Close();
            }
            else if (use == "Commercial")
            {
                var applyCommercialCustomerFrm = new frmCreateCommercialCustomer(_user, _userManager, _customerManager);
                var applyCommercialCustomerResult = applyCommercialCustomerFrm.ShowDialog();
                if (applyCommercialCustomerResult == true)
                {
                    MessageBox.Show("Application Submitted!");
                }
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}