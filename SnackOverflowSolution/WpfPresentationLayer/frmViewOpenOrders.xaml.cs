using System;
using DataObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LogicLayer;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewOrdersByStatus.xaml
    /// </summary>
    public partial class frmViewOrdersByStatus : Window
    {
        private ProductOrderManager _myProductOrderManager = new ProductOrderManager();
        List<ProductOrder> _currentOpenOrders;
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize Open Orders Window and Open Orders List View.
        /// Standardized method.
        /// </summary>
        public frmViewOrdersByStatus()
        {
            InitializeComponent();

            try
            {
                _currentOpenOrders = _myProductOrderManager.RetrieveProductOrdersByStatus("Open");
                lvOpenOrders.Items.Clear();

                for(int i=0; i<_currentOpenOrders.Count; i++)
                {
                    this.lvOpenOrders.Items.Add(_currentOpenOrders[i].OrderId);
                    this.lvOpenOrders.Items.Add(_currentOpenOrders[i].CustomerId);
                    this.lvOpenOrders.Items.Add(_currentOpenOrders[i].OrderDate);
                    this.lvOpenOrders.Items.Add(_currentOpenOrders[i].DateExpected);
                    this.lvOpenOrders.Items.Add(_currentOpenOrders[i].OrderStatusId);
                }
                lblStatus.Content += "Success";
            }
            catch(Exception e)
            {
                lblStatus.Content += "Something went wrong - " + e;
            }
        }
    }
}