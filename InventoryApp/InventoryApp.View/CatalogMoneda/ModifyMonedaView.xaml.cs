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
using InventoryApp.ViewModel.CatalogMoneda;

namespace InventoryApp.View.CatalogMoneda
{
    /// <summary>
    /// Lógica de interacción para ModifyMonedaView.xaml
    /// </summary>
    public partial class ModifyMonedaView : Window
    {
        public ModifyMonedaView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNombreMoneda);
            this.txtNombreMoneda.SelectAll();
            this.txtMonedaAbr.SelectAll();
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
            ModifyMonedaViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyMonedaViewModel ConvertDataContext(object dataSource)
        {
            ModifyMonedaViewModel viewModel = dataSource as ModifyMonedaViewModel;
            return viewModel;
        }
    }
}
