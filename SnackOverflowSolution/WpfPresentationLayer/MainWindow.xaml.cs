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
            _employee = new Employee();
            _employee.EmployeeId = 10001;
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
    }
}
