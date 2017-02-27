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
    /// Interaction logic for ViewProduct.xaml
    /// </summary>
    public partial class frmViewProduct : Window
    {
        Product _product = new Product();

        IProductManager _productManager;



        public frmViewProduct()
        {
            _productManager = new ProductManager();
            InitializeComponent();

        }

        int productID;
        //public frmViewProduct(int productID)
        //{
        //    this.productID = productID;          
        //}

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


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnViewProdct_Click(object sender, RoutedEventArgs e)
        {
            this.displayProductDetails();
        }
    }
}
