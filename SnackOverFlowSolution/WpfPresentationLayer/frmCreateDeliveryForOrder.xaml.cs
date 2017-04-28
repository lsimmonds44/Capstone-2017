using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Robert Forbes
    /// Created: 2017/03/09
    /// 
    /// Interaction logic for frmCreateDeliveryForOrder.xaml
    /// </summary>
    public partial class frmCreateDeliveryForOrder : Window
    {
        IPackageManager _packageManager;

        IDeliveryManager _deliveryManager;
        List<Package> _allPackages;
        List<Package> _unassignedPackages;
        List<Package> _proposedPackages;
        int _orderId;

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/09
        /// 
        /// Creates the window using the passed orderId
        /// </summary>
        /// <param name="orderId"></param>
        public frmCreateDeliveryForOrder(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            _packageManager = new PackageManager();
            _deliveryManager = new DeliveryManager();
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/09
        /// 
        /// Moves a package from the unassigned list to the proposed list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPackageToProposed_Click(object sender, RoutedEventArgs e)
        {
            if(lvUnassignedPackages.SelectedItem != null){
                _proposedPackages.Add((Package)lvUnassignedPackages.SelectedItem);
                _unassignedPackages.Remove((Package)lvUnassignedPackages.SelectedItem);
                RefreshListViews();
            }
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/09
        /// 
        /// Moves a package from the proposed list to the unassigned list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemovePackageFromProposed_Click(object sender, RoutedEventArgs e)
        {
            if(lvProposedPackages.SelectedItem != null){
                _unassignedPackages.Add((Package)lvProposedPackages.SelectedItem);
                _proposedPackages.Remove((Package)lvProposedPackages.SelectedItem);
                RefreshListViews();
            }
        }

        /// <summary>
        /// Robert Forbes
        /// Created 2017/03/09
        /// 
        /// Refreshes both of the list views so they show the what is currently held in the proposed list and unassigned list
        /// </summary>
        private void RefreshListViews()
        {
            lvProposedPackages.ItemsSource = null;
            lvUnassignedPackages.ItemsSource = null;
            lvProposedPackages.ItemsSource = _proposedPackages;
            lvUnassignedPackages.ItemsSource = _unassignedPackages;
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/09
        /// 
        /// Creates a new delivery in the database and assignes all packages in the proposed list to the new delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateDelivery_Click(object sender, RoutedEventArgs e)
        {
            if(_proposedPackages.Count > 0){
                if(dpDeliveryDate.SelectedDate != null){
                    try
                    {
                        Delivery delivery = new Delivery()
                        {
                            RouteId = null,
                            DeliveryDate = dpDeliveryDate.SelectedDate,
                            Verification = null,
                            StatusId = "Ready For Assignment",
                            DeliveryTypeId = "Drop off",
                            OrderId = _orderId
                        };
                        int deliveryId = _deliveryManager.CreateDeliveryAndRetrieveDeliveryId(delivery);
                        List<Package> toBeRemovedFromProposed = new List<Package>();
                        foreach(Package p in _proposedPackages){
                            if(!_packageManager.UpdatePackageDelivery(p.PackageId, deliveryId)){
                                MessageBox.Show("There was a problem assigning package: " + p.PackageId + " to the delivery");
                            }
                            else
                            {
                                toBeRemovedFromProposed.Add(p);
                            }
                        }

                        foreach (Package p in toBeRemovedFromProposed)
                        {
                            _proposedPackages.Remove(p);
                        }
                        LoadPackages();
                        RefreshListViews();


                    }catch{
                        MessageBox.Show("There was a problem communicating with the database");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a delivery date");
                }
            }
            else
            {
                MessageBox.Show("You cannot create an empty delivery");
            }
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/09
        /// 
        /// runs the LoadPackages method when the window is loaded to update the list views
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCreateDeliveryFromOrder_Loaded(object sender, RoutedEventArgs e)
        {
            _proposedPackages = new List<Package>();
            LoadPackages();
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/09
        /// 
        /// Refreshes the list views with the current unassigned packages from the database
        /// </summary>
        private void LoadPackages()
        {
            try
            {
                //Retrieving all packages for the order
                _allPackages = _packageManager.RetrievePackagesInOrder(_orderId);

                //Reducing the packages down to only packages that have no delivery assigned
                _unassignedPackages = _allPackages.Where(p => (p.DeliveryId.Equals(null))).Select(p => p).OrderBy(p => p.PackageId).ToList();
                RefreshListViews();
            }
            catch
            {
                MessageBox.Show("There was a problem communicating with the database");
            }
        }
    } // End of class
} // End of namespace

