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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Laura Simmonds
    /// 2017/27/02
    /// 
    /// Interaction logic for ViewProduct.xaml
    /// </summary>
    public partial class frmViewProduct : Window
    {
        Product _product = new Product();

        IProductManager _productManager;


        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize View Product Window.
        /// Standaridized method.
        /// </summary>
        public frmViewProduct()
        {
            _productManager = new ProductManager();
            InitializeComponent();

        }

        int productID;

        /// <summary>
        /// Laura Simmonds 
        /// 2017/27/02
        /// 
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Displays Product Details.
        /// Standaridized method.
        /// </summary>
        public void displayProductDetails()
        {
            try
            {
                int productID = 10000;
                _product = _productManager.RetrieveProductById(productID);
                txtProductID.Text = _product.ProductId.ToString();
                txtDescription.Text = _product.Description;
                txtProductName.Text = _product.Name;
                txtPrice.Text = _product.DeliveryChargePerUnit.ToString();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Cancels and Closes the View Product details.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Invokes the View Product Details Display.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewProduct_Click(object sender, RoutedEventArgs e)
        {
            this.displayProductDetails();
        }
    }
}
