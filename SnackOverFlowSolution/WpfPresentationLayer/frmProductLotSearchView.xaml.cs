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
    /// Interaction logic for ProductLotSearchView.xaml
    /// </summary>
    public partial class frmProductLotSearchView : Window
    {
        ProductLotSearchCriteria _criteria;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize Product Lot Search View Window.
        /// Standardized method. 
        /// </summary>
        /// <param name="criteria"></param>
        public frmProductLotSearchView(ProductLotSearchCriteria criteria)
        {
            this._criteria = criteria;
            InitializeComponent();
            chkBoxExpiring.IsChecked = criteria.Expired;
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Invokes search of Criteria and Closes Window.
        /// Standardized method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            _criteria.Expired = chkBoxExpiring.IsChecked??false;
            this.Close();
        }
    }
}
