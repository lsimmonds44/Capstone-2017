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
    /// 
    /// Updated by Mason Allen
    /// Updated on 5/2/17
    /// Updated to include params for the passed product instead of creating a new blank one
    /// </summary>
    public partial class frmViewProduct : Window
    {
        Product currentProduct;

        IProductManager _productManager;


        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize View Product Window.
        /// Standardized method.
        /// 
        /// Updated by Mason Allen
        /// Updated on 5/2/17
        /// Added product arg to constructor
        /// </summary>
        public frmViewProduct(Product selectedProduct)
        {
            _productManager = new ProductManager();
            currentProduct = selectedProduct;
            InitializeComponent();
            displayProductDetails();
        }


        /// <summary>
        /// Laura Simmonds 
        /// 2017/27/02
        /// 
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Displays Product Details.
        /// Standardized method.
        /// 
        /// Updated by Mason Allen
        /// Updated on 5/2/17
        /// Method now retrieves the db record for the selected product instead of a test record
        /// </summary>
        public void displayProductDetails()
        {
            try
            {
                currentProduct = _productManager.RetrieveProductById(currentProduct.ProductId);
                txtProductID.Text = currentProduct.ProductId.ToString();
                txtDescription.Text = currentProduct.Description;
                txtProductName.Text = currentProduct.Name;
                txtPrice.Text = currentProduct.DeliveryChargePerUnit.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("There was an error retrieving product details");
            }
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Cancels and Closes the View Product details.
        /// Standardized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
