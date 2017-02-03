using DataObjects;
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
    }
}
