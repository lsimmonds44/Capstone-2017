using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
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
        private ICustomerManager _customerManager = new CustomerManager();
        private EmployeeManager _employeeManager = new EmployeeManager();
        private IProductOrderManager _orderManager = new ProductOrderManager();
        private IVehicleManager _vehicleManager = new VehicleManager();
        List<ProductOrder> _currentOpenOrders;
        List<Employee> _employeeList;
        List<Charity> _charityList;
        private List<ProductLot> _productLotList;
        private List<CommercialCustomer> _commercialCustomers;
        private List<Vehicle> _vehicleList;
        private List<Supplier> _supplierList;
        private IUserManager _userManager = new UserManager();
        private ISupplierManager _supplierManager = new SupplierManager();
        private IProductLotManager _productLotManager = new ProductLotManager();
        private IProductManager _productManager = new ProductManager();
        private IDeliveryManager _deliveryManager;
        private IWarehouseManager _warehouseManager = new WarehouseManager();
        private IAgreementManager _agreementManager = new AgreementManager();
        private List<Delivery> _deliveries;
        private List<Warehouse> _warehouseList;
        private ProductLotSearchCriteria _productLotSearchCriteria;
        private ICharityManager _charityManager;
        private IPreferenceManager _preferenceManager;
        private ISupplierInventoryManager _supplierInventoryManager;
        private ILocationManager _locationManager;
        


        Employee _employee = null;
        Supplier _supplier = null;
        CommercialCustomer _commercialCustomer = null;
        Charity _charity = null;
        User _user = null;
        Role _role = null;

        private IPackageManager _packageManager = new PackageManager();
        List<Package> _packageList = null;
        private IOrderStatusManager _orderStatusManager = new OrderStatusManager();
        List<string> _orderStatusList = null;
        ISupplierInvoiceManager _supplierInvoiceManager = new SupplierInvoiceManager();
        List<SupplierInvoice> _supplierInvoiceList;
        List<string> _supplierApplicationStatus = null;
        List<User> _userList = null;
        private List<SupplierCatalogViewModel> _parsedSupplierCatalogueData = null;

        public MainWindow()
        {
            InitializeComponent();
            _userManager = new UserManager();
            _charityManager = new CharityManager();
            _employeeManager = new EmployeeManager();
            _deliveryManager = new DeliveryManager();
            _supplierInventoryManager = new SupplierInventoryManager();
            DisposeFiles();
            _productLotSearchCriteria = new ProductLotSearchCriteria() { Expired = false };
        }

        /// <summary>
        /// Eric Walton
        /// 2017/02/06
        /// 
        /// Button to load Create Commercial Customer Window
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_CommercialCustomer_Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (cboCustomerType.SelectedItem as String == "Commercial")
            {
                try
                {
                    _employee = _employeeManager.RetrieveEmployeeByUserName(_user.UserName);
                    frmCreateCommercialCustomer cCCW = new frmCreateCommercialCustomer((int)_employee.EmployeeId);
                    if (cCCW.ShowDialog() == true)
                    {
                        _commercialCustomers = _customerManager.RetrieveCommercialCustomers();
                        dgCustomer.ItemsSource = _commercialCustomers;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: An employee must be logged in to create a commercial customer.");
                }
            }
            else if (cboCustomerType.SelectedItem as String == "Residential")
            {
                // If creating a residential customer is added to desktop code will go here to create one.
            }
        }

        /// <summary>
        /// Eric Walton
        /// 2017/03/03
        /// Invoked when the customer type combo box is initialized
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCustomerType_Initialized(object sender, EventArgs e)
        {
            cboCustomerType.Items.Add("Commercial");
            cboCustomerType.Items.Add("Residential");
            cboCustomerType.SelectedItem = "Commercial";
            refreshCustomerList();
        }

        /// <summary>
        /// Eric Walton
        /// 2017/03/03
        /// Invoked when the customer type combo box is changed
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/4/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCustomerType_Selected(object sender, EventArgs e)
        {
            refreshCustomerList();
        }

        /// <summary>
        /// Eric Walton
        /// 2017/03/03
        /// Refreshes the customer list from the database
        /// </summary>
        private void refreshCustomerList()
        {
            if (cboCustomerType.SelectedItem as String == "Commercial")
            {
                try
                {
                    _commercialCustomers = _customerManager.RetrieveCommercialCustomers();
                    dgCustomer.ItemsSource = _commercialCustomers;
                }
                catch (Exception ex)
                {
                    if (null != ex.InnerException)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (cboCustomerType.SelectedItem as String == "Residential")
            {
                dgCustomer.ItemsSource = null;
                // When functionality to retrieve list of residential customers the code will go here.
            }
        }
        /// <summary>
        /// Eric Walton
        /// 2017/03/05
        /// Invoked when the create order button is clicked on the customers tab.
        /// Loads the create order window
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/4/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomer.SelectedIndex >= 0)
            {
                var selectedCustomer = (CommercialCustomer)dgCustomer.SelectedItem;
         
                if (selectedCustomer.Active)
                {
                    try
                    {
                        frmCreateOrder createOrderWindow = new frmCreateOrder((int)_employee.EmployeeId, (CommercialCustomer)dgCustomer.SelectedItem);
                        if (createOrderWindow.ShowDialog() == true)
                        {

                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Only employees can create order from the desktop app.");
                    }
                }
                else
                {
                    MessageBox.Show(selectedCustomer.CommercialId + " Must be active");
                }   
            }
            else
            {
                MessageBox.Show("Must select a user to create an order!");
            }
            
        }

        /// <summary>
        /// Eric Walton
        /// 2017/3/10
        /// Enables the create order button when a customer is selected.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/4/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Customer_Selected(object sender, SelectionChangedEventArgs e)
        {
            btnCreateOrder.IsEnabled = true;
        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2017/2/10
        /// 
        /// Invokes the Update Employee form.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/4/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Employee_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frmUpdateEmployee fUE = new frmUpdateEmployee(_employeeManager, _employeeList[dgEmployee.SelectedIndex]);
                fUE.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Please Select an Employee to Edit.");
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hideTabs();
            tabSetMain.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Bobby Thorne
        /// 2017/2/11
        /// 
        /// When this button is pushed it first checks to see if there is a user logged in
        /// If there is not it will use the username and password text field and check if
        /// it matches with any user if so it then recieves the user's info
        /// 
        /// Needs work on returning _employee info so tabs can be 
        /// filtered and not just show all
        /// 
        /// Bobby Thorne
        /// Updated: 2017-3-24
        /// 
        /// Reset buttons and nulled supplier, _charity, and customer variables
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/4/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                try
                {
                    if (_userManager.AuthenticateUser(txtUsername.Text, pwbPassword.Password))
                    {
                        lblPassword.Visibility = Visibility.Collapsed;
                        lblUsername.Visibility = Visibility.Collapsed;
                        txtUsername.Visibility = Visibility.Collapsed;
                        pwbPassword.Visibility = Visibility.Collapsed;
                        mnuRequestUsername.Visibility = Visibility.Collapsed;
                        tabCommercialCustomer.Focus();
                        pwbPassword.Password = "";
                        btnLogin.Content = "Logout";
                        btnLogin.IsDefault = false;
                        pwbPassword.Background = Brushes.White;
                        try
                        {
                            _user = _userManager.userInstance;
                            if (tabMyAccount.IsFocused)
                            {
                                tabMyAccount_Selected(sender, e);
                            }

                            if ("ADMIN" == _user.UserName)
                            {
                                btnResetPassword.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                btnResetPassword.Visibility = Visibility.Collapsed;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to find user.");
                            btnLogin_Click(sender, e);
                        }
                        try
                        {
                            _employee = _employeeManager.RetrieveEmployeeByUserName(_user.UserName);
                        }
                        catch (Exception ex)
                        {
                            // Enters here if user that access this is not an _employee.
                            // For now it does nothing. 
                            if (null != ex.InnerException)
                            {
                                MessageBox.Show("Employee table is empty or DB connection error." + "\n\n" + ex.InnerException.Message);
                            }
                            else
                            {
                                MessageBox.Show("Employee table is empty or DB connection error." + "\n\n" + ex.Message);
                            }                           
                        }
                        statusMessage.Content = "Welcome " + _user.UserName;
                        showTabs(); // This needs to be updated so it will show just one that is 
                        // assigned to the employee
                        mnuChangePassword.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        statusMessage.Content = "Username and Password did not match.";
                        pwbPassword.Password = "";
                        pwbPassword.Background = Brushes.Red;
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                _user = null;
                btnLogin.Content = "Login";
                btnLogin.IsDefault = true;
                _supplier = null;
                _charity = null;
                _commercialCustomer = null;
                dgMyInvoices.Visibility = Visibility.Hidden;
                lblMyInvoices.Visibility = Visibility.Hidden;
                btnSupplierApplicationStatusCheck.Content = "Check Supplier Status";
                btnCharityApplicationStatusCheck.Content = "Check Charity Status";
                btnCommercialCustomerApplicationStatusCheck.Content = "Check Commerical Status";
                btnSupplierApplicationStatusCheck.IsEnabled = true;
                btnCharityApplicationStatusCheck.IsEnabled = true;
                btnCommercialCustomerApplicationStatusCheck.IsEnabled = true;
                statusMessage.Content = "Please Log in to continue...";
                hideTabs();
                lblPassword.Visibility = Visibility.Visible;
                lblUsername.Visibility = Visibility.Visible;
                txtUsername.Visibility = Visibility.Visible;
                pwbPassword.Visibility = Visibility.Visible;
                mnuRequestUsername.Visibility = Visibility.Visible;
                mnuChangePassword.Visibility = Visibility.Collapsed;
                btnResetPassword.Visibility = Visibility.Collapsed;
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
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Create New User form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateNewUser_Click(object sender, RoutedEventArgs e)
        {
            frmCreateNewUser fCU = new frmCreateNewUser();
            fCU.ShowDialog();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Charity Tab and returns a List of Charities 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCharity_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                _charityList = _charityManager.RetrieveCharityList();
                dgCharity.DataContext = _charityList;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Charity View form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewCharity_Click(object sender, RoutedEventArgs e)
        {
            if (dgCharity.SelectedIndex >= 0)
            {
                var CharityViewInstance = new frmCharityView(_charityManager, _charityList[dgCharity.SelectedIndex]);
                CharityViewInstance.ShowDialog();
                tabCharity_Selected(sender, e);
            }

        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the ability to Add Charity
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCharity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Employee> employeeSearchList = _employeeManager.SearchEmployees(new Employee() { UserId = _user.UserId });
                int employeeId;
                if (employeeSearchList.Count > 0)
                {
                    employeeId = (int)employeeSearchList[0].EmployeeId;
                    var CharityViewInstance = new frmCharityView(_charityManager, employeeId);
                    CharityViewInstance.SetEditable();
                    CharityViewInstance.ShowDialog();
                    tabCharity_Selected(sender, e);
                }
                else
                {
                    var applyForCharityFrm = new frmCharityView(_user, _charityManager);
                    var result = applyForCharityFrm.ShowDialog();
                    if (result == true)
                    {
                        MessageBox.Show("Application submitted");
                        tabCharity_Selected(sender, e);
                    }

                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Employee Tab with a list of Employees.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabEmployee_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                _employeeList = _employeeManager.RetrieveEmployeeList();
                dgEmployee.DataContext = _employeeList;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the ability to Add an Employee.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var EmployeeViewInstance = new frmEmployeeViews(_employeeManager);
            EmployeeViewInstance.SetEditable();
            EmployeeViewInstance.ShowDialog();
            tabEmployee_Selected(sender, e);
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Order tab.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabOrder_Selected(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Employee View form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployee.SelectedIndex >= 0)
            {
                var EmployeeViewInstance = new frmEmployeeViews(_employeeManager, _employeeList[dgEmployee.SelectedIndex]);
                EmployeeViewInstance.ShowDialog();
                tabEmployee_Selected(sender, e);
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Product Lot tab and a lists of Product Lot.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabProductLot_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ProductLot> productLotList;
                if (_productLotSearchCriteria.Expired)
                {
                    productLotList = _productLotManager.RetrieveExpiredProductLots();
                }
                else
                {
                    productLotList = _productLotManager.RetrieveProductLots();
                }
                dgProductLots.ItemsSource = productLotList;
            }
            catch (Exception ex)
            {

                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the ability to add a new Product Lot. 
        /// Invokes the Add Inspection form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProductLot_Click(object sender, RoutedEventArgs e)
        {
            var productLotView = new frmAddProductLot();
            productLotView.SetEditable();
            var addResult = productLotView.ShowDialog();
            if (addResult == true)
            {
                try
                {
                    var addInspectionFrm = new frmAddInspection(_productLotManager.RetrieveNewestProductLotBySupplier(_supplierManager.RetrieveSupplierBySupplierID(productLotView.supplierId)),
                        new GradeManager(), _employee, new ProductManager(), _supplierManager, new InspectionManager(), new ProductLotManager());
                    var addInspectionResult = addInspectionFrm.ShowDialog();
                    if (addInspectionResult == true)
                    {
                        MessageBox.Show("Inspection Added");
                    }
                }
                catch (Exception ex)
                {
                    if (null != ex.InnerException)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            tabProductLot_Selected(sender, e);
        }
        /// <summary>
        /// Christian Lopez
        /// 2017/01/31
        /// 
        /// Invokes Add Supplier Form.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Christian Lopez Updated: 2017/02/02</remarks>
        private void btnCreateSupplier_Click(object sender, RoutedEventArgs e)
        {
            var addSupplierFrm = new frmAddSupplier(_user, _userManager, _supplierManager, _productManager, _agreementManager);
            var addSupplierResult = addSupplierFrm.ShowDialog();
            if (addSupplierResult == true)
            {
                MessageBox.Show("Supplier added!");
                tabSupplier_Selected(sender, e);
            }
        }
        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Open a frmAddInspection
        /// </summary>
        /// <summary>
        /// Robert Forbes
        /// Updated: 2017/03/30
        /// 
        /// Now shows an error message if there is no product lot selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInspection_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductLots.SelectedIndex != -1)
            {
                try
                {
                    // Will need to redo method call when linked with either datagrid of ProductLots or immediately aftermaking a productLot
                    //_productLotManager.RetrieveNewestProductLotBySupplier(_supplierManager.RetrieveSupplierByUserId(_user.UserId))
                    var addInspectionFrm = new frmAddInspection((ProductLot)dgProductLots.SelectedItem,
                        new GradeManager(), _employee, new ProductManager(), _supplierManager, new InspectionManager(), new ProductLotManager());
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
            else
            {
                MessageBox.Show("Please select a product lot to create a new inspection");
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Password KeyDown.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pwbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            pwbPassword.Background = Brushes.White;
        }

        /// <summary>
        /// Laura Simmonds
        /// 2017/27/02
        /// 
        /// Invokes View Product form.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewProduct_Click(object sender, RoutedEventArgs e)
        {
            frmViewProduct btnViewProduct = new frmViewProduct();
            btnViewProduct.ShowDialog();

        }

        /// <summary>
        /// Mason Allen
        /// 2017/3/01
        /// 
        /// Invokes ListView that displays current open orders.  Double clicking an item will display the order details.
        /// </summary>
        /// <remarks>
        /// Robert Forbes
        /// Updated: 2017/03/01
        /// 
        /// Modified to work with drop down to select status
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabOpenOrders_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _orderStatusList = _orderStatusManager.RetrieveAllOrderStatus();
                cboOrderStatus.ItemsSource = _orderStatusList;
                cboOrderStatus.SelectedIndex = 0;
                if (cboOrderStatus.SelectedItem != null)
                {
                    _currentOpenOrders = _orderManager.RetrieveProductOrdersByStatus((string)cboOrderStatus.SelectedItem);
                    lvOpenOrders.Items.Clear();

                    for (int i = 0; i < _currentOpenOrders.Count; i++)
                    {
                        this.lvOpenOrders.Items.Add(_currentOpenOrders[i]);
                    }
                    lblStatus.Content = "Status: Success";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Content += ex.ToString();
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the List View of Open Orders from Product Order.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvOpenOrders_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ProductOrder selectedItem = (ProductOrder)lvOpenOrders.SelectedItem;
                int index = selectedItem.OrderId;

                var detailsWindow = new frmProductOrderDetails(index);
                detailsWindow.Show();
            }
            catch (Exception)
            {
                lblStatus.Content = "Nothing selected";
            }

        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/01
        /// 
        /// Invokes the update of the list of orders to match the new combo box selection
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrderStatus_Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboOrderStatus.SelectedItem != null)
                {
                    _currentOpenOrders = _orderManager.RetrieveProductOrdersByStatus((string)cboOrderStatus.SelectedItem);
                    lvOpenOrders.Items.Clear();

                    for (int i = 0; i < _currentOpenOrders.Count; i++)
                    {
                        this.lvOpenOrders.Items.Add(_currentOpenOrders[i]);
                    }
                    lblStatus.Content = "Status: Success";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Content += ex.ToString();
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Add Product form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_OnClick(object sender, RoutedEventArgs e)
        {
            var frmAddProduct = new frmAddProduct(_user, _productManager);
            frmAddProduct.ShowDialog();
        }

        /// <summary>
        /// Created by Natacha Ilunga
        /// Created on 02-28-2017
        /// 
        /// Invokes the ability to navigate to Browse Products Page
        /// Invokes the Browse Products form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseProduct_Click(object sender, RoutedEventArgs e)
        {
            var frmBrowseProducts = new frmBrowseProducts(_user, _productManager);
            frmBrowseProducts.Show();
        }


        /// <summary>
        /// Created by Michael Takrama, Natacha Ilunga
        /// Creatd on 02-28-2017
        /// 
        /// Invokes method to cleanup cached files
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DisposeFiles()
        {
            try
            {
                this.DisposeImages();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// Robert Forbes
        /// 2017/03/09
        /// 
        /// Invokes Package Tab that shows all packages in the database
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPackages_Selected(object sender, RoutedEventArgs e)
        {
            RefreshPackageList();
            dgPackages.ItemsSource = _packageList;
            dgPackages.Items.Refresh();
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/01
        /// 
        /// Invokes updates the locally stored list of packages
        /// </summary>
        private void RefreshPackageList()
        {
            try
            {
                _packageList = _packageManager.RetrieveAllPackages();
            }
            catch
            {
                MessageBox.Show("Unable to retrieve packages from database");
            }
        }

        /// <summary>
        /// Mason Allen
        /// 2017/3/01
        /// Invokes Add New Vehicle form to create a new _vehicle record
        /// </summary>
        private void btnAddNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            var addNewVehicleWindow = new frmAddEditVehicle();
            addNewVehicleWindow.Show();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Manage Stock form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManageStock_Click(object sender, RoutedEventArgs e)
        {
            frmManageStock fms = new frmManageStock(_productLotManager, _productManager, _supplierManager, _locationManager);
            fms.ShowDialog();
        }

        /// <summary>
        /// Eric Walton 
        /// 2017/03/02
        /// 
        /// Invokes Vehicle Tab and a list of all vehicles.
        /// </summary>
        /// /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabVehicle_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                _vehicleList = _vehicleManager.RetrieveAllVehicles();
                dgVehicle.ItemsSource = _vehicleList;
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        /// <summary>
        /// Laura Simmonds
        /// 2017/03/24
        /// 
        /// Invokes Check Out Vehicle form with _vehicle data
        /// and allows user to update the _vehicle status to checked out or checked in.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckOutVehicle_Click(object sender, RoutedEventArgs e)
        {
            frmVehicleCheckOut vehicleCheckOut = new frmVehicleCheckOut((Vehicle)dgVehicle.SelectedItem);
            if (vehicleCheckOut.ShowDialog() == true)
            {
                dgVehicle.ItemsSource = _vehicleManager.RetrieveAllVehicles();
            }
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/02
        /// 
        /// Invokes Add Supplier form, but this calling does not 
        /// automatically set the approved status that the other one does.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplyForSupplierAct_Click(object sender, RoutedEventArgs e)
        {
            var addSupplierFrm = new frmAddSupplier(_user, _userManager, _supplierManager, _productManager, _agreementManager, "Applying");
            var addSupplierResult = addSupplierFrm.ShowDialog();
            if (addSupplierResult == true)
            {
                MessageBox.Show("Application Submitted!");
                tabSupplier_Selected(sender, e);
            }
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Change Password form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangePassword_OnClick(object sender, RoutedEventArgs e)
        {
            if (null != _user)
            {
                var updateScreen = new frmPasswordChangeView(_user.UserName);
                updateScreen.Show();
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Add Edit Vehicle form
        /// Invokes the ability to edit a _vehicle from a list of vehicles.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (dgVehicle.SelectedItem != null)
            {
                var addNewVehicleWindow = new frmAddEditVehicle((Vehicle)dgVehicle.SelectedItem);
                if (addNewVehicleWindow.ShowDialog() == true)
                {
                    try
                    {
                        _vehicleList = _vehicleManager.RetrieveAllVehicles();
                        dgVehicle.ItemsSource = _vehicleList;
                    }
                    catch (Exception ex)
                    {
                        if (null != ex.InnerException)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                        }
                        else
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Daniel Brown
        /// 2017/3/31
        /// 
        /// Invokes the Users Tab and Populates a List of Users.
        /// 
        /// NOTE: Needs to be changed to only show full users list to employees and only the current user to non-employee
        ///       Cannot be done to roles have been established.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabUser_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManager usrMgr = new UserManager();
                _userList = usrMgr.RetrieveFullUserList();
                dgUsers.ItemsSource = _userList;
            }catch(Exception){
                MessageBox.Show("There are currently no users");
            }
            if ("ADMIN" == _user.UserName)
            {
                btnResetPassword.Visibility = Visibility.Visible;
            }
            else
            {
                btnResetPassword.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Reset Password form and populates a User list.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<User> userList = _userManager.RetrieveFullUserList();
                var passwordResetWindow = new frmResetPassword(_userManager, userList);
                passwordResetWindow.Show();
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Christian Lopez
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Supplier Tab and populates a Supplier Application Status List.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabSupplier_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                _supplierApplicationStatus = _supplierManager.SupplierAppStatusList();
                cboSupplierStatus.ItemsSource = _supplierApplicationStatus;
                _supplierList = _supplierManager.ListSuppliers();
                dgSuppliers.ItemsSource = _supplierList;
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/03
        /// 
        /// Invokes the logic of what happens when the warehouse tab is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabWarehouse_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                _warehouseList = _warehouseManager.ListWarehouses();
                dgWarehouses.ItemsSource = _warehouseList;
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 2017/3/02
        /// 
        /// Invokes the Product Category form to create a new Category.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            frmProductCategory prodCategoryWindow = new frmProductCategory();
            prodCategoryWindow.Show();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Request Username form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuRequestUsername_OnClick(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                frmRequestUsername requestUsername = new frmRequestUsername();
                requestUsername.ShowDialog();
            }
            else { MessageBox.Show("Must not be signed in to use this feature"); }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Charity Approval form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApproveDeny_Click(object sender, RoutedEventArgs e)
        {
            if (dgCharity.SelectedIndex >= 0)
            {
                var frmCharityApproval = new frmCharityApproval(_charityManager, _charityList[dgCharity.SelectedIndex]);
                frmCharityApproval.ShowDialog();
                tabCharity_Selected(sender, e);
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/09
        /// 
        /// Invokes Edit Supplier form from the Add Supplier form to edit existing supplier
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (0 > dgSuppliers.SelectedIndex)
            {
                MessageBox.Show("Select a supplier to edit.");
            }
            else
            {
                var frmEditSupplier = new frmAddSupplier(_user, _userManager, _supplierManager, _productManager,
                    _agreementManager, "Editing", (Supplier)dgSuppliers.SelectedItem);
                var result = frmEditSupplier.ShowDialog();
                if (result == true)
                {
                    MessageBox.Show("Supplier Edited.");
                    tabSupplier_Selected(sender, e);
                }
            }
        }

        /// <summary>
        /// Laura Simmonds
        /// Created 2017/04/07
        /// 
        /// Launch form to view supplier application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewOpenApps_Click(object sender, RoutedEventArgs e)
        {
            if (0 > dgSuppliers.SelectedIndex)
            {
                MessageBox.Show("Select a supplier to view application");
            }
            else
            {
                var frmEditSupplier = new frmAddSupplier(_user, _userManager, _supplierManager, _productManager,
                    _agreementManager, "Viewing", (Supplier)dgSuppliers.SelectedItem);
                var result = frmEditSupplier.ShowDialog();
                if (result == true)
                {
                    tabSupplier_Selected(sender, e);
                }

            }
        }
        /// <summary>
        /// William Flood
        /// 2017/03/09
        /// 
        /// Retrieve _criteria to filter product lots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterProductLots_Click(object sender, RoutedEventArgs e)
        {
            var filterWindow = new frmProductLotSearchView(_productLotSearchCriteria);
            filterWindow.ShowDialog();
            try
            {
                List<ProductLot> productLotList;
                if (_productLotSearchCriteria.Expired)
                {
                    productLotList = _productLotManager.RetrieveExpiredProductLots();
                }
                else
                {
                    productLotList = _productLotManager.RetrieveProductLots();
                }
                dgProductLots.ItemsSource = productLotList;
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// Robert Forbes
        /// 2017/03/09
        /// 
        /// Invokes delivery management window for the selected order.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateDeliveries_Click(object sender, RoutedEventArgs e)
        {
            if (lvOpenOrders.SelectedItem != null)
            {
                if (((ProductOrder)lvOpenOrders.SelectedItem).OrderStatusId.Equals("Ready For Shipment"))
                {
                    frmCreateDeliveryForOrder deliveryWindow = new frmCreateDeliveryForOrder(((ProductOrder)lvOpenOrders.SelectedItem).OrderId);
                    deliveryWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a delivery that is ready for shipment");
                }

            }
            else
            {
                MessageBox.Show("Please select a delivery that is ready for shipment");
            }
        }
        /// <summary>
        /// Created by Natacha Ilunga
        /// 2017/3/09
        /// 
        /// Invokes Supplier Catalog Tab 
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabSupplierCatalog_Selected(object sender, RoutedEventArgs e)
        {
            //Load Supplier Data
            try
            {
                RefreshSupplierCatalog();
            }
            catch (Exception ex)
            {

                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        /// <summary>
        /// Created by Natacha Ilunga
        /// 03/09/2017
        /// 
        /// Parase Supplier Object into SupplierCatalogue view model.
        /// </summary>
        /// <param name="suppliersList"></param>
        /// <returns></returns>
        private List<SupplierCatalogViewModel> parseIntoSupplierCatalog(List<Supplier> suppliersList)
        {
            List<SupplierCatalogViewModel> viewModelList = new List<SupplierCatalogViewModel>();
            SupplierCatalogViewModel viewModel;

            foreach (var k in suppliersList)
            {
                viewModel = new SupplierCatalogViewModel()
                {
                    SupplierID = k.SupplierID,
                    FarmName = k.FarmName,
                    FarmAddress = k.FarmAddress,
                    FarmCity = k.FarmCity,
                    FarmState = k.FarmState,
                    FarmTaxID = k.FarmTaxID,
                    IsApproved = k.IsApproved,
                    UserId = k.UserId,
                    UserData = _userManager.RetrieveUser(k.UserId)
                };
                viewModelList.Add(viewModel);
            }

            return viewModelList;
        }

        /// <summary>
        /// Natacha Ilunga
        /// 2017/03/29
        /// 
        /// Invokeds  product data grid with products by supplier id.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadProductsBySupplierId_Click(object sender, MouseButtonEventArgs e)
        {

            var supplierData =  (SupplierCatalogViewModel)dgSupplierCatalogue.SelectedItem;
            dgProductListAgreements.Visibility = Visibility.Hidden;
            dgProductList.Visibility = Visibility.Visible;
            RefreshSupplierProductDataGrid(supplierData.SupplierID);

        }

        /// <summary>
        /// Created by Natacha Ilunga
        /// 04/13/17
        /// 
        /// Invokes Utility Method to refresh Supplier Catalogue Data Grid.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        private void RefreshSupplierCatalog()
        {
            dgSupplierCatalogue.ItemsSource = null;
            dgProductList.ItemsSource = null;
            try
            {
                var suppliersData = _supplierManager.ListSuppliers();

                _parsedSupplierCatalogueData = parseIntoSupplierCatalog(suppliersData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RefreshSupplierCatalogue(): " + ex.Message);

                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }

            }
            dgSupplierCatalogue.ItemsSource = _parsedSupplierCatalogueData;
        }


        /// <summary>
        /// Created by Natacha Ilunga
        /// 03/29/2016
        /// 
        /// Refreshes Products Datagrid on Supplier Catalog tab
        /// </summary>
        /// <param name="supplierId"></param>
        private void RefreshSupplierProductDataGrid(int supplierId)
        {
            dgProductList.ItemsSource = null;
            dgProductList.ItemsSource = RetrieveProductsBySupplierId(supplierId);
        }

        /// <summary>
        /// Created by Natacha Ilunga
        /// 03/29/2017
        /// 
        /// Retrieves Products by SupplierId to View Model -- Supplier Catalog Tab
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private List<BrowseProductViewModel> RetrieveProductsBySupplierId(int supplierId)
        {
            List<BrowseProductViewModel> productsbySupplierIdData = null;
            try
            {
                productsbySupplierIdData = _productManager.RetrieveProductsBySupplierId(supplierId);

                //Append Image URIs to _products lists
                try
                {
                    foreach (var a in productsbySupplierIdData)
                    {
                        a.SaveImageToTempFile();
                        a.SourceString = WpfExtensionMethods.FilePath + a.ProductId + ".jpg";
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return productsbySupplierIdData;
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Add Maintenance Record form to create a new maintenance record.
        /// Standaridized method.
        /// </summary>>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMaintenance_Click(object sender, RoutedEventArgs e)
        {
            Vehicle selectedVehicle;
            if (dgVehicle.SelectedItem != null)
            {
                selectedVehicle = (Vehicle)dgVehicle.SelectedItem;
                frmAddMaintenanceRecord addMaint = new frmAddMaintenanceRecord(selectedVehicle.VehicleID);
                addMaint.Show();
            }

        }

        //private void btnCheckSupplierStatus_Click(object sender, RoutedEventArgs e)
        //{
        //    if (_user != null)
        //    {

        //    }
        //}

        /// <summary>
        /// Bobby Thorne
        /// 2017/03/24
        /// 
        /// Populates Applications that have been submitted 
        /// both approved and also pending applications.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckApplicationStatus_Click(object sender, RoutedEventArgs e)
        {
            bool isSupplierApproved = false;
            bool isCommercialCustomerApproved = false;
            bool isCharityApproved = false;
            if (_user != null)
            {
                //btnCheckStatusDone.Visibility = Visibility.Visible;
                //btnCancelApplication.Visibility = Visibility.Visible;
                //dgMyAccount.Visibility = Visibility.Visible;
                try
                {
                    _supplier = _supplierManager.RetrieveSupplierByUserId(_user.UserId);
                    isSupplierApproved = _supplier.IsApproved;
                }
                catch
                {
                    //frmCheckSupplierStatus checkSupplierStatus = new frmCheckSupplierStatus(_user,_userManager,_supplierManager,_productManager,_agreementManager);
                    //checkSupplierStatus.Show();
                    //throw;
                }
                try
                {
                    _commercialCustomer = _customerManager.RetrieveCommercialCustomerByUserId(_user.UserId);
                    isCommercialCustomerApproved = _commercialCustomer.IsApproved;
                }
                catch
                {
                    //throw;
                }

                if (_supplier == null && _commercialCustomer == null)
                {
                    frmCheckSupplierStatus checkSupplierStatus = new frmCheckSupplierStatus(_user, _userManager, _supplierManager, _productManager, _agreementManager);
                    checkSupplierStatus.Show();
                }
            }
        }

        /// <summary>
        /// Bobby Thorne
        /// 2017/03/10
        /// 
        /// Invokes the reverts back to the original status of the tab.
        ///
        /// Removed
        /// Bobby Thorne 
        /// Updates 2017/03/10
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckApplicationStatusDone_Click(object sender, RoutedEventArgs e)
        {
            //btnCheckStatus.Visibility = Visibility.Visible;
            //btnCancelApplication.Visibility = Visibility.Collapsed;
            //btnCheckStatusDone.Visibility = Visibility.Collapsed;

        }

        /// <summary>
        /// Bobby Thorne
        /// 3/10/2017
        /// Removed
        /// 3/10/2017
        /// 
        /// Invokes the cancel applications that have been submitted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelApplication_Click(object sender, RoutedEventArgs e)
        {
            //btnCheckStatus.Visibility = Visibility.Visible;
            //btnCancelApplication.Visibility = Visibility.Collapsed;
            //btnCheckStatusDone.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the View Vehicle form.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgVehicle_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (dgVehicle.SelectedItem != null)
            {
                Vehicle vehicle = (Vehicle)dgVehicle.SelectedItem;
                frmViewVehicle vehicleWindow = new frmViewVehicle(vehicle.VehicleID);
                vehicleWindow.ShowDialog();
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// 
        /// Invokes the Supplier Invoice Tab.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabSupplierInvoice_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                _supplierInvoiceList = _supplierInvoiceManager.RetrieveAllSupplierInvoices();
                dgSupplierInvoices.ItemsSource = _supplierInvoiceList;
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tabMyAccount_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                _supplier = _supplierManager.RetrieveSupplierByUserId(_user.UserId);
                dgMyInvoices.Visibility = Visibility.Visible;
                lblMyInvoices.Visibility = Visibility.Visible;
            }
            catch 
            {
                dgMyInvoices.Visibility = Visibility.Hidden;
                lblMyInvoices.Visibility = Visibility.Hidden;
            }

            if (_supplier != null)
            {
                try
                {
                    _supplierInvoiceList = _supplierInvoiceManager.RetrieveAllSupplierInvoicesBySupplierId(_supplier.SupplierID);
                    dgMyInvoices.ItemsSource = _supplierInvoiceList;
                }
                catch (Exception ex)
                {
                    if (null != ex.InnerException)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// 
        /// Invokes double click of the Supplier Invoice Data Grid. Invokes the Supplier Invoice Details form.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Last modified by Christian Lopez 2017/03/23</remarks>
        private void dgSupplierInvoices_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (!(dgSupplierInvoices.SelectedIndex < 0))
            {
                var supplierInvoiceDetail = new frmSupplierInvoiceDetails((SupplierInvoice)dgSupplierInvoices.SelectedItem, _supplierInvoiceManager, _supplierManager);
                var result = supplierInvoiceDetail.ShowDialog();
                if (result == true)
                {
                    tabSupplierInvoice_Selected(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please select an invoice to view.");
            }
        }


        /// <summary>
        /// Bobby Thorne
        /// 2017/03/22
        /// 
        /// Handles what happens if the datagrid in my account is double clicked. Launches the detail window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Last modified by Christian Lopez 2017/03/23</remarks>
        private void dgUserInvoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(dgMyInvoices.SelectedIndex < 0))
            {
                var MyInvoicesDetail = new frmSupplierInvoiceDetails((SupplierInvoice)dgMyInvoices.SelectedItem, _supplierInvoiceManager, _supplierManager, "ReadOnly");
                var result = MyInvoicesDetail.ShowDialog();
                if (result == true)
                {
                    tabSupplierInvoice_Selected(sender, e);
                }
            }
            else
            {

                MessageBox.Show("Please select an invoice to view. " );
            }
        }


        /// <summary>
        /// Christian Lopez
        /// 2017/03/23
        /// 
        /// Invokes the logic to approve the selected invoice.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApproveInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgSupplierInvoices.SelectedIndex < 0))
            {
                try
                {
                    if (_supplierInvoiceManager.ApproveSupplierInvoice(((SupplierInvoice)dgSupplierInvoices.SelectedItem).SupplierInvoiceId))
                    {
                        tabSupplierInvoice_Selected(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Unable to approve the invoice.");
                    }
                }
                catch (Exception ex)
                {

                    if (null != ex.InnerException)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Bobby Thorne
        /// 2017/24/03
        /// 
        /// Checks user's Commercial Customer application status. 
        /// 
        /// Bobby Thorne
        /// Updated: 2017/03/31
        /// Added application form if there is no application in the system.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommercialCustomerApplicationStatusCheck_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {

                try
                {
                    _commercialCustomer = _customerManager.RetrieveCommercialCustomerByUserId(_user.UserId);

                    if (_commercialCustomer.IsApproved && _commercialCustomer.Active)
                    {
                        btnCommercialCustomerApplicationStatusCheck.Content = btnCommercialCustomerApplicationStatusCheck.Content + "\nAPPROVED";
                        btnCommercialCustomerApplicationStatusCheck.IsEnabled = false;
                        //btnCommercialCustomerApplicationStatusCheck.Background = Brushes.Green;
                    }
                    else if (!_commercialCustomer.IsApproved && _commercialCustomer.Active)
                    {
                        btnCommercialCustomerApplicationStatusCheck.Content = btnCommercialCustomerApplicationStatusCheck.Content + "\nPENDING";
                        btnCommercialCustomerApplicationStatusCheck.IsEnabled = false;
                    }
                    else if (!_commercialCustomer.IsApproved && _commercialCustomer.Active)
                    {
                        btnCommercialCustomerApplicationStatusCheck.Content = btnCommercialCustomerApplicationStatusCheck.Content + "\nDENIED";
                        btnCommercialCustomerApplicationStatusCheck.IsEnabled = false;
                    }
                }
                catch
                {
                    frmApplicationAskUser askAboutApplication = new frmApplicationAskUser(_user, _userManager, _customerManager);
                    askAboutApplication.Show();
                }
            }

        }

        /// <summary>
        /// Bobby Thorne
        /// 2017/03/24
        /// 
        /// Checks user's supplier application status 
        /// 
        /// Bobby Thorne
        /// UPDATE
        /// Updated: 2017/03/31
        /// Added application form if there is no application in the system.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupplierApplicationStatusCheck_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {

                try
                {
                    _supplier = _supplierManager.RetrieveSupplierByUserId(_user.UserId);

                    if (_supplier.IsApproved && _supplier.Active)
                    {
                        btnSupplierApplicationStatusCheck.Content = btnSupplierApplicationStatusCheck.Content + "\nAPPROVED";
                        btnSupplierApplicationStatusCheck.IsEnabled = false;
                        //btnSupplierApplicationStatusCheck.Background = Brushes.Green;
                    }
                    else if (!_supplier.IsApproved && _supplier.Active)
                    {
                        btnSupplierApplicationStatusCheck.Content = btnSupplierApplicationStatusCheck.Content + "\nPENDING";

                        btnSupplierApplicationStatusCheck.IsEnabled = false;
                    }
                    else if (!_supplier.IsApproved && !_supplier.Active)
                    {
                        btnSupplierApplicationStatusCheck.Content = btnSupplierApplicationStatusCheck.Content + "\nDENIED";
                        btnSupplierApplicationStatusCheck.IsEnabled = false;
                    }
                }
                catch
                {
                    frmApplicationAskUser askAboutApplication = new frmApplicationAskUser(_user, _userManager, _supplierManager, _productManager, _agreementManager);
                    askAboutApplication.Show();
                }
            }
        }

        /// <summary>
        /// Bobby Thorne
        /// 2017/03/24
        /// 
        /// Checks user's _charity application status.
        /// 
        /// Bobby Thorne
        /// UPDATE
        /// Updated: 2017/03/31
        /// 
        /// Added apply form if there is not an application in the system.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCharityApplicationStatusCheck_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {
                try
                {
                    _charity = _charityManager.RetrieveCharityByUserId(_user.UserId);

                    if (_charity.Status.Equals("Approved"))
                    {
                        btnCharityApplicationStatusCheck.Content = btnCharityApplicationStatusCheck.Content + "\nAPPROVED";
                        btnCharityApplicationStatusCheck.IsEnabled = false;
                        //btnCommercialCustomerApplicationStatusCheck.Background = Brushes.Green;
                    }
                    else if (_charity.Status.Equals("PENDING"))
                    {
                        btnCharityApplicationStatusCheck.Content = btnCharityApplicationStatusCheck.Content + "\nPENDING";
                        btnCharityApplicationStatusCheck.IsEnabled = false;
                    }
                    else if (_charity.Status.Equals("Denied"))
                    {
                        btnCharityApplicationStatusCheck.Content = btnCharityApplicationStatusCheck.Content + "\nDENIED";
                        btnCharityApplicationStatusCheck.IsEnabled = false;
                    }
                }
                catch
                {
                    frmApplicationAskUser askAboutApplication = new frmApplicationAskUser(_user, _userManager, _charityManager);
                    askAboutApplication.Show();
                }
            }
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 2017/03/20
        /// 
        /// Allows splitting a product lot into two smaller lots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSplitLot_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgProductLots.SelectedIndex < 0))
            {
                try
                {
                    // Call showDialog so that we know we have a result to use after this

                    var selectedItem = (ProductLot)dgProductLots.SelectedItem;
                    var frm = new frmSplitProductLot(selectedItem);
                    var result = frm.ShowDialog();

                    if (result ?? false)
                    {
                        _productLotManager.CreateProductLot(new ProductLot()
                        {
                            AvailableQuantity = frm.OldQty - (selectedItem.Quantity - selectedItem.AvailableQuantity),
                            DateReceived = selectedItem.DateReceived,
                            ExpirationDate = selectedItem.ExpirationDate,
                            LocationId = selectedItem.LocationId,
                            ProductId = selectedItem.ProductId,
                            ProductName = selectedItem.ProductName,
                            Quantity = frm.OldQty,
                            SupplierId = selectedItem.SupplierId,
                            SupplyManagerId = selectedItem.SupplyManagerId,
                            WarehouseId = selectedItem.WarehouseId
                        });
                        _productLotManager.CreateProductLot(new ProductLot()
                        {
                            AvailableQuantity = frm.NewQty,
                            DateReceived = selectedItem.DateReceived,
                            ExpirationDate = selectedItem.ExpirationDate,
                            LocationId = selectedItem.LocationId,
                            ProductId = selectedItem.ProductId,
                            ProductName = selectedItem.ProductName,
                            Quantity = frm.NewQty,
                            SupplierId = selectedItem.SupplierId,
                            SupplyManagerId = selectedItem.SupplyManagerId,
                            WarehouseId = selectedItem.WarehouseId
                        });
                        _productLotManager.DeleteProductLot(selectedItem);
                        tabProductLot_Selected(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/24
        /// 
        /// Invokes the View Maintenance Records form.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewMaintenance_Click(object sender, RoutedEventArgs e)
        {
            if(dgVehicle.SelectedIndex > 0){
                frmViewMaintenanceRecords viewMaintenanceRecordsWindow = new frmViewMaintenanceRecords(((Vehicle)dgVehicle.SelectedItem).RepairList);
                viewMaintenanceRecordsWindow.ShowDialog();
            }
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 2017/3/23
        /// 
        /// Brings up the Product Lot detail window
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProductLots_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {
            var productLot = (ProductLot)dgProductLots.SelectedItem;
            var productLotMgr = new ProductLotManager();
            var productLotDetail = new frmAddProductLot(productLotMgr, productLot);
            productLotDetail.ShowDialog();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Menu Quit.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirmation:", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Menu Preferences.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuPreferences_OnClick(object sender, RoutedEventArgs e)
        {
            _preferenceManager = new PreferenceManager();
            var frmPreferences = new frmMnuPreferences(_preferenceManager);
            frmPreferences.ShowDialog();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the Deliveries Tab.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDeliveries_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _deliveries = _deliveryManager.RetrieveDeliveries();
                var deliveriesWithVehicleId = new List<ExpandoObject>();
                foreach (var item in _deliveries)
                {
                    dynamic newItem = new ExpandoObject();
                    newItem.DeliveryDate = item.DeliveryDate;
                    newItem.StatusId = item.StatusId;
                    newItem.DeliveryTypeId = item.DeliveryTypeId;
                    newItem.VehicleId = _deliveryManager.RetrieveVehicleByDelivery(item.DeliveryId.Value).VehicleID;
                    deliveriesWithVehicleId.Add(newItem);
                }
                lvDeliveries.Items.Clear();

                for (int i = 0; i < _deliveries.Count; i++)
                {
                    lvDeliveries.Items.Add(deliveriesWithVehicleId[i]);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes the list View of Deliveries and ability to Add Edit a Delivery.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvDeliveries_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {
            Delivery delivery;
            if (lvDeliveries.SelectedItem != null)
            {
                delivery = _deliveries[lvDeliveries.SelectedIndex];
            }
            else
            {
                MessageBox.Show("Please select a delivery to edit.");
                return;
            }
            var editForm = new frmAddEditDelivery(delivery, _deliveryManager, true);
            var result = editForm.ShowDialog();
            _deliveries = _deliveryManager.RetrieveDeliveries();
            lvDeliveries.Items.Refresh();
        }

        /// <summary>
        /// Created on 2017-03-30 by William Flood
        /// Invokes the click event on btnAddSupplierInventory
        /// 
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Modified by Christian Lopez on 2017/04/03 to handle all exceptions</remarks>
        private void btnAddSupplierInventory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = _supplierManager.RetrieveSupplierByUserId(_user.UserId);
                if(null!=supplier)
                {
                    var agreementList = _agreementManager.RetrieveAgreementsBySupplierId(supplier.SupplierID);
                    if(0==agreementList.Count)
                    {
                        MessageBox.Show("Create an agreement first!");
                        return;
                    }
                    var supplierInventoryWindow = new frmAddSupplierInventory(_supplierInventoryManager,agreementList);
                    supplierInventoryWindow.ShowDialog();
                }
            } catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        
        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Invokes Supplier Invoice form.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmitSupplierInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (_supplierList == null)
            {
                try
                {
                    _supplierList = _supplierManager.ListSuppliers();
                }
                catch (Exception ex)
                {

                    if (null != ex.InnerException)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return;
                }
                
            }
            // See if the current user is a supplier
            if (_supplierList.Find(s => s.UserId == _user.UserId) != null)
            {
                var supplierInvoiceForm = new frmSubmitSupplierInvoice(_supplierList.Find(s => s.UserId == _user.UserId), _productLotManager, _supplierInvoiceManager);
                supplierInvoiceForm.ShowDialog();
            }
            else
            {
                var selectSupplierForm = new frmSelectSupplier(_supplierList);
                var result = selectSupplierForm.ShowDialog();
                if (result == true)
                {
                    // We have a supplier in the form
                    var supplierInvoiceForm = new frmSubmitSupplierInvoice(selectSupplierForm.selectedSupplier, _productLotManager, _supplierInvoiceManager);
                    var innerResult = supplierInvoiceForm.ShowDialog();
                    if (innerResult == true)
                    {
                        tabSupplierInvoice_Selected(sender, e);
                        MessageBox.Show("Invoice submitted");
                    }
                }
            }
            
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Invokes Add Warehouse form to create a new warehouse.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNewWarehouse_Click(object sender, RoutedEventArgs e)
        {
            frmAddWarehouse addWarehouseWindow = new frmAddWarehouse();
            addWarehouseWindow.ShowDialog();
		}
		
        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// Invokes the Update Supplier Invoice form and the ability to add/edit a
        /// supplier invoice.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateInvoice_Click(object sender, RoutedEventArgs e)
        {

            if(dgSupplierInvoices.SelectedIndex >= 0){
                frmUpdateSupplierInvoice updateSupplierInvoiceWindow = new frmUpdateSupplierInvoice((SupplierInvoice)dgSupplierInvoices.SelectedItem);
                updateSupplierInvoiceWindow.ShowDialog();
                tabSupplierInvoice_Selected(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an invoice");
            }
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 4/6/2017
        /// 
        /// Populates the datagrid for suppliers based on supplier applications status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSupplierStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string txt = cboSupplierStatus.SelectedItem.ToString();
            List<Supplier> supplierApps = new List<Supplier>();
            try
            {
                if (cboSupplierStatus.SelectedItem != null)
                {
                    if (txt == "Pending")
                    {
                        _supplierList = _supplierManager.ListSuppliers();
                        supplierApps = _supplierList.FindAll(s => s.IsApproved == false);
                        dgSuppliers.ItemsSource = supplierApps;
                    }
                    else if (txt == "Approved")
                    {
                        _supplierList = _supplierManager.ListSuppliers();
                        supplierApps = _supplierList.FindAll(s => s.IsApproved == true);
                        dgSuppliers.ItemsSource = supplierApps;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		
        /// Bobby Thorne
        /// 2017/04/12
        /// 
        /// Invokes Approval form for supplier.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupplierApproval_Click(object sender, RoutedEventArgs e)
        {
            if (dgSuppliers.SelectedIndex >= 0)
            {
                frmApproval ApprovalWindow = new frmApproval(_supplierManager, (Supplier)dgSuppliers.SelectedItem, _user.UserId);
                ApprovalWindow.ShowDialog();
                createAgreementForApprovedSupplier((Supplier)dgSuppliers.SelectedItem);
                tabSupplier_Selected(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a supplier account to approve.");
            }
        }

        /// <summary>
        /// Invokes Approval form for Commercial Customer
        /// </summary>
        ////// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/17
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCCAccountApproval_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomer.SelectedIndex >= 0)
            {
                frmApproval ApprovalWindow = new frmApproval(_customerManager, (CommercialCustomer)dgCustomer.SelectedItem, _user.UserId);
                ApprovalWindow.ShowDialog();
                tabSupplier_Selected(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a Commercial account to approve.");
            }
        }

        private void refreshUserInvoices()
        {

            try
            {
                _supplier = _supplierManager.RetrieveSupplierByUserId(_user.UserId);
            }
            catch
            {
                dgMyInvoices.Visibility = Visibility.Hidden;
                lblMyInvoices.Visibility = Visibility.Hidden;
            }

            if (_supplier != null)
            {
                try
                {
                    _supplierInvoiceList = _supplierInvoiceManager.RetrieveAllSupplierInvoicesBySupplierId(_supplier.SupplierID);
                    dgMyInvoices.ItemsSource = _supplierInvoiceList;
                }
                catch (Exception ex)
                {
                    if (null != ex.InnerException)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void tabMyAccount_GotFocus(object sender, RoutedEventArgs e)
        {
            refreshUserInvoices();
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 4/13/2017
        /// 
        /// Prompts the user to create an agreement for supplier after clicking approve supplier.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="supplier"></param>
        private void createAgreementForApprovedSupplier(Supplier supplier)
        {
            MessageBox.Show("Select products for the approved supplier to create an agreement.");

            try
            {
                var frmEditSupplier = new frmAddSupplier(_user, _userManager, _supplierManager, _productManager,
                            _agreementManager, "Editing", supplier);
                frmEditSupplier.btnSubmit.Visibility = Visibility.Hidden;
                frmEditSupplier.btnSubmitAgreement.Visibility = Visibility.Visible;
                var result = frmEditSupplier.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 4/20/2017
        /// 
        /// View agreements of supplier selected in supplier catalogue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgreement_Click(object sender, RoutedEventArgs e)
        {
            List<Agreement> agreementList = null;
            SupplierCatalogViewModel supplier = null;
            try
            {
                supplier = (SupplierCatalogViewModel)dgSupplierCatalogue.SelectedItem;
                int supplierId = supplier.SupplierID;
                agreementList = _agreementManager.RetrieveAgreementsBySupplierId(supplierId);
                
                List < AgreementWithProductName > newList = new List<AgreementWithProductName>();
                foreach(var agreement in agreementList)
                {
                    AgreementWithProductName temp = new AgreementWithProductName()
                    {
                        ProductName = _productLotManager.RetrieveProductLotById(agreement.ProductId).ProductName,
                        Active = agreement.Active,
                        AgreementId = agreement.AgreementId,
                        ProductId = agreement.ProductId,
                        SupplierId = agreement.SupplierId,
                        DateSubmitted = agreement.DateSubmitted,
                        IsApproved = agreement.IsApproved,
                        ApprovedBy = agreement.ApprovedBy
                    };
                    newList.Add(temp);
                }
                

                if(agreementList == null)
                {
                    MessageBox.Show("No agreement product list found for this supplier.");
                }
                else
                {
                    dgProductList.Visibility = Visibility.Hidden;
                    dgProductListAgreements.Visibility = Visibility.Visible;
                    dgProductListAgreements.ItemsSource = newList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    } // end of class
} // end of namespace 
