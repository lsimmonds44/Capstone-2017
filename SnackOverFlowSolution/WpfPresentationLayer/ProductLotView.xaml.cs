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
    /// Interaction logic for ProductLotView.xaml
    /// </summary>
    public partial class ProductLotView : Window
    {
        List<Location> locationList;
        List<Product> productList;
        List<Supplier> supplierList;
        List<Employee> employeeList;
        List<Warehouse> warehouseList;
        public int supplierId { get; private set; }
        public ProductLotView()
        {
            InitializeComponent();
        }

        public void SetEditable()
        {
            lblDateReceivedVal.Visibility = Visibility.Collapsed;
            lblExpirationDateVal.Visibility = Visibility.Collapsed;
            lblLocationIDVal.Visibility = Visibility.Collapsed;
            lblProductIDVal.Visibility = Visibility.Collapsed;
            lblProductLotIDVal.Visibility = Visibility.Collapsed;
            lblQuantityVal.Visibility = Visibility.Collapsed;
            lblSupplierIDVal.Visibility = Visibility.Collapsed;
            lblSupplyManagerIDVal.Visibility = Visibility.Collapsed;
            lblWarehouseIDVal.Visibility = Visibility.Collapsed;
            btnPost.Visibility = Visibility.Visible;

            try
            {
                locationList = (new DummyLocationManager()).ListLocations();
                cbxLocationIDVal.ItemsSource = locationList;
                cbxLocationIDVal.Visibility = Visibility.Visible;
                productList = (new DummyProductManager()).ListProducts();
                cbxProductIDVal.ItemsSource = productList;
                cbxProductIDVal.Visibility = Visibility.Visible;
                supplierList = (new DummySupplierManager()).ListSuppliers();
                cbxSupplierIDVal.ItemsSource = supplierList;
                cbxSupplierIDVal.Visibility = Visibility.Visible;
                employeeList = (new EmployeeManager()).RetrieveEmployeeList();
                cbxSupplyManagerIDVal.ItemsSource = employeeList;
                cbxSupplyManagerIDVal.Visibility = Visibility.Visible;
                warehouseList = (new DummyWarehouseManager()).ListWarehouses();
                cbxWarehouseIDVal.ItemsSource = warehouseList;
                cbxWarehouseIDVal.Visibility = Visibility.Visible;
                txtQuantityVal.Visibility = Visibility.Visible;
                dpkDateReceivedVal.Visibility = Visibility.Visible;
                dpkExpirationDateVal.Visibility = Visibility.Visible;
            } catch (System.Data.SqlClient.SqlException ex)
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
            if(!Int32.TryParse(txtQuantityVal.Text, out quantityRead) && quantityRead >= 0)
            {
                shouldPost = false;
                MessageBox.Show("Quantity needs a whole number value");
            }
            if (null == dpkDateReceivedVal.SelectedDate)
            {
                shouldPost = false;
                MessageBox.Show("Add a date for Date Received");
            }
            if (null == dpkExpirationDateVal.SelectedDate)
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
                    DateReceived = dpkDateReceivedVal.SelectedDate,
                    ExpirationDate = dpkExpirationDateVal.SelectedDate
                };
                try
                {
                    (new ProductLotManager()).AddProductLot(toSave);
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
            ProductLot newProductLot = new ProductLot();
        }
    }
}
