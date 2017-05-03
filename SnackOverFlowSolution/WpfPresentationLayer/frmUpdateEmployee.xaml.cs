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
    /// 
    /// <remarks>
    /// Edited by Christian Lopez
    /// 2017/05/03
    /// 
    /// Removed reliance on concrete instances and instead implement interfaces.
    /// </remarks>
    /// </summary>

    public partial class frmUpdateEmployee : Window
    {
        public List<Employee> _listOfEmployees { get; set; }
        private Employee _employee;
        //private EmployeeManager _employeeManager;
        private List<Employee> employeeList = new List<Employee>();
        //private UserManager _userManager = new UserManager();
        private IUserManager _userManager;
        private IEmployeeManager _employeeManager;
        //public frmUpdateEmployee(EmployeeManager employeeManager, Employee employee)
        //{
        //    InitializeComponent();
        //    _employeeManager = employeeManager;
        //    _employee = employee;
        //}

        /// <summary>
        /// Chistian Lopez
        /// 2017/05/03
        /// 
        /// Modified constructor to work with interfaces and proper naming conventions
        /// </summary>
        /// <param name="_employeeManager1"></param>
        /// <param name="userManager"></param>
        /// <param name="_employee"></param>
        public frmUpdateEmployee(IEmployeeManager employeeManager, IUserManager userManager, Employee employee)
        {
            // TODO: Complete member initialization
            this._employeeManager = employeeManager;
            this._employee = employee;
            this._userManager = userManager;
            InitializeComponent();
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2017/10/02
        /// 
        /// Loads Window for Edit
        /// </summary>
        /// <remarks>
        /// Edited by Christian Lopez
        /// 2017/05/03
        /// 
        /// Working with new fields on window
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_employee == null)
            {
                MessageBox.Show("no employee selected");
            }
            else
            {
                User user = null;

                try
                {
                    user = _userManager.RetrieveUser((int)_employee.UserId);
                }
                catch (Exception ex)
                {

                    if (null == ex.InnerException)
                    {
                        MessageBox.Show("Could not find user associated with the employee. " + Environment.NewLine + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show("Could not find user associated with the employee. " + Environment.NewLine + ex.Message
                            + Environment.NewLine + ex.InnerException.Message);
                    }
                }

                if (null == user)
                {
                    this.Title = "Editing the record for " + _employee.UserId;
                }
                else
                {
                    this.Title = "Editing the record for " + user.LastName + ", " + user.FirstName;
                    lblEmpUserId.Content = user.UserId;
                    lblEmpName.Content = user.LastName + ", " + user.FirstName;
                }
                lblEmpid.Content = _employee.EmployeeId;

                //txtEmployeeID.Text = _employee.EmployeeId.ToString();
                //txtUserID.Text = _employee.UserId.ToString();
                //txtSalary.Text = _employee.Salary.ToString();
                //txtDateOfBirth.Text = _employee.DateOfBirth.ToString();
                chkActive.IsChecked = _employee.Active;
            }

        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2017/10/02
        /// 
        /// Confirms Employee Update changes
        /// </summary>
        /// <remarks>
        /// Edited by Christian Lopez
        /// 2017/05/03
        /// 
        /// Update method to work with the data changes; we only allow updating of active status.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var newEmployeeID = Int32.Parse(txtEmployeeID.Text);
                //var newUserID = Int32.Parse(txtUserID.Text);
                //var newSalary = Decimal.Parse(txtSalary.Text);
                var newActive = chkActive.IsChecked ?? false;
                //var newDob = Convert.ToDateTime(txtDateOfBirth);
                //Employee newEmp = new Employee() {
                //    EmployeeId = newEmployeeID,
                //    UserId = newUserID,
                //    Salary = newSalary,
                //    Active = newActive,
                //    DateOfBirth = newDob
                //};

                Employee newEmp = new Employee()
                {
                    EmployeeId = _employee.EmployeeId,
                    UserId = _employee.UserId,
                    DateOfBirth = _employee.DateOfBirth,
                    Salary = _employee.Salary,
                    Active = newActive
                };
                
                _employeeManager.UpdateEmployee(_employee, newEmp);

                _listOfEmployees = _employeeManager.RetrieveEmployeeList();
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
