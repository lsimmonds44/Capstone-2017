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

        List<Location> locationList;
        List<Product> productList;
        List<Supplier> supplierList;
        List<Employee> employeeList;
        List<Warehouse> warehouseList;
        public int supplierId { get; private set; }
        public frmAddProductLot()
        {
            InitializeComponent();
            _productLotManager = new ProductLotManager();
            FillDefaultValues();
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
                locationList = (new LocationManager()).ListLocations();
                cbxLocationIDVal.ItemsSource = locationList;
                cbxLocationIDVal.Visibility = Visibility.Visible;
                productList = (new DummyProductManager()).ListProducts();
                cbxProductIDVal.ItemsSource = productList;
                cbxProductIDVal.Visibility = Visibility.Visible;
                supplierList = (new SupplierManager()).ListSuppliers();
                cbxSupplierIDVal.ItemsSource = supplierList;
                cbxSupplierIDVal.Visibility = Visibility.Visible;
                employeeList = (new EmployeeManager()).RetrieveEmployeeList();
                cbxSupplyManagerIDVal.ItemsSource = employeeList;
                cbxSupplyManagerIDVal.Visibility = Visibility.Visible;
                warehouseList = (new WarehouseManager()).ListWarehouses();
                cbxWarehouseIDVal.ItemsSource = warehouseList;
                cbxWarehouseIDVal.Visibility = Visibility.Visible;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorAlert.ShowDatabaseError();
            }
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            int quantityRead = 0;
            bool shouldPost = cbxLocationIDVal.SelectedIndex >= 0;
            shouldPost = shouldPost && cbxProductIDVal.SelectedIndex >= 0;
            shouldPost = shouldPost && cbxSupplierIDVal.SelectedIndex >= 0;
            shouldPost = shouldPost && cbxSupplyManagerIDVal.SelectedIndex >= 0;
            shouldPost = shouldPost && cbxWarehouseIDVal.SelectedIndex >= 0;
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
                    ProductId = productList[cbxProductIDVal.SelectedIndex].ProductId,
                    LocationId = locationList[cbxLocationIDVal.SelectedIndex].LocationId,
                    SupplierId = supplierList[cbxSupplierIDVal.SelectedIndex].SupplierID,
                    SupplyManagerId = (int)employeeList[cbxSupplyManagerIDVal.SelectedIndex].EmployeeId,
                    WarehouseId = warehouseList[cbxWarehouseIDVal.SelectedIndex].WarehouseID,
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
                        supplierId = supplierList[cbxSupplierIDVal.SelectedIndex].SupplierID;
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
    }
}
