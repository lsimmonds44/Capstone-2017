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
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmCreateOrder.xaml
    /// </summary>
    public partial class frmCreateOrder : Window
    {

        private int _employee_Id;
        private CommercialCustomer _cCustomer;

        public frmCreateOrder()
        {
            InitializeComponent();
        }
        public frmCreateOrder(int employee_Id, CommercialCustomer customer)
        {
            InitializeComponent();
            _employee_Id = employee_Id;
            _cCustomer = customer;
        }


    }
}
