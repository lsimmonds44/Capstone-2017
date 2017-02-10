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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeManager _employeeManager = new EmployeeManager();


        Employee _employee = null;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateCommercialCustomerWindow cCCW = new CreateCommercialCustomerWindow(_employee.EmployeeId);
            cCCW.ShowDialog();
        }

/// <summary>
/// Ariel Sigo
/// Created 2017/10/02
/// 
/// Button that leads to update employee form
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void Button_Click_Update_Employee(object sender, RoutedEventArgs e)
        {
            frmUpdateEmployee fUE = new frmUpdateEmployee(_employeeManager, _employee);
            fUE.ShowDialog();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hideTabs();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (_employee == null)
            {
                _employee = _employeeManager.RetrieveEmployeeByUserName("");
                showTabs();
                btnLogin.Content = "Logout"; 
            }
            else
            {
                _employee = null;
                btnLogin.Content = "Login";
                hideTabs();
            }
           

        }

        private void showTabs()
        {
            tabSetMain.Visibility = Visibility.Visible;
            tabCommercialCustomer.Visibility = Visibility.Visible;
            tabEmployee.Visibility = Visibility.Visible;
            tabUser.Visibility = Visibility.Visible;
            
        }

        private void hideTabs()
        {
            tabSetMain.Visibility = Visibility.Hidden;
            tabCommercialCustomer.Visibility = Visibility.Collapsed;
            tabEmployee.Visibility = Visibility.Collapsed;
            tabUser.Visibility = Visibility.Collapsed;
        }

        


    } // end of class
} // end of namespace 