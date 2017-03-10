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
    public partial class ProductLotSearchView : Window
    {
        ProductLotSearchCriteria criteria;
        public ProductLotSearchView(ProductLotSearchCriteria criteria)
        {
            this.criteria = criteria;
            InitializeComponent();
            chkBoxExpiring.IsChecked = criteria.Expired;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            criteria.Expired = chkBoxExpiring.IsChecked??false;
            this.Close();
        }
    }
}
