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
    /// Created By Natacha Ilunga on 2/10/2017
    /// 
    /// Code Behind for BrowseProducts.xaml
    /// </summary>
    public partial class frmBrowseProducts : Window
    {
        /// <summary>
        /// Products View Model
        /// </summary>
        List<BrowseProductViewModel> _products = new List<BrowseProductViewModel>();

        /// <summary>
        /// Vendor Filter List
        /// </summary>
        List<string> _vendors = new List<string>();

        /// <summary>
        /// Category Filter List
        /// </summary>
        List<string> _categories = new List<string>();

        User _currentUser;
        IProductManager _productManager;

        public frmBrowseProducts(User user, IProductManager iProductManager)
        {
            InitializeComponent();
            _currentUser = user;
            _productManager = iProductManager;
            RetrieveProducts();
            RefreshProductsDataGrid();
            FillFilterGrids();
        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Created on 2/10/2017
        /// 
        /// Retrieves Products from DB
        /// </summary>
        public void RetrieveProducts()
        {

            _products = _productManager.RetrieveProductsToBrowseProducts();

            //Append Image URIs to _products lists
            try
            {
                foreach (var a in _products)
                {
                    a.SaveImageToTempFile();
                    a.SourceString = WpfExtensionMethods.FilePath + a.ProductId + ".jpg";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }


        }

        /// <summary>
        /// Created By Natacha Ilunga 
        /// Created on 02/15/17
        /// 
        /// Applies Filters to Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {

            double min, max;

            try
            {
                min = txtFmPrice.Text == "" ? 0 : double.Parse(txtFmPrice.Text);
                max = txtToPrice.Text == "" ? 0 : double.Parse(txtToPrice.Text);

                if (min < 0 || max < 0)
                {
                    txtFmPrice.BorderBrush = System.Windows.Media.Brushes.Red;
                    txtToPrice.BorderBrush = System.Windows.Media.Brushes.Red;
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Inputs");
                txtFmPrice.BorderBrush = System.Windows.Media.Brushes.Red;
                txtToPrice.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }

            _products = _productManager.FilterProducts(_vendors, _categories, min, max);

            RefreshProductsDataGrid();

            //Report Empty Grid
            if (dgProductList.Items.Count == 0)
                MessageBox.Show("No Products Meet this Criteria");

        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Creatd on 02/10/2017
        /// 
        /// Remplit les grilles de filtrage avec des éléments de filtre distincts basés sur les produits chargés.
        /// </summary>
        private void FillFilterGrids()
        {
            dgVendors.ItemsSource = _products.Select(x => x.Supplier_Name).Distinct().ToList();
            dgCategories.ItemsSource = _products.Select(x => x.CategoryID).Distinct().ToList();
        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Created on 02/11/17
        /// 
        /// Refreshes Data Grid with Products
        /// </summary>
        private void RefreshProductsDataGrid()
        {
            //Append Image URIs to _products lists
            foreach (var a in _products)
            {
                a.SourceString = WpfExtensionMethods.FilePath + a.ProductId + ".jpg";
            }

            dgProductList.ItemsSource = _products;

            if (_products.Count == 0)
                txtPrompts.Text = "No Items to display";
        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Creatd  on 02/10/17
        /// 
        /// Changes From Price TextBox Back to Black on Text Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFmPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtFmPrice.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Creatd on on 02/10/17
        /// 
        /// Changes To Price TextBox Back to Black on Text Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtToPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtToPrice.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        /// <summary>
        /// Created By Natacha Ilunga
        /// Created on 02/10/2017
        /// 
        /// Adds Items to Vendor Filter List on 2/10/2017
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkVendorItem_Checked(object sender, RoutedEventArgs e)
        {
            _vendors.Add(dgVendors.SelectedItem.ToString());
        }

        /// <summary>
        /// Created By Natacha Ilunga 
        /// Creatd on on 2/10/2017
        /// 
        /// Removes Items from Vendor Filter List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkVendorItem_Unchecked(object sender, RoutedEventArgs e)
        {
            _vendors.Remove(dgVendors.SelectedItem.ToString());
        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Created on 2/10/2017
        /// 
        /// Adds Items to Category Filter List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCategoryItem_Checked(object sender, RoutedEventArgs e)
        {
            _categories.Add(dgCategories.SelectedItem.ToString());
        }

        /// <summary>
        /// Created by Natacha Ilunga
        /// Created on 02/10/2017
        /// 
        /// Removes Items to Category Filter List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCategoryItem_Unchecked(object sender, RoutedEventArgs e)
        {
            _categories.Remove(dgCategories.SelectedItem.ToString());
        }

        private void BrowseProducts_OnClosed(object sender, EventArgs e) //trigger subscriber-event call
        {
            dgProductList.ItemsSource = null;
        }
    }

}
