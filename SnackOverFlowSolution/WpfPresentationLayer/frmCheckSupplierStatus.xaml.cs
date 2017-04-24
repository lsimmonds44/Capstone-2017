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
    /// Interaction logic for frmCheckSupplierStatus.xaml
    /// </summary>
    /// <summary>
    /// Alissa Duffy
    /// Updated: 2017/04/24
    /// 
    /// Standaridized Commments.
    /// Standaridized Methods.
    /// </summary>
    public partial class frmCheckSupplierStatus : Window
    {
        User _user;
        private AgreementManager _agreementManager;
        private UserManager _userManager;
        private SupplierManager _supplierManager;
        private ProductManager _productManager;

        /// <summary>
        /// Bobby Thorne
        /// Created: 2017/03/10
        /// 
        /// Initailized the checkSupplierStatus window
        /// More work is needed to add an ability to apply after
        /// this window if they choose to
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userManager"></param>
        /// <param name="supplierManager"></param>
        /// <param name="_productManager"></param>
        /// <param name="agreementManager"></param>
        public frmCheckSupplierStatus(User user, IUserManager userManager, ISupplierManager supplierManager, IProductManager productManager, IAgreementManager agreementManager)
        {
            InitializeComponent();
            _user = user;
            _userManager =(UserManager) userManager;
            _supplierManager = (SupplierManager)supplierManager;
            _productManager = (ProductManager)productManager;
            _agreementManager = (AgreementManager)agreementManager;
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize Check Supplier Status.
        /// Standaridized Methods.
        /// </summary>
        public frmCheckSupplierStatus()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// When a user can apply for a supplier.
        /// Standaridized Methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            //frmAddSupplier addSupplier = new frmAddSupplier(_user,_userManager,_supplierManager,_productManager,_agreementManager);
            //addSupplier.Show();
            MessageBox.Show("When users can apply as suppliers add it to this");
            this.Close();
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Cancel/Closes Check Supplier Status Window.
        /// Standaridized Methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    } // End of class
} // End of namespace