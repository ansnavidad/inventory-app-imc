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
    /// Lógica de interacción para SalidaRMASoloLectura.xaml
    /// </summary>
    public partial class SalidaRMASoloLectura : Window
    {
        public SalidaRMASoloLectura()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ReadOnlySalidaRMAViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ReadOnlySalidaRMAViewModel ConvertDataContext(object dataSource)
        {
            ReadOnlySalidaRMAViewModel viewModel = dataSource as ReadOnlySalidaRMAViewModel;
            return viewModel;
        }
    }
}
