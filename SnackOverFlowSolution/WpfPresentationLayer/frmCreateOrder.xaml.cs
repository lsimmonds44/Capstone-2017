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
    /// Interaction logic for frmCreateOrder.xaml
    /// </summary>
    public partial class frmCreateOrder : Window
    {

        private int _employee_Id;
        private CommercialCustomer _cCustomer;
        private IUserManager _userManager = new UserManager();
        private IProductOrderManager _orderManager = new ProductOrderManager();
        private IOrderLineManager _orderLineManager = new OrderLineManager();
        private User _cCUser;
        private int _orderNum;
        private List<ProductLot> _productLots;
       
        public frmCreateOrder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Eric Walton
        /// 2017/10/3
        /// Window initializer for a commercial customer
        /// </summary>
        /// <param name="employee_Id"></param>
        /// <param name="customer"></param>
        public frmCreateOrder(int employee_Id, CommercialCustomer customer)
        {
            InitializeComponent();
            _employee_Id = employee_Id;
            _cCustomer = customer;
            displayCustomerInfo();
        }

        /// <summary>
        /// Eric Walton
        /// 2017/10/3
        /// Sets the approprite fields on the window to the data for the selected customer.
        /// </summary>
        private void displayCustomerInfo()
        {
            try
            {
                _cCUser = _userManager.RetrieveUser(_cCustomer.User_Id);
            }
            catch (Exception)
            {
                MessageBox.Show("Commercial Customer user account could not be retrieved.");
            }
            tfCustomerID.Text = _cCustomer.Commercial_Id.ToString();
            tfCustomerUserName.Text = _cCUser.UserName;
            tfUserAddress.Text = "Address will go here.";
            tfOrderType.Text = "Commercial Customer";
            dpOrderDate.SelectedDate = DateTime.Now;
            cboDeliveryType.Items.Add("Truck");
            dpExpectedDate.SelectedDate = DateTime.Now.AddDays(5);

        }

        /// <summary>
        /// Eric Walton
        /// 2017/10/3
        /// Checks to make sure all the needed infor is supplied to create and order.
        /// </summary>
        /// <returns></returns>
        private ProductOrder validateOrder()
        {
            ProductOrder order = new ProductOrder();
            bool valid = true;
            if (tfCustomerID.Text.Length < 5)
            {
                valid = false;
                MessageBox.Show("The customer Id field is empty go back to customer tab on main menu select a customer and try again.");
            }
            if (cboDeliveryType.SelectedItem == null)
            {
                valid = false;
                MessageBox.Show("A delivery type must be selected.");
            }
            if (!valid)
            {
                order = null;
            }
            else
            {
                order.EmployeeId = _employee_Id;
                order.CustomerId = parseToInt(tfCustomerID.Text);
                order.OrderTypeId = tfOrderType.Text;
                order.AddressType = "Commercial";
                order.DeliveryTypeId = cboDeliveryType.SelectedItem.ToString();
                order.Amount = (decimal)0.0;
                order.OrderDate = dpOrderDate.SelectedDate;
                order.DateExpected = dpExpectedDate.SelectedDate;
                order.Discount = (decimal)0.0;
                order.OrderStatusId = "Open";
                order.UserAddressId = _cCUser.PreferredAddressId;
                order.HasArrived = false;
            }

            return order;
        }

        /// <summary>
        /// Eric Walton
        /// 2017/10/3
        /// Invokes a method in the product order manager to create an order
        /// and get the order number for the created order from the database
        /// and then sets the order number on the window to the order number.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool createOrder(ProductOrder order)
        {
            bool result = false;
            try
            {
                _orderNum = _orderManager.createProductOrder(order);
                tfOrderNumber.Text = _orderNum.ToString();
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to initialize an order. " + e);
            }
            return result;
        }

        private void displayProductLots()
        {
            ProductLotManager pLM = new ProductLotManager();
            
            try
            {
                _productLots = pLM.RetrieveProductLotsWithGradeAndPrice();
                foreach (var product in _productLots)
                {
                    cboProducts.Items.Add(product.ProductName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving product lots" + ex);
            }
            


        }

        /// <summary>
        /// Eric Walton
        /// 2017/10/3
        /// The action for start order button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartOrderClick(object sender, RoutedEventArgs e)
        {
            var order = validateOrder();
            if (order != null)
            {
                if (createOrder(order))
                {
                    btnAddOrderLine.IsEnabled = true;
                    cboProducts.IsEnabled = true;
                    displayProductLots();
                } 
            }

        }




        /// <summary>
        /// Eric Walton
        /// 2017/06/02 
        /// 
        /// Helper method to parse a string to an int
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int parseToInt(String input)
        {
            int result;

            int.TryParse(input, out result);
            return result;
        }

        private void productSelected(object sender, EventArgs e)
        {
            if (cboProducts.SelectedIndex >= 0)
            {
                ProductLot selectedProduct = _productLots[cboProducts.SelectedIndex];
                tfAvailableProduct.Text = selectedProduct.Quantity.ToString();
                lblProductGradeResult.Content = selectedProduct.Grade.ToString();
            }
        }

        private void AddOrderLineClick(object sender, RoutedEventArgs e)
        {
            if (tfQty.Text.Length > 0)
            {
                if (parseToInt(tfQty.Text) <= parseToInt(tfAvailableProduct.Text))
                {


                    OrderLine oLine = new OrderLine();
                    oLine.ProductOrderID = _orderNum;
                    oLine.ProductID = _productLots[cboProducts.SelectedIndex].ProductId;
                    oLine.ProductName = cboProducts.SelectedItem.ToString();
                    oLine.Quantity = parseToInt(tfQty.Text);
                    oLine.GradeID = lblProductGradeResult.Content.ToString();
                    oLine.Price = 100;
                    oLine.UnitDiscount = (decimal)0.0;
                    try
                    {
                        _orderLineManager.CreateOrderLine(oLine);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to add product to order." + ex);
                    }
                }
                else
                {
                    MessageBox.Show("Quantity must be lower or equal to available product.");
                }
            }
            else
            {
                MessageBox.Show("Need to enter a quantity.");
            }
            RefreshOrderLines();
        }

        public void RefreshOrderLines()
        {

            dgOrderLines.ItemsSource = _orderLineManager.RetrieveOrderLineListByProductOrderId(_orderNum);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        


    } // end of class
} // end of namespace
