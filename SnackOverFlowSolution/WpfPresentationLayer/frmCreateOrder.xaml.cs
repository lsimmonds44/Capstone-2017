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
        private Decimal _orderTotal = 0;
        private ProductLot _currentlySelectedProductLot;
       
        public frmCreateOrder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Eric Walton
        /// 2017/03/10
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
        /// 2017/03/10
        /// Sets the approprite fields on the window to the data for the selected customer.
        /// 
        /// Laura Simmonds
        /// Updated: 2017/04/21
        /// </summary>
        private void displayCustomerInfo()
        {
            //UserAddress userAddress;
            try
            {
                _cCUser = _userManager.RetrieveUser(_cCustomer.UserId);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Commercial Customer user account could not be retrieved.");
            }
            try
            {
                //userAddress = _userManager.RetrieveUserAddress(_cCUser.PreferredAddressId.Value);
                txtUserAddress.Text = (_cCUser.AddressLineOne ?? "") + (_cCUser.AddressLineTwo ?? "") + "\n" + (_cCUser.City ?? "") + " " + (_cCUser.State ?? "") + " " + (_cCUser.Zip ?? "");
                //txtUserAddress.Text = userAddress.AddressLineOne + "\n" + userAddress.AddressLineTwo + "\n" + userAddress.City + " " + userAddress.State + " " + userAddress.Zip;
            }
            catch (Exception)
            {
                txtUserAddress.Text = "User address not found. Check address info on user tab.";
            }

            txtCustomerID.Text = _cCustomer.CommercialId.ToString();
            txtCustomerUserName.Text = _cCUser.UserName;
            txtOrderType.Text = "Commercial Customer";
            dpOrderDate.SelectedDate = DateTime.Now;
            cboDeliveryType.Items.Add("Truck");
            dpExpectedDate.SelectedDate = DateTime.Now.AddDays(5);
        }

        /// <summary>
        /// Eric Walton
        /// 2017/03/10
        /// Checks to make sure all the needed info is supplied to create and order.
        /// 
        /// Laura Simmonds
        /// Updated: 2017/04/21
        /// </summary>
        /// <returns></returns>
        private ProductOrder validateOrder()
        {
            ProductOrder order = new ProductOrder();
            bool valid = true;
            if (txtCustomerID.Text.Length < 5)
            {
                valid = false;
                MessageBox.Show("The customer Id field is empty go back to customer tab on main menu select a customer and try again.");
            }
            if (cboDeliveryType.SelectedItem == null)
            {
                valid = false;
                MessageBox.Show("A delivery type must be selected.");
            }
            if (_cCUser.AddressLineOne == null || _cCUser.City == null)
            {
                valid = false;
                MessageBox.Show("Customer does not have an address. Go to user tab and resolve the issue.");
            }
            if (!valid)
            {
                order = null;
            }
            else
            {
                order.EmployeeId = _employee_Id;
                order.CustomerId = parseToInt(txtCustomerID.Text);
                order.OrderTypeId = txtOrderType.Text;
                order.Address1 = _cCUser.AddressLineOne;
                order.Address2 = _cCUser.AddressLineTwo;
                order.City = _cCUser.City;
                order.State = _cCUser.State;
                order.Zip = _cCUser.Zip;
                order.AddressType = "Commercial";
                order.DeliveryTypeId = cboDeliveryType.SelectedItem.ToString();
                order.Amount = (decimal)0.0;
                order.OrderDate = dpOrderDate.SelectedDate;
                order.DateExpected = dpExpectedDate.SelectedDate;
                order.Discount = (decimal)0.0;
                order.OrderStatusId = "Open";
                order.HasArrived = false;
            }

            return order;
        }

        /// <summary>
        /// Eric Walton
        /// 2017/03/10
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
                txtOrderNumber.Text = _orderNum.ToString();
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to initialize an order. " + e);
            }
            return result;
        }

        /// <summary>
        /// Eric Walton
        /// 2017/03/10
        /// helper method to display the product lots information
        /// </summary>
        private void displayProductLots()
        {
            ProductLotManager pLM = new ProductLotManager();
            
            try
            {
                _productLots = pLM.RetrieveActiveProductLots();
                if (_productLots.Count < 1)
                {
                    MessageBox.Show("No available productlots. Check to see if product lots are ready to be inspected.");
                }
                else
                {
                    foreach (var product in _productLots)
                    {
                        cboProducts.Items.Add(product.ProductName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving product lots" + ex);
            }
            


        }

        /// <summary>
        /// Eric Walton
        /// 2017/03/10
        /// The action for start order button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartOrderClick(object sender, RoutedEventArgs e)
        {
            if (btnStartOrder.Content.ToString() == "Start Order")
            {
                var order = validateOrder();
                if (order != null)
                {
                    if (createOrder(order))
                    {
                        btnAddOrderLine.IsEnabled = true;
                        cboProducts.IsEnabled = true;
                        displayProductLots();
                        btnStartOrder.Content = "Done";
                    }
                }
            }
            else
            {
                DialogResult = true;
            }
        }




        /// <summary>
        /// Eric Walton
        /// 2017/02/06
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

        /// <summary>
        /// Eric Walton
        /// 2017/03/24
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productSelected(object sender, EventArgs e)
        {
            if (cboProducts.SelectedIndex >= 0)
            {
                _currentlySelectedProductLot = _productLots[cboProducts.SelectedIndex];
                txtAvailableProduct.Text = _currentlySelectedProductLot.Quantity.ToString();
                lblProductGradeResult.Content = _currentlySelectedProductLot.Grade.ToString();
                lblProductPrice.Content = "$" + _currentlySelectedProductLot.Price.ToString();
            }
        }

        /// <summary>
        /// Eric Walton
        /// /// 2017/02/06 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrderLineClick(object sender, RoutedEventArgs e)
        {
            if (txtQty.Text.Length > 0)
            {
                if (parseToInt(txtQty.Text) <= parseToInt(txtAvailableProduct.Text))
                {
                    if (_currentlySelectedProductLot.Price <= 0)
                    {
                        MessageBox.Show("Productlot has not been priced. Not available for order yet.");
                    }
                    else
                    {
                        OrderLine oLine = new OrderLine();
                        oLine.ProductOrderID = _orderNum;
                        oLine.ProductID = _productLots[cboProducts.SelectedIndex].ProductId;
                        oLine.ProductName = cboProducts.SelectedItem.ToString();
                        oLine.Quantity = parseToInt(txtQty.Text);
                        oLine.GradeID = lblProductGradeResult.Content.ToString();
                        oLine.Price = _currentlySelectedProductLot.Price.Value;
                        oLine.UnitDiscount = (decimal)0.0;
                        try
                        {
                            _orderLineManager.CreateOrderLine(oLine);
                            _orderTotal += oLine.Price * oLine.Quantity;
                            txtOrderAmount.Text = "$" + _orderTotal.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to add product to order." + ex);
                        }
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

        /// <summary>
        /// Eric Walton
        /// 2017/02/06
        /// Refreshes the list of orderlines
        /// </summary>
        private void RefreshOrderLines()
        {
            var oLines = _orderLineManager.RetrieveOrderLineListByProductOrderId(_orderNum, _orderTotal);
            dgOrderLines.ItemsSource = oLines;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        


    } // end of class
} // end of namespace
