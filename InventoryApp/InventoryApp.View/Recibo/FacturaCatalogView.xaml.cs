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
    /// Lógica de interacción para AddReciboView.xaml
    /// </summary>
    public partial class FacturaCatalogView : UserControl
    {
        public FacturaCatalogView()
        {
            InitializeComponent();
        }

        private void btnFacturaAdd_Click_1(object sender, RoutedEventArgs e)
        {
            DlgAddFacturaView addFactura = new DlgAddFacturaView();
            FacturaCatalogViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateAddFacturaViewModel();
            addFactura.ShowDialog();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DlgModifyFacturaView dlgModifyFacturaView = new DlgModifyFacturaView();
            FacturaCatalogViewModel viewModel = this.ConvertDataContext(this.DataContext);
            ModifyFacturaViewModel mfvm = viewModel.CraeteModifyFacturaViewModel();
            if (mfvm != null)
            {
                dlgModifyFacturaView.DataContext = mfvm;
                dlgModifyFacturaView.ShowDialog();
            }
        }

        private FacturaCatalogViewModel ConvertDataContext(object dataSource)
        {
            FacturaCatalogViewModel viewModel = dataSource as FacturaCatalogViewModel;
            return viewModel;
        }
    }
}
