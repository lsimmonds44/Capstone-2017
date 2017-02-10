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
    /// Interaction logic for frmUpdateEmployee.xaml
    /// </summary>

    public partial class frmUpdateEmployee : Window
    {
        public List<Employee> listOfEmployees { get; set; }
        private Employee _employee;
        private EmployeeManager _employeeManager;
        private List<Employee> employeeList = new List<Employee>();
        private UserManager _userManager = new UserManager();
        public frmUpdateEmployee(EmployeeManager employeeManager, Employee employee)
        {
            InitializeComponent();
            _employeeManager = employeeManager;
            _employee = employee;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2017/10/02
        /// 
        /// Loads Window for Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Editing the record for " + _employee.UserId;

            txtEmployeeID.Text = _employee.EmployeeId.ToString();
            txtUserID.Text = _employee.UserId.ToString();
            txtSalary.Text = _employee.Salary.ToString();
            txtDateOfBirth.Text = _employee.DateOfBirth.ToString();
            chkActive.IsChecked = _employee.Active; 
            

        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2017/10/02
        /// 
        /// Confirms Employee Update changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newEmployeeID = Int32.Parse(txtEmployeeID.Text);
                var newUserID = Int32.Parse(txtUserID.Text);
                var newSalary = Decimal.Parse(txtSalary.Text);
                var newActive = chkActive.IsChecked ?? false;
                var newDob = Convert.ToDateTime(txtDateOfBirth);
                Employee newEmp = new Employee() {
                    EmployeeId = newEmployeeID,
                    UserId = newUserID,
                    Salary = newSalary,
                    Active = newActive,
                    DateOfBirth = newDob
                };
                
                _employeeManager.UpdateEmployee(_employee, newEmp);

                listOfEmployees = _employeeManager.RetrieveEmployeeList();
                MessageBox.Show("Update Completed!");
                this.DialogResult = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Update Not Complete.");
                
            }
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2017/10/02
        /// 
        /// Cancels Employee Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit Update Employee", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }

       
    }
}
