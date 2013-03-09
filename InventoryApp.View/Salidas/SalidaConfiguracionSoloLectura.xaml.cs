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
using InventoryApp.ViewModel.Salidas;
using InventoryApp.View.Historial;

namespace InventoryApp.View.Salidas
{
    /// <summary>
    /// Lógica de interacción para SalidaConfiguracionSoloLectura.xaml
    /// </summary>
    public partial class SalidaConfiguracionSoloLectura : Window
    {
        public SalidaConfiguracionSoloLectura()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ReadOnlySalidaConfiguracionViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ReadOnlySalidaConfiguracionViewModel ConvertDataContext(object dataSource)
        {
            ReadOnlySalidaConfiguracionViewModel viewModel = dataSource as ReadOnlySalidaConfiguracionViewModel;
            return viewModel;
        }
    }
}
