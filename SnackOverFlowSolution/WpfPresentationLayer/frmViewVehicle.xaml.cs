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
    /// Interaction logic for frmViewVehicle.xaml
    /// 
    /// Victor Algarin
    /// Created 2017/02/28  
    /// 
    /// </summary>
    public partial class frmViewVehicle : Window
    {
        public frmViewVehicle()
        {
            InitializeComponent();
        }

        private int vehicleId;

        public frmViewVehicle(int vehicleId)
        {
            InitializeComponent();
            this.vehicleId = vehicleId;
            displayVehicle();
        }

        Vehicle vehicle = new Vehicle();
        IVehicleManager vehMgr = new VehicleManager();

        // This method sends the vehicleId to the vehicle manager class
        // then sets the retreived data as text in the corresponding textboxes on the form  
        private void displayVehicle()
        {
            try
            {
                vehicle = vehMgr.RetreiveVehicleById(vehicleId);
                txtVehicleId.Text = vehicle.VehicleID.ToString();
                txtVIN.Text = vehicle.VIN.ToString();
                txtMake.Text = vehicle.Make.ToString();
                txtModel.Text = vehicle.Model.ToString();
                txtYear.Text = vehicle.Year.ToString();
                txtActive.Text = vehicle.Active.ToString();
                txtColor.Text = vehicle.Color.ToString();
                txtRepairDate.Text = vehicle.LatestRepair.ToString();
                txtType.Text = vehicle.VehicleTypeID.ToString();
                txtLastDriver.Text = vehicle.LastDriver.ToString();
                txtMileage.Text = vehicle.Mileage.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
