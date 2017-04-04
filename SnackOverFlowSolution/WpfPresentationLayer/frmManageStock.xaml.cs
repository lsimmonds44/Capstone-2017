using System;
using System.Collections.Generic;
using System.Windows;
using DataObjects;
using LogicLayer;

namespace WpfPresentationLayer
{
    /// <summary>
    ///     Created by Michael Takrama
    ///     03/01/2017
    ///     Interaction logic for frmManageStock.xaml
    /// </summary>
    public partial class frmManageStock : Window
    {
        private bool _checkBoxOverride;
        private ILocationManager _locationManager;
        private List<ManageStockViewModel> _manageStockViewModels = new List<ManageStockViewModel>();
        private List<ProductLot> _productLotList;
        private readonly IProductLotManager _productLotManager;
        private IProductManager _productManager;
        private ISupplierManager _supplierManager;

        public frmManageStock(IProductLotManager productLotManager, IProductManager productManager,
            ISupplierManager supplierManager, ILocationManager locationManager)
        {
            _productLotManager = productLotManager;
            _supplierManager = supplierManager;
            _productManager = productManager;
            _locationManager = locationManager;
            InitializeComponent();
            RetrieveProductLots();
            ParseLotsIntoViewModel();
            RefreshDgProductLot();
        }

        /// <summary>
        ///     Created by Michael Takrama
        ///     3/2/2017
        ///     Parses the lots into a View Model for Datagrid
        /// </summary>
        private void ParseLotsIntoViewModel()
        {
            _productManager = new ProductManager();
            _supplierManager = new SupplierManager();
            _locationManager = new LocationManager();

            try
            {
                _manageStockViewModels.Clear();
                foreach (var a in _productLotList)
                {
                    var manageStockViewModel = new ManageStockViewModel
                    {
                        ProductId = a.ProductId,
                        ProductName = _productManager.RetrieveProductById((int) a.ProductId).Name,
                        SupplierId = a.SupplierId,
                        SupplierName = _supplierManager.RetrieveSupplierBySupplierID((int) a.SupplierId).FarmName,
                        LocationDesc = _locationManager.RetrieveLocationByID((int) a.LocationId).Description,
                        Quantity = a.Quantity,
                        AvailableQuantity = a.AvailableQuantity,
                        ProductLotId = a.ProductLotId
                    };

                    if (manageStockViewModel.AvailableQuantity != 0 || _checkBoxOverride)
                        _manageStockViewModels.Add(manageStockViewModel);
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
                    MessageBox.Show("Catastrophic Error: " + ex.Message);
                }
                
            }
        }

        /// <summary>
        ///     Created by Michael Takrama
        ///     3/2/2017
        ///     Refreshes Datagrid
        /// </summary>
        private void RefreshDgProductLot()
        {
            dgProductList.ItemsSource = null;
            dgProductList.ItemsSource = _manageStockViewModels;
        }

        /// <summary>
        ///     Created by Michael Takrama
        ///     3/2/2017
        ///     Retrieves Product Lots from Database
        /// </summary>
        private void RetrieveProductLots()
        {
            try
            {
                _productLotList = _productLotManager.RetrieveProductLots();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        /// <summary>
        ///     Created by Michael Takrama
        ///     3/2/2017
        ///     Updates Available Quantity of Product Lot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateQuantity_OnClick(object sender, RoutedEventArgs e)
        {
            if (dgProductList.SelectedItem == null)
            {
                MessageBox.Show("Kindly select a product lot");
                return;
            }

            var selectedProductLot = (ManageStockViewModel) dgProductList.SelectedItem;
            var selectedIndex = dgProductList.SelectedIndex;

            var oldProductLot = _productLotList.Find(x => x.ProductLotId == selectedProductLot.ProductLotId);

            //Open sub form for update
            var valueFromSubUdateView = new frmManageStockSubAdjustQty(oldProductLot);
            valueFromSubUdateView.ShowDialog();

            var newProductLot = new ProductLot
            {
                ProductLotId = oldProductLot.ProductLotId,
                WarehouseId = oldProductLot.WarehouseId,
                SupplierId = oldProductLot.SupplierId,
                LocationId = oldProductLot.LocationId,
                ProductId = oldProductLot.ProductId,
                SupplyManagerId = oldProductLot.SupplyManagerId,
                Quantity = oldProductLot.Quantity,
                AvailableQuantity = valueFromSubUdateView.getNewQuantity(),
                DateReceived = oldProductLot.DateReceived,
                ExpirationDate = oldProductLot.ExpirationDate
            };

            if (ValidateQuantityUpdates(newProductLot, oldProductLot))
                return;

            try

            {
                if (_productLotManager.UpdateProductLotAvailableQuantity(oldProductLot, newProductLot) == 1)
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
        ///     Created by Michael Takram
        ///     04/01/17
        ///     Validates quantities from user
        /// </summary>
        /// <param name="newProductLot"></param>
        /// <param name="oldProductLot"></param>
        /// <returns></returns>
        private static bool ValidateQuantityUpdates(ProductLot newProductLot, ProductLot oldProductLot)
        {
            if (newProductLot.AvailableQuantity == oldProductLot.AvailableQuantity)
            {
                MessageBox.Show("No changes mades.");
                return true;
            }

            if (newProductLot.AvailableQuantity > oldProductLot.Quantity)
            {
                MessageBox.Show("New Available cannot be higher than quantity for product lot. Updates not effected");
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Created by Michael Takrama
        ///     3/2/2017
        ///     Makes Product Lots with 0 Available Products Visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkInactiveLots_OnChecked(object sender, RoutedEventArgs e)
        {
            _checkBoxOverride = true;
            ParseLotsIntoViewModel();
            RefreshDgProductLot();
        }

        /// <summary>
        ///     Created by Michael Takrama
        ///     3/2/2017
        ///     Hides Products Lots with 0 Available Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkInactiveLots_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _checkBoxOverride = false;
            ParseLotsIntoViewModel();
            RefreshDgProductLot();
        }

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Created by Michael Takrama
        ///     3/2/2017
        ///     Refreshes Datagrid
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

        /// <summary>
        ///     Created by Michael Takrama
        ///     04/01/2017
        ///     Search criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            RetrieveProductLots();
            ParseLotsIntoViewModel();

            _manageStockViewModels = _manageStockViewModels
                .FindAll(s => string.Equals(
                    s.ProductName,
                    txtSearchCriteria.Text,
                    StringComparison.CurrentCultureIgnoreCase)
                );
            RefreshDgProductLot();
        }
    }
}