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
    /// Interaction logic for frmAddProductLot.xaml
    /// </summary>
    public partial class frmAddProductLot : Window
    {
        private IProductLotManager _productLotManager;
        private IPickupManager _pickupManager;
        private ISupplierManager _supplierManager;
        private ProductLot _productLot;
        private PickupLine _pickupLine;
        private Employee _employee;

        List<Location> _locationList;
        List<Product> _productList;
        List<Supplier> _supplierList;
        List<Employee> _employeeList;
        List<Warehouse> _warehouseList;
        public int supplierId { get; private set; }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize of Add Product Lot View Window.
        /// Standardized method.
        /// </summary>
        public frmAddProductLot()
        {
            InitializeComponent();
            _productLotManager = new ProductLotManager();
            FillDefaultValues();
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize of Add Product Lot View Window.
        /// Standardized method.
        /// </summary>
        /// <param name="prodMgr"></param>
        /// <param name="prodLot"></param>
        public frmAddProductLot(ProductLotManager prodMgr, ProductLot prodLot)
        {
            InitializeComponent();
            _productLotManager = prodMgr;
            _productLot = prodLot;
            FillProductLotDetails(_productLot);

        }

        /// <summary>
        /// Ryan Spurgetis
        /// 4/28/2017
        /// 
        /// Initialize product lot window based on pickup selected
        /// </summary>
        /// <param name="pickupManager"></param>
        /// <param name="pickupLine"></param>
        public frmAddProductLot(IPickupManager pickupManager, PickupLine pickupLine, Employee employee)
        {
            InitializeComponent();
            _productLotManager = new ProductLotManager();
            _pickupManager = pickupManager;
            _pickupLine = pickupLine;
            _employee = employee;
            SetValuesFromPickupLine(_pickupLine);
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Fills in the Default Values
        /// Standardized method. 
        /// </summary>
        private void FillDefaultValues()
        {
            txtQuantity.Text = "1";
            dpExpirationDate.SelectedDate = DateTime.Now;
            dpDateReceived.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Set Editable View.
        /// Standardized method.
        /// </summary>
        public void SetEditable()
        {
            dpDateReceived.Visibility = Visibility.Visible;
            dpExpirationDate.Visibility = Visibility.Visible;
            lblLocationIDVal.Visibility = Visibility.Collapsed;
            lblProductVal.Visibility = Visibility.Collapsed;
            lblQuantityVal.Visibility = Visibility.Visible;
            lblSupplierVal.Visibility = Visibility.Collapsed;
            lblSupplyManagerIDVal.Visibility = Visibility.Collapsed;
            lblWarehouseIDVal.Visibility = Visibility.Collapsed;
            btnPost.Visibility = Visibility.Visible;

            try
            {
                _locationList = (new LocationManager()).ListLocations();
                cboLocationIDVal.ItemsSource = _locationList;
                cboLocationIDVal.Visibility = Visibility.Visible;
                _productList = (new ProductManager()).ListProducts();
                cboProductIDVal.ItemsSource = _productList;
                cboProductIDVal.Visibility = Visibility.Visible;
                _supplierList = (new SupplierManager()).ListSuppliers();
                cboSupplierIDVal.ItemsSource = _supplierList;
                cboSupplierIDVal.Visibility = Visibility.Visible;
                _employeeList = (new EmployeeManager()).RetrieveEmployeeList();
                cboSupplyManagerIDVal.ItemsSource = _employeeList;
                cboSupplyManagerIDVal.Visibility = Visibility.Visible;
                _warehouseList = (new WarehouseManager()).ListWarehouses();
                cboWarehouseIDVal.ItemsSource = _warehouseList;
                cboWarehouseIDVal.Visibility = Visibility.Visible;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Saves Changes to Product Lot View.
        /// Standardized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            int quantityRead = 0;
            bool shouldPost = cboLocationIDVal.SelectedIndex >= 0;
            shouldPost = shouldPost && cboProductIDVal.SelectedIndex >= 0;
            shouldPost = shouldPost && cboSupplierIDVal.SelectedIndex >= 0;
            shouldPost = shouldPost && cboSupplyManagerIDVal.SelectedIndex >= 0;
            shouldPost = shouldPost && cboWarehouseIDVal.SelectedIndex >= 0;
            if(!Int32.TryParse(txtQuantity.Text, out quantityRead) && quantityRead >= 0)
            {
                shouldPost = false;
                MessageBox.Show("Quantity needs a whole number value");
            }
            if (null == dpDateReceived.SelectedDate)
            {
                shouldPost = false;
                MessageBox.Show("Add a date for Date Received");
            }
            if (null == dpExpirationDate.SelectedDate)
            {
                shouldPost = false;
                MessageBox.Show("Add a date for the Expiration Date");
            }
            if(shouldPost)
            {
                ProductLot toSave = new ProductLot()
                {
                    ProductId = _productList[cboProductIDVal.SelectedIndex].ProductId,
                    LocationId = _locationList[cboLocationIDVal.SelectedIndex].LocationId,
                    SupplierId = _supplierList[cboSupplierIDVal.SelectedIndex].SupplierID,
                    SupplyManagerId = (int)_employeeList[cboSupplyManagerIDVal.SelectedIndex].EmployeeId,
                    WarehouseId = _warehouseList[cboWarehouseIDVal.SelectedIndex].WarehouseID,
                    Quantity = quantityRead,
                    AvailableQuantity = quantityRead,
                    DateReceived = dpDateReceived.SelectedDate,
                    ExpirationDate = dpExpirationDate.SelectedDate
                };
                try
                {
                    _productLotManager.CreateProductLot(toSave);
                    MessageBox.Show("Product Lot Added");
                    try
                    {
                        supplierId = _supplierList[cboSupplierIDVal.SelectedIndex].SupplierID;
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                    this.DialogResult = true;
                } catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 3/24/2017
        /// 
        /// Populates the fields of product lot window based on clicked product lot
        /// </summary>
        /// <param name="_productLot"></param>
        public void FillProductLotDetails(ProductLot _productLot)
        {
            btnClose.Content = "Close";
            try
            {
                lblSupplierVal.Content = _productLot.SupplierId;
                lblWarehouseIDVal.Content = _productLot.WarehouseId;
                lblProductVal.Content = _productLot.ProductName;
                lblLocationIDVal.Content = _productLot.LocationId;
                lblQuantityVal.Content = _productLot.Quantity;
                lblSupplyManagerIDVal.Content = _productLot.SupplyManagerId;
                dpExpirationDate.SelectedDate = _productLot.ExpirationDate;
                dpDateReceived.SelectedDate = _productLot.DateReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured", ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 4/29/2017
        /// 
        /// Loads the form window based on values from pickup selected
        /// </summary>
        /// <param name="_pickupLine"></param>
        private void SetValuesFromPickupLine(PickupLine _pickupLine)
        {
            _supplierManager = new SupplierManager();
            ProductManager _productManager = new ProductManager();
            btnCreateFromPickup.Visibility = Visibility.Visible;

            try
            {
                Pickup _pickup = _pickupManager.RetrievePickupById(_pickupLine.PickupId);
                var supplierName = _supplierManager.RetrieveSupplierBySupplierID((int)_pickup.SupplierId);
                var productName = _productManager.RetrieveProductById((int)_pickupLine.ProductId).Name;
                lblSupplierVal.Content = supplierName;
                cboSupplierIDVal.SelectedIndex = (int)_pickup.SupplierId;
                cboSupplierIDVal.Visibility = Visibility.Collapsed;
                lblWarehouseIDVal.Content = (int)_pickup.WarehouseId;
                cboWarehouseIDVal.SelectedIndex = (int)_pickup.WarehouseId;
                cboWarehouseIDVal.Visibility = Visibility.Collapsed;
                lblProductVal.Content = productName;
                cboProductIDVal.SelectedItem = (int)_pickupLine.ProductId;
                cboProductIDVal.Visibility = Visibility.Collapsed;
                txtQuantity.Text = _pickupLine.Quantity.ToString();
                _locationList = (new LocationManager()).ListLocations();
                cboLocationIDVal.ItemsSource = _locationList;
                cboLocationIDVal.Visibility = Visibility.Visible;
                lblSupplyManagerID.Visibility = Visibility.Hidden;
                cboSupplyManagerIDVal.Visibility = Visibility.Hidden;
                dpDateReceived.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occured" + ex.Message + ex.StackTrace);
            }

        }

        /// <summary>
        /// Ryan Spurgetis
        /// 4/28/2017
        /// 
        /// Sends the create product lot info from pickup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateFromPickup_Click(object sender, RoutedEventArgs e)
        {
            int lotQuantity = 0;

            Pickup pickup = _pickupManager.RetrievePickupById(_pickupLine.PickupId);
            var productId = (int)_pickupLine.ProductId;
            var lotSupplierId = (int)pickup.SupplierId;
            var warehouseId = (int)lblWarehouseIDVal.Content;

            if (null == txtQuantity.Text)
            {
                MessageBox.Show("Add a quantity for the product lot.");
            }
            else
            {
                bool canConvertQ = int.TryParse(txtQuantity.Text, out lotQuantity);
                if(canConvertQ == false)
                {
                    MessageBox.Show("Quantity needs a whole number value");
                }
            }
            if (null == cboLocationIDVal.SelectedItem)
            {
                MessageBox.Show("Select a location for lot.");
                return;
            }
            //if (null == cboSupplyManagerIDVal.SelectedItem)
            //{
            //    MessageBox.Show("Select a supply manager for lot.");
            //    return;
            //}
            if (null == dpDateReceived.SelectedDate)
            {
                MessageBox.Show("Add a date for Date Received");
                return;
            }
            if (null == dpExpirationDate.SelectedDate)
            {
                MessageBox.Show("Add a date for the Expiration Date");
                return;
            }
            ProductLot newLot = new ProductLot()
            {
                WarehouseId = warehouseId,
                SupplierId = lotSupplierId,
                LocationId = _locationList[cboLocationIDVal.SelectedIndex].LocationId,
                ProductId = productId,
                SupplyManagerId = _employee.EmployeeId,
                //SupplyManagerId = (int)_employeeList[cboSupplyManagerIDVal.SelectedIndex].EmployeeId,
                Quantity = lotQuantity,
                AvailableQuantity = lotQuantity,
                DateReceived = dpDateReceived.SelectedDate,
                ExpirationDate = dpExpirationDate.SelectedDate
            };

            try
            {
                _productLotManager.CreateProductLot(newLot);
                MessageBox.Show("Product Lot Added");
                _pickupManager.DeletePickupLine(_pickupLine);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured. " + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Closes Product Lot View Window.
        /// Standardized method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
