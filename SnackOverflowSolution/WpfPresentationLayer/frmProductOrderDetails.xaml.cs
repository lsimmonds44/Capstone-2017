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
    /// Interaction logic for frmProductOrderDetails.xaml
    /// </summary>
    public partial class frmProductOrderDetails : Window
    {
        public frmProductOrderDetails()
        {
            InitializeComponent();
        }

        int orderID;

        public frmProductOrderDetails(int orderID)
        {
            InitializeComponent();
            this.orderID = orderID;
            displayDetails();
        }

        ProductOrder prodOrd = new ProductOrder();
        IProductOrderManager ordMgr = new ProductOrderManager();



        public void displayDetails()
        {
            try
            {
                prodOrd = ordMgr.retrieveProductOrderDetails(orderID);
                txtOrderId.Text = prodOrd.OrderId.ToString();
                txtCustomerId.Text = prodOrd.CustomerId.ToString();
                txtOrderType.Text = prodOrd.OrderTypeId;
                txtAddressType.Text = prodOrd.AddressType;
                txtDeliveryType.Text = prodOrd.DeliveryTypeId;
                txtAmount.Text = prodOrd.Amount.ToString();
                txtOrderDate.Text = prodOrd.OrderDate.ToString();
                txtExpectedDate.Text = prodOrd.DateExpected.ToString();
                txtDiscount.Text = prodOrd.Discount.ToString();
                txtOrderStatus.Text = prodOrd.OrderStatusId;
                txtUserAddress.Text = prodOrd.UserAddressId.ToString();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnPackaging_Click(object sender, RoutedEventArgs e)
        {
            frmProductOrderPackages pakMgr = new frmProductOrderPackages(this.orderID);
            pakMgr.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
