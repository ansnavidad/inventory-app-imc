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
using InventoryApp.ViewModel.Entradas;
using InventoryApp.View.Historial;

namespace InventoryApp.View.Entradas
{
    /// <summary>
    /// Lógica de interacción para EntradaValidacionRevisionSoloLectura.xaml
    /// </summary>
    public partial class EntradaValidacionRevisionSoloLectura : Window
    {
        public EntradaValidacionRevisionSoloLectura()
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
            ReadOnlyEntradaPorValidaionViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ReadOnlyEntradaPorValidaionViewModel ConvertDataContext(object dataSource)
        {
            ReadOnlyEntradaPorValidaionViewModel viewModel = dataSource as ReadOnlyEntradaPorValidaionViewModel;
            return viewModel;
        }
    }
}
