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
using InventoryApp.ViewModel.CatalogProgramado;
using InventoryApp.View.Historial;

namespace InventoryApp.View.CatalogProgramado
{
    /// <summary>
    /// Lógica de interacción para ModifyProgramadoView.xaml
    /// </summary>
    public partial class ModifyProgramadoView : Window
    {
        public ModifyProgramadoView()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AddArticuloView altaView = new AddArticuloView();
            AddArticulosProgramado addArticuloViewModel = new AddArticulosProgramado((ModifyProgramadoViewModel)this.DataContext);
            altaView.DataContext = addArticuloViewModel;
            altaView.ShowDialog();

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
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
            ModifyProgramadoViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }
        private ModifyProgramadoViewModel ConvertDataContext(object dataSource)
        {
            ModifyProgramadoViewModel viewModel = dataSource as ModifyProgramadoViewModel;
            return viewModel;
        }
    }
}
