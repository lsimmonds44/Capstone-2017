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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeManager _employeeManager = new EmployeeManager();
        private IUserManager _userManager = new UserManager();
        private ISupplierManager _supplierManager = new SupplierManager();
        private IProductLotManager _productLotManager = new TestProductLotManager();

        Employee _employee = null;
        User _user = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateCommercialCustomerWindow cCCW = new CreateCommercialCustomerWindow(_employee.EmployeeId);
            cCCW.ShowDialog();
        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2017/10/02
        /// 
        /// Button that leads to update employee form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Update_Employee(object sender, RoutedEventArgs e)
        {
            frmUpdateEmployee fUE = new frmUpdateEmployee(_employeeManager, _employee);
            fUE.ShowDialog();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hideTabs();
        }

        /// <summary>
        /// Bobby Thorne
        /// 2/11/17
        /// 
        /// When this button is pushed it first checks to see if there is a user logged in
        /// If there is not it will use the username and password text field and check if
        /// it matches with any user if so it then recieves the user's info
        /// 
        /// Needs work on returning employee info so tabs can be 
        /// filtered and not just show all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (_employee == null)
            {
                String result = _userManager.LogIn(tfUsername.Text, tfPassword.Password);

                if (result.Equals("Found"))
                {

                    lblPassword.Visibility = Visibility.Collapsed;
                    lblUsername.Visibility = Visibility.Collapsed;
                    tfUsername.Visibility = Visibility.Collapsed;
                    tfPassword.Visibility = Visibility.Collapsed;
                    tfPassword.Password = "";
                    btnLogin.Content = "Logout";
                    btnLogin.IsDefault = false;
                    tfPassword.Background = Brushes.White;
                    tfUsername.Background = Brushes.White;
                    _user = _userManager.RetrieveUserByUserName(tfUsername.Text);
                    _employee = _employeeManager.RetrieveEmployeeByUserName(tfUsername.Text); //need to add user to employee
                    statusMessage.Content = "Welcome " + _user.UserName;
                    showTabs(); // This needs to be updated so it will show just one that is 
                    // assigned to the employee


                }
                else if (result.Equals("Invalid Username"))
                {
                    statusMessage.Content = result;
                    tfUsername.Background = Brushes.Red;
                    tfPassword.Background = Brushes.White;
                }
                else if (result.Equals("Invalid Password"))
                {
                    statusMessage.Content = result;
                    tfPassword.Background = Brushes.Red;
                    tfUsername.Background = Brushes.White;
                }
                else if (result.Equals("UserNotFound"))
                {
                    statusMessage.Content = "Username and Password Did not match.";
                    tfPassword.Background = Brushes.Red;
                    tfUsername.Background = Brushes.White;
                }
            }
            else
            {
                _employee = null;
                _user = null;
                btnLogin.Content = "Login";
                btnLogin.IsDefault = true;

                statusMessage.Content = "Please Log in to continue...";
                hideTabs();
                lblPassword.Visibility = Visibility.Visible;
                lblUsername.Visibility = Visibility.Visible;
                tfUsername.Visibility = Visibility.Visible;
                tfPassword.Visibility = Visibility.Visible;

            }


        }
        private void showTabs()
        {
            tabSetMain.Visibility = Visibility.Visible;
            tabCommercialCustomer.Visibility = Visibility.Visible;
            tabEmployee.Visibility = Visibility.Visible;
            tabUser.Visibility = Visibility.Visible;

        }

        private void hideTabs()
        {
            tabSetMain.Visibility = Visibility.Hidden;
            tabCommercialCustomer.Visibility = Visibility.Collapsed;
            tabEmployee.Visibility = Visibility.Collapsed;
            tabUser.Visibility = Visibility.Collapsed;
        }

        private void btnCreateNewUser_Click(object sender, RoutedEventArgs e)
        {
            CreateNewUser fCU = new CreateNewUser();
            fCU.ShowDialog();
        }

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/01/31
        /// 
        /// Open a frmAddSupplier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Last modified by Christian Lopez on 2017/02/02</remarks>
        private void btnCreateSupplier_Click(object sender, RoutedEventArgs e)
        {
            var addSupplierFrm = new frmAddSupplier(_user, _userManager, _supplierManager);
            var addSupplierResult = addSupplierFrm.ShowDialog();
            if (addSupplierResult == true)
            {
                MessageBox.Show("Supplier added!");
            }
        }


        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Open a frmAddInspection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInspection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Will need to redo method call when linked with either datagrid of ProductLots or immediately aftermaking a productLot
                var addInspectionFrm = new frmAddInspection(_productLotManager.RetrieveNewestProductLotBySupplier(_supplierManager.RetrieveSupplierByUserId(_user.UserId)),
                    new GradeManager(), _employee, new TestProductManager(), _supplierManager, new InspectionManager());
                var addInspectionResult = addInspectionFrm.ShowDialog();
                if (addInspectionResult == true)
                {
                    MessageBox.Show("Inspection Added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    } // end of class
} // end of namespace 