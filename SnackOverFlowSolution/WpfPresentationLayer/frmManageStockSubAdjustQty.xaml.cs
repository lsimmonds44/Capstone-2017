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
    /// 2017/04/02
    /// 
    /// Interaction logic for frmManageProductSubAdjustQty.xaml
    /// </summary>
    public partial class frmManageStockSubAdjustQty : Window
    {
        private ProductLot _oldProductLot;
        private int? _newQuantity;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize Manage Stock Sub Adjust Qty Window.
        /// Standaridized method.
        /// </summary>
        /// <param name="oldProductLot"></param>
        public frmManageStockSubAdjustQty(ProductLot oldProductLot)
        {
            InitializeComponent();
            _oldProductLot = oldProductLot;
            _newQuantity = oldProductLot.AvailableQuantity;
            SetupWindow();
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize Setup of Manage Stock Sub Adjust Qty Window.
        /// Standaridized method.
        /// </summary>
        private void SetupWindow()
        {
            ProductManager pm = new ProductManager();
            txtProductLotID.Text = _oldProductLot.ProductLotId.ToString();
            txtProductName.Text = pm.RetrieveProductById((int)_oldProductLot.ProductId).Name;
            txtCurrentQuantity.Text = _oldProductLot.AvailableQuantity.ToString();
            txtNewQuantity.Text = _newQuantity.ToString();
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 2017/03/02
        /// Modified on 2017/04/01
        /// 
        /// Returns newQuantity to parent form
        /// </summary>
        /// <returns></returns>
        public int? getNewQuantity()
        {
            return _newQuantity;
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Updates Available Quantity.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                _newQuantity = (int)Double.Parse(txtNewQuantity.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Illegal Argument");
                _newQuantity = (int)_oldProductLot.AvailableQuantity;
            }
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 2017/03/02
        /// Modified on 2017/04/17
        /// 
        /// Validates user input
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            if (txtNewQuantity.Text == "")
            {
                return false;
            }
            try
            {
                if (Double.Parse(txtNewQuantity.Text) < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
