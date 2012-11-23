using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventoryApp.ViewModel.Recibo;

namespace InventoryApp.View.Recibo
{
    /// <summary>
    /// Lógica de interacción para CatalogReciboView.xaml
    /// </summary>
    public partial class CatalogReciboView : UserControl
    {
        public CatalogReciboView()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            DlgAddReciboView dlg = new DlgAddReciboView();
            CatalogReciboViewModel viewModel = this.ConvertDataContext(this.DataContext);
            dlg.DataContext = viewModel.CraeteAddReciboViewModel();
            dlg.ShowDialog();
        }

        private CatalogReciboViewModel ConvertDataContext(object dataSource)
        {
            CatalogReciboViewModel viewModel = dataSource as CatalogReciboViewModel;
            return viewModel;
        }
    }
}
