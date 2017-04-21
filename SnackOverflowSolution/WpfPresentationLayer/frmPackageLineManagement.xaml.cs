///<summary>
///Robert Forbes
///2017/02/07
///
/// Interaction logic for PackageLineManagement.xaml
///</summary>
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
    public partial class frmPackageLineManagementWindow : Window
    {
        Package _package;
        IPackageLineManager _packageLineManager;

        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// Constructor for the window
        /// </summary>
        /// <param name="p">The package that the package lines a relevant to</param>
        public frmPackageLineManagementWindow(Package p)
        {
            _package = p;
            _packageLineManager = new PackageLineManager();
            InitializeComponent();
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// Method for handling when the add package line button is clicked
        /// Checks to make sure the data entered is valid and then attempts
        /// to run the database access method to create a package line from the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPackageLine_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            int productLotID = 0;
            int quantity = 0;
            decimal price = 0.0M;

            txtPricePaid.BorderBrush = System.Windows.Media.Brushes.Black;
            txtProductLot.BorderBrush = System.Windows.Media.Brushes.Black;
            txtQuantity.BorderBrush = System.Windows.Media.Brushes.Black;
            try
            {
                productLotID = int.Parse(txtProductLot.Text);
            }
            catch
            {
                valid = false;
                //Seting the outline of the text box to red if its not valid
                txtProductLot.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            try
            {
                quantity = int.Parse(txtQuantity.Text);
            }
            catch
            {
                valid = false;
                //Seting the outline of the text box to red if its not valid
                txtQuantity.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            try
            {
                price = int.Parse(txtPricePaid.Text);
            }
            catch
            {
                valid = false;
                //Seting the outline of the text box to red if its not valid
                txtPricePaid.BorderBrush = System.Windows.Media.Brushes.Red;
            }



            if (valid == true)
            {
                //Creating a package line to pass to the manager
                PackageLine pl = new PackageLine()
                {
                    PackageId = _package.PackageId,
                    ProductLotId = productLotID,
                    Quantity = quantity,
                    PricePaid = price
                };

                try
                {
                    if (_packageLineManager.CreatePackageLine(pl))
                    {
                        LoadPackageLines();
                    }
                }
                catch
                {
                    MessageBox.Show("There was a problem communicating with the database. Please ensure you entered a valid product lot ID.");
                }
            }
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// Method for handling when the window loads.
        /// gets the latest package lines for the package from the database
        /// runs LoadPackageLines() to refresh the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPackageLines_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPackageLines();
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// Refreshes the list view so that the items source is up to date
        /// </summary>
        private void LoadPackageLines()
        {
            try
            {
                _package.PackageLineList = _packageLineManager.RetrievePackageLinesInPackage(_package.PackageId);
                lvListPackageLines.ItemsSource = _package.PackageLineList;
            }
            catch
            {
                MessageBox.Show("There was a problem getting the package lines from the database.");
            }
        }


    }
}
