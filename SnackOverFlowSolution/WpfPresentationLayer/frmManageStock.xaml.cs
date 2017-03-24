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
using LogicLayer;
using DataObjects;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Created by Michael Takrama
    /// 03/01/2017
    /// 
    /// Interaction logic for frmManageStock.xaml
    /// </summary>
    public partial class frmManageStock : Window
    {
        private List<ProductLot> _productLotList;

        private List<ManageStockViewModel> _manageStockViewModels = new List<ManageStockViewModel>();

        private bool checkBoxOverride;

        public frmManageStock()
        {
            InitializeComponent();
            RetrieveProductLots();
            ParseLotsIntoViewModel();
            RefreshDgProductLot();
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 3/2/2017
        /// 
        /// Parses the lots into a View Model for Datagrid
        /// </summary>
        private void ParseLotsIntoViewModel()
        {
            var productManager = new ProductManager();
            var supplierManager = new SupplierManager();
            var locationManager = new LocationManager();

            try
            {
                _manageStockViewModels.Clear();
                foreach (var a in _productLotList)
                {
                    var manageStockViewModel = new ManageStockViewModel
                    {
                        ProductId = a.ProductId,
                        ProductName = productManager.RetrieveProductById((int)a.ProductId).Name,
                        SupplierId = a.SupplierId,
                        SupplierName = supplierManager.RetrieveSupplierBySupplierID((int)a.SupplierId).FarmName,
                        LocationDesc = locationManager.RetrieveLocationByID((int)a.LocationId).Description,
                        Quantity = a.Quantity,
                        AvailableQuantity = a.AvailableQuantity,
                        ProductLotId = a.ProductLotId
                    };

                    if (manageStockViewModel.AvailableQuantity != 0 || checkBoxOverride)
                        _manageStockViewModels.Add(manageStockViewModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Catastrophic Error: " + ex.Message);
            }

        }

        /// <summary>
        /// Created by Michael Takrama
        /// 3/2/2017
        /// 
        /// Refreshes Datagrid
        /// </summary>
        private void RefreshDgProductLot()
        {
            dgProductList.ItemsSource = null;
            dgProductList.ItemsSource = _manageStockViewModels;
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 3/2/2017
        /// 
        /// Retrieves Product Lots from Database
        /// </summary>
        private void RetrieveProductLots()
        {
            var pm = new ProductLotManager();

            try
            {
                _productLotList = pm.RetrieveProductLots();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 3/2/2017
        /// 
        /// Updates Available Quantity of Product Lot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateQuantity_OnClick(object sender, RoutedEventArgs e)
        {
            var pm = new ProductLotManager();

            ManageStockViewModel msv = (ManageStockViewModel)dgProductList.SelectedItem;
            int selectedIndex = dgProductList.SelectedIndex;

            ProductLot oldProductLot = _productLotList.Find(x => x.ProductLotId == msv.ProductLotId);

            frmManageStockSubAdjustQty fmsa = new frmManageStockSubAdjustQty(oldProductLot);
            fmsa.ShowDialog();

            var newProductLot = new ProductLot
            {
                ProductLotId = oldProductLot.ProductLotId,
                WarehouseId = oldProductLot.WarehouseId,
                SupplierId = oldProductLot.SupplierId,
                LocationId = oldProductLot.LocationId,
                ProductId = oldProductLot.ProductId,
                SupplyManagerId = oldProductLot.SupplyManagerId,
                Quantity = oldProductLot.Quantity,
                AvailableQuantity = fmsa.getNewQuantity(),
                DateReceived = oldProductLot.DateReceived,
                ExpirationDate = oldProductLot.ExpirationDate
            };

            try
            {
                if (pm.UpdateProductLotAvailableQuantity(oldProductLot, newProductLot) == 1)
                {
                    MessageBox.Show("Quantity Updated Successfully");
                    RetrieveProductLots();
                    ParseLotsIntoViewModel();
                    RefreshDgProductLot();
                    dgProductList.SelectedIndex = selectedIndex;
                }
                else
                {
                    MessageBox.Show("Error Updating quantity. Kindly refresh and try again");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in update." + ex.Message);
            }


        }

        /// <summary>
        /// Created by Michael Takrama
        /// 3/2/2017
        /// 
        /// Makes Product Lots with 0 Available Products Visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkInactiveLots_OnChecked(object sender, RoutedEventArgs e)
        {
            checkBoxOverride = true;
            ParseLotsIntoViewModel();
            RefreshDgProductLot();
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 3/2/2017
        /// 
        /// Hides Products Lots with 0 Available Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkInactiveLots_OnUnchecked(object sender, RoutedEventArgs e)
        {
            checkBoxOverride = false;
            ParseLotsIntoViewModel();
            RefreshDgProductLot();
        }

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 3/2/2017
        /// 
        /// Refreshes Datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshData_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RetrieveProductLots();
                ParseLotsIntoViewModel();
                RefreshDgProductLot();
                MessageBox.Show("Refresh Successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Catastrophic Error: " + ex.Message);
            }

        }
    }
}
