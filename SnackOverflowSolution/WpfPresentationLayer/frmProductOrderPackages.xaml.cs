///<summary>
///Robert Forbes
///2017/02/02
///
///Interaction logic for ProductOrderPackages.xaml
///</summery>
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
    public partial class frmProductOrderPackages : Window
    {

        int _orderId;
        IPackageManager _packageManager;
        IProductOrderManager _orderManager;

        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// Constructor for the window
        /// </summary>
        /// <param name="orderId">The orderID that the packages should relate to</param>
        public frmProductOrderPackages(int orderId)
        {
            _orderId = orderId;
            _packageManager = new PackageManager();
            _orderManager = new ProductOrderManager();
            InitializeComponent();
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// When the window loads this method calls LoadPackages() to update the list of packages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderPackages_Loaded(object sender, RoutedEventArgs e)
        {
            loadPackages();
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// When the add package button is clicked this method calls the createPackage method 
        /// and checks to see if the package was created successfully
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPackage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_packageManager.CreatePackage(_orderId, null) == true)
                {
                    loadPackages();
                }
                else
                {
                    MessageBox.Show("The package could not be added, please try again");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem adding the package to the database, please try again");
            }
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// This method attempts to get the packages from the database and updates the list view to show them.
        /// </summary>
        private void loadPackages()
        {
            try
            {
                lvListPackages.ItemsSource = _packageManager.RetrievePackagesInOrder(_orderId);
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem retrieveing the packages from the database, please try again");
            }
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// Opens a window to add package lines when the list view is double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvListPackages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Package selected = (Package)lvListPackages.SelectedItem;
            if (selected != null)
            {
                frmPackageLineManagementWindow lineManagement = new frmPackageLineManagementWindow(selected);
                lineManagement.ShowDialog();
            }
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/02/16
        /// 
        /// Marks the order as ready for shipment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadyForShipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_orderManager.UpdateProductOrderStatus(_orderId, "Ready For Shipment"))
                {
                    MessageBox.Show("The order status has successfully been marked as ready for shipment");
                }
                else
                {
                    MessageBox.Show("The order status could not be updated");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem communicating with the database, please try again");
            }
        }

    }
}
