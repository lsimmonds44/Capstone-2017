﻿using DataObjects;
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
    /// Interaction logic for frmCheckSupplierStatus.xaml
    /// </summary>
    public partial class frmCheckSupplierStatus : Window
    {
        User _user;
        private AgreementManager _agreementManager;
        private UserManager _userManager;
        private SupplierManager _supplierManager;
        private ProductManager _productManager;

        /// <summary>
        /// Bobby Thorne
        /// 3/10/2017
        /// 
        /// Initailized the checkSupplierStatus window
        /// More work is needed to add an ability to apply after
        /// this window if they choose to
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userManager"></param>
        /// <param name="supplierManager"></param>
        /// <param name="productManager"></param>
        /// <param name="agreementManager"></param>
        public frmCheckSupplierStatus(User user, IUserManager userManager, ISupplierManager supplierManager, IProductManager productManager, IAgreementManager agreementManager)
        {
            InitializeComponent();
            _user = user;
            _userManager =(UserManager) userManager;
            _supplierManager = (SupplierManager)supplierManager;
            _productManager = (ProductManager)productManager;
            _agreementManager = (AgreementManager)agreementManager;
        }

        public frmCheckSupplierStatus()
        {
            InitializeComponent();
            
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            //frmAddSupplier addSupplier = new frmAddSupplier(_user,_userManager,_supplierManager,_productManager,_agreementManager);
            //addSupplier.Show();
            MessageBox.Show("When users can apply as suppliers add it to this");
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
