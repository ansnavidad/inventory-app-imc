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
using InventoryApp.ViewModel.Traspasos;
using InventoryApp.View.Historial;

namespace InventoryApp.View.Traspasos
{
    /// <summary>
    /// Lógica de interacción para TraspasoStockSoloLectura.xaml
    /// </summary>
    public partial class TraspasoStockSoloLectura : Window
    {
        public TraspasoStockSoloLectura()
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
            ReadOnlyTraspasoStockViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ReadOnlyTraspasoStockViewModel ConvertDataContext(object dataSource)
        {
            ReadOnlyTraspasoStockViewModel viewModel = dataSource as ReadOnlyTraspasoStockViewModel;
            return viewModel;
        }
    }
}
