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


        private int _vehicleId;
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Standaridized method.
        /// Initialize the View Vehicle Window.
        /// </summary>
        /// <param name="_vehicleId"></param>
        public frmViewVehicle(int vehicleId)
        {
            InitializeComponent();
            this._vehicleId = vehicleId;
            displayVehicle();
        }

        Vehicle _vehicle = new Vehicle();
        IVehicleManager _vehMgr = new VehicleManager();
        
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Standaridized method.
        /// This method sends the _vehicleId to the _vehicle manager class
        /// then sets the retreived data as text in the corresponding textboxes on the form  
        /// </summary>
        private void displayVehicle()
        {
            try
            {
                _vehicle = _vehMgr.RetreiveVehicleById(_vehicleId);
                txtVehicleId.Text = _vehicle.VehicleID.ToString();
                txtVIN.Text = _vehicle.VIN.ToString();
                txtMake.Text = _vehicle.Make.ToString();
                txtModel.Text = _vehicle.Model.ToString();
                txtYear.Text = _vehicle.Year.ToString();
                txtActive.Text = _vehicle.Active.ToString();
                txtColor.Text = _vehicle.Color.ToString();
                txtRepairDate.Text = _vehicle.LatestRepair.ToString();
                txtType.Text = _vehicle.VehicleTypeID.ToString();
                txtLastDriver.Text = _vehicle.LastDriver.ToString();
                txtMileage.Text = _vehicle.Mileage.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Close Method that closes the View Vehicle Window.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
