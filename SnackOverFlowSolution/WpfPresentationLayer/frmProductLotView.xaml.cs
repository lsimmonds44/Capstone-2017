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
        private ProductLot _productLot;

        List<Location> _locationList;
        List<Product> _productList;
        List<Supplier> _supplierList;
        List<Employee> _employeeList;
        List<Warehouse> _warehouseList;
        public int supplierId { get; private set; }
        public frmAddProductLot()
        {
            InitializeComponent();
            _productLotManager = new ProductLotManager();
            FillDefaultValues();
        }

        public frmAddProductLot(ProductLotManager prodMgr, ProductLot prodLot)
        {
            InitializeComponent();
            _productLotManager = prodMgr;
            _productLot = prodLot;
            FillProductLotDetails(_productLot);

        }

        private void FillDefaultValues()
        {
            txtQuantity.Text = "1";
            dpExpirationDate.SelectedDate = DateTime.Now;
            dpDateReceived.SelectedDate = DateTime.Now;
        }

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
                ErrorAlert.ShowDatabaseError();
            }
        }

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
                    DateReceived = dpDateReceived.SelectedDate,
                    ExpirationDate = dpExpirationDate.SelectedDate
                };
                try
                {
                    _productLotManager.AddProductLot(toSave);
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
                    ErrorAlert.ShowDatabaseError();
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
