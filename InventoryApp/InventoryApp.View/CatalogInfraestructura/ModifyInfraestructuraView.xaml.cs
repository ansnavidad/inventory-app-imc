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
using InventoryApp.ViewModel.CatalogInfraestructura;
using InventoryApp.View.Historial;

namespace InventoryApp.View.CatalogInfraestructura
{
    /// <summary>
    /// Lógica de interacción para ModifyInfraestructuraView.xaml
    /// </summary>
    public partial class ModifyInfraestructuraView : Window
    {
        public ModifyInfraestructuraView()
        {
            InitializeComponent();
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ModifyInfraestructuraViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyInfraestructuraViewModel ConvertDataContext(object dataSource)
        {
            ModifyInfraestructuraViewModel viewModel = dataSource as ModifyInfraestructuraViewModel;
            return viewModel;
        }
    }
}
