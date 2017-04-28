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
    /// Interaction logic for frmProductCategory.xaml
    /// </summary>
    public partial class frmProductCategory : Window
    {

        CategoryManager _prodCatMgr = new CategoryManager();

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize Product Category Form.
        /// Standaridized methods. 
        /// </summary>
        public frmProductCategory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Save new Product Category.
        /// Standaridized methods. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var categoryName = txtCategoryName.Text;
            var categoryDesc = txtCategoryDesc.Text;

            if (categoryName == "")
            {
                MessageBox.Show("Must supply a category name.");
                txtCategoryName.Focus();
                return;
            }

            if (categoryDesc == "")
            {
                MessageBox.Show("Must supply a category description.");
                txtCategoryDesc.Focus();
                return;
            }

            try
            {
                var category = new Category()
                {
                    CategoryID = categoryName,
                    Description = categoryDesc
                };
                if (_prodCatMgr.CreateCategory(category))
                {
                    MessageBox.Show("Category added!");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to create a new product category. Please check fields and try again");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem" + ex.StackTrace);
                txtCategoryName.Clear();
                txtCategoryDesc.Clear();
                txtCategoryName.Focus();
            }

        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Cancels/Closes Product Category Form.
        /// Standaridized methods. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    } // End of class
} // End of namespace
