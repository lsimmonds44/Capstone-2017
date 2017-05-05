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
    /// Interaction logic for frmCreateRoute.xaml
    /// </summary>
    public partial class frmCreateRoute : Window
    {
        private IVehicleManager _vehicleManager = new VehicleManager();
        private IDriverManager _driverManager = new DriverManager();
        private IDeliveryManager _deliveryManager = new DeliveryManager();
        private IRouteManager _routeManager = new RouteManager();
        private IEmployeeManager _employeeManager = new EmployeeManager();
        private IUserManager _userManager = new UserManager();

        private List<Driver> _drivers;
        private List<DriverComboViewModel> _driversViewModel = new List<DriverComboViewModel>();
        private List<Vehicle> _vehicles;
        private List<Delivery> _unassignedDeliveries;
        private List<Delivery> _proposedDeliveries;
        private DriverComboViewModel _selectedDriver;
        private Vehicle _SelectedVehicle;
        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        public frmCreateRoute()
        {
            try{
                _drivers = _driverManager.RetrieveAllDrivers();
            _vehicles = _vehicleManager.RetrieveAllVehicles();
            _unassignedDeliveries = _deliveryManager.RetrieveDeliveries().Where(d => d.RouteId == null).Select(d => d).ToList();
            _proposedDeliveries = new List<Delivery>();
            foreach(var d in _drivers){
                _driversViewModel.Add(new DriverComboViewModel()
                {
                    driver = d,
                    user = _userManager.RetrieveUser((int)_employeeManager.RetrieveEmployee((int)d.DriverId).UserId)
                });
            }

            
            }catch{
                MessageBox.Show("There was a problem communicating with the database");
            }

            InitializeComponent();
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        private void RefreshLists()
        {
            lvProposedDeliveries.ItemsSource = null;
            lvUnassignedDeliveries.ItemsSource = null;
            lvProposedDeliveries.ItemsSource = _proposedDeliveries;
            lvUnassignedDeliveries.ItemsSource = _unassignedDeliveries;
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        private void btnAddDeliveryToProposed_Click(object sender, RoutedEventArgs e)
        {
            if (lvUnassignedDeliveries.SelectedItem != null)
            {
                _proposedDeliveries.Add((Delivery)lvUnassignedDeliveries.SelectedItem);
                _unassignedDeliveries.Remove((Delivery)lvUnassignedDeliveries.SelectedItem);
                RefreshLists();
            }
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        private void btnRemoveDeliveryFromProposed_Click(object sender, RoutedEventArgs e)
        {
            if (lvProposedDeliveries.SelectedItem != null)
            {
                _unassignedDeliveries.Add((Delivery)lvProposedDeliveries.SelectedItem);
                _proposedDeliveries.Remove((Delivery)lvProposedDeliveries.SelectedItem);
                RefreshLists();
            }
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        private void cboDrivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDriver = (DriverComboViewModel)cboDrivers.SelectedItem;
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        private void cboVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _SelectedVehicle = (Vehicle)cboVehicles.SelectedItem;
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        private void btnCreateRoute_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            bool valid = true;
            if(_selectedDriver == null){
                errorMessage += "\nYou must select a driver";
                valid = false;
            }

            if(_SelectedVehicle == null){
                errorMessage += "\nYou must select a vehicle";
                valid = false;
            }

            if(dpAssignedDate.SelectedDate == null){
                errorMessage += "\nYou must select a date";
                valid = false;
            }

            if(_proposedDeliveries.Count < 1){
                errorMessage += "\nYou must select at least one delivery";
                valid = false;
            }

            if(valid){
                try
                {
                    int RouteID = _routeManager.CreateRouteAndRetrieveRouteId(new Route()
                    {
                        DriverId = _selectedDriver.driver.DriverId,
                        VehicleId = _SelectedVehicle.VehicleID,
                        AssignedDate = dpAssignedDate.SelectedDate.Value
                    });
                    foreach(var d in _proposedDeliveries){
                        _deliveryManager.AssignRouteToDelivery((int)d.DeliveryId, RouteID);
                    }
                    _proposedDeliveries = new List<Delivery>();
                    RefreshLists();
                    MessageBox.Show("Route Successfully Created");
                }
                catch
                {
                    MessageBox.Show("Failed To Create Route");
                }
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        private void frmRoute_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshLists();
            cboDrivers.ItemsSource = _driversViewModel;
            cboVehicles.ItemsSource = _vehicles;
        }

    }

    
}

class DriverComboViewModel
{
    public Driver driver { get; set; }
    public User user { get; set; }

    /// <summary>
    /// Robert Forbes
    /// 2017/05/04
    /// 
    /// overrides the default ToString method
    /// </summary>
    /// <returns>a string representation of the driver</returns>
    public override string ToString()
    {
        return user.FirstName + " " + user.LastName;

    }
}
