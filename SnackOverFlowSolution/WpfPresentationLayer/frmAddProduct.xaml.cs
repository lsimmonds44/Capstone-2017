using DataObjects;
using LogicLayer;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmCreateProduct.xaml
    /// </summary>
    public partial class frmAddProduct : Window
    {

        private Product _product = new Product();
        private User _currentUser;
        private IProductManager _productManager;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize the Add Product form.
        /// Standardized method.
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="iproductManager"></param>
        public frmAddProduct(User currentUser, IProductManager iproductManager)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _productManager = iproductManager;
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Saves Product's Image.
        /// Standardized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                string imageName = openFileDialog.FileName;
                ImgProduct.Source = new BitmapImage(new Uri(imageName, UriKind.Absolute));
                byte[] bitPattern = File.ReadAllBytes(imageName);
                _product.ImageBinary = bitPattern;
            }
        }

        /// <summary>
        /// Michael Takrama 
        /// Created: 2017/02/10
        /// 
        /// Close form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Michael Takrama 
        /// Created: 2017/02/10
        /// 
        /// Saves User Input To DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (1 == ValidateInput())
            {
                return;
            }

            GetUserInput();

            try
            {
                if (1 == _productManager.CreateProduct(_product))
                {
                    MessageBox.Show("Product Created Successfully");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        /// <summary>
        /// Michael Takrama 
        /// Created: 2017/02/10
        /// 
        /// Clears fields
        /// </summary>
        private void ClearFields()
        {
            txtName.Clear();
            txtProductDescription.Clear();
            txtUnitPrice.Clear();
            chkActive.IsChecked = false;
            txtUnitOfMeasure.Clear();
            txtDeliveryChargePerUnit.Clear();
            ImgProduct.Source = null;
            _product = new Product();
        }

        /// <summary>
        /// Michael Takrama 
        /// Created: 2017/02/10
        /// 
        /// Gets User Input
        /// </summary>
        /// <returns></returns>
        private void GetUserInput()
        {
            _product.Name = txtName.Text;
            _product.Description = txtProductDescription.Text;
            _product.UnitPrice = txtUnitPrice.Text == "" ? 0 : decimal.Parse(txtUnitPrice.Text);
            _product.Active = chkActive.IsChecked;
            _product.UnitOfMeasurement = txtUnitOfMeasure.Text;
            _product.DeliveryChargePerUnit = txtDeliveryChargePerUnit.Text == "" ? 0 : decimal.Parse(txtDeliveryChargePerUnit.Text);

        }

        /// <summary>
        /// Michael Takrama 
        /// Created: 2017/02/10
        /// 
        /// Validates User Input.
        /// </summary>
        private int ValidateInput()
        {
            int signal = 0;

            if ("" == txtName.Text)
            {
                txtName.BorderBrush = System.Windows.Media.Brushes.Red;
                signal = 1;
            }

            if ("" == txtProductDescription.Text)
            {
                txtProductDescription.BorderBrush = System.Windows.Media.Brushes.Red;
                signal = 1;
            }

            if ("" != txtUnitOfMeasure.Text)
            {
                try
                {
                    decimal.Parse(txtUnitPrice.Text);
                }
                catch (FormatException fe)
                {
                    txtUnitPrice.BorderBrush = System.Windows.Media.Brushes.Red;
                    signal = 1;
                }
            }

            if ("" != txtDeliveryChargePerUnit.Text)
            {
                try
                {
                    decimal.Parse(txtDeliveryChargePerUnit.Text);
                }
                catch (FormatException fe)
                {

                    txtDeliveryChargePerUnit.BorderBrush = System.Windows.Media.Brushes.Red;
                    signal = 1;
                }
            }

            if (signal == 1)
            {
                MessageBox.Show("Invalid Entry/No Input inputs");
            }

            return signal;

        }
    } // End of class
} // End of namespace
