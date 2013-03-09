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
using InventoryApp.ViewModel.Entradas;

namespace InventoryApp.View.Entradas
{
    /// <summary>
    /// Lógica de interacción para EntradaDesinstalacionSoloLectura.xaml
    /// </summary>
    public partial class EntradaDesinstalacionSoloLectura : Window
    {
        public EntradaDesinstalacionSoloLectura()
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
            ReadOnlyEntradaDesistalacionViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ReadOnlyEntradaDesistalacionViewModel ConvertDataContext(object dataSource)
        {
            ReadOnlyEntradaDesistalacionViewModel viewModel = dataSource as ReadOnlyEntradaDesistalacionViewModel;
            return viewModel;
        }
    }
}
