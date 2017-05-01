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
using DataObjects;
using LogicLayer;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmVehicleCheckOut.xaml
    /// </summary>
    public partial class frmVehicleCheckOut : Window
    {
        private Vehicle _vehicle = new Vehicle();
        VehicleManager _vehicleManager = new VehicleManager();
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize Vehicle Checkout Window.
        /// Standardized method. 
        /// </summary>
        public frmVehicleCheckOut()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize Vehicle Checkout Window and populates the vehicle view.
        /// Standardized method. 
        /// </summary>
        /// <param name="vehicle"></param>
        public frmVehicleCheckOut(Vehicle vehicle)
        {
            InitializeComponent();
            populateView(vehicle);
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Populate the View of a vehicle.
        /// Standardized method.  
        /// </summary>
        /// <param name="vehicle"></param>
        private void populateView(Vehicle vehicle)
        {
            txtVIN.Text = vehicle.VIN;
            txtMake.Text = vehicle.Make;
            txtModel.Text = vehicle.Model;
            _vehicle = vehicle;
        }


        /// <summary>
        /// Laura Simmonds
        /// Created 2017/03/30
        /// 
        /// Changes _vehicle status to checked out or checked in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_vehicleManager.CheckVehicleOutIn(_vehicle) == true)
                {
                    MessageBox.Show("The vehicle status has been updated.");
                    this.DialogResult = true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error updating the vehicle" + ex);
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Closes the Vehicle Checkout Window.
        /// Standardized method.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
