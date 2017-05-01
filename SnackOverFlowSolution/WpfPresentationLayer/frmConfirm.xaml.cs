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
    /// Ryan Spurgetis
    /// Created:  2017/4/20
    /// 
    /// Interaction logic for frmConfirm.xaml
    /// </summary>
    /// <summary>
    /// Alissa Duffy
    /// Updated: 2017/04/24
    /// 
    /// Standardized Comments.
    /// Standardized Methods.
    /// </summary>
    public partial class frmConfirm : Window
    {
        ProductLot _prodLOt;
        IProductLotManager _prodLotManager;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize Confirm Form.
        /// Standardized methods. 
        /// </summary>
        /// <param name="prodLotManager"></param>
        /// <param name="prodLot"></param>
        public frmConfirm(IProductLotManager prodLotManager, ProductLot prodLot)
        {
            _prodLotManager = prodLotManager;
            _prodLOt = prodLot;
            InitializeComponent();
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Confirm ok to Delete product.
        /// Standardized methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_prodLotManager.DeleteProductLot(_prodLOt))
                {
                    MessageBox.Show("Product lot removed.");
                    Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error deleting product lot:" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Confirm Cancel.
        /// Standardized methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    } // End of class
} // End of namespace
