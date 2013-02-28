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
using System.Windows.Shapes;
using InventoryApp.View.Historial;
using InventoryApp.ViewModel.CatalogTipoCotizacion;

namespace InventoryApp.View.CatalogTipoCotizacion
{
    /// <summary>
    /// Lógica de interacción para ModifyTipoCotizacionView.xaml
    /// </summary>
    public partial class ModifyTipoCotizacionView : Window
    {
        public ModifyTipoCotizacionView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtTipoCotizacion);
            this.txtTipoCotizacion.SelectAll();
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ModifyTipoCotizacionViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyTipoCotizacionViewModel ConvertDataContext(object dataSource)
        {
            ModifyTipoCotizacionViewModel viewModel = dataSource as ModifyTipoCotizacionViewModel;
            return viewModel;
        }
    }
}
