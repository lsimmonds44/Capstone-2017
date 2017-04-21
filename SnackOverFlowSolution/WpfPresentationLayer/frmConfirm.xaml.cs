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
    /// 4/20/2017
    /// 
    /// Interaction logic for frmConfirm.xaml
    /// </summary>
    public partial class frmConfirm : Window
    {
        ProductLot _prodLOt;
        IProductLotManager _prodLotManager;

        public frmConfirm(IProductLotManager prodLotManager, ProductLot prodLot)
        {
            _prodLotManager = prodLotManager;
            _prodLOt = prodLot;
            InitializeComponent();
        }

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

        private void ConfirmCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
