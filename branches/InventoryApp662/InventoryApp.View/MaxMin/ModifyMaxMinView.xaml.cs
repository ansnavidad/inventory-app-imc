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
using InventoryApp.ViewModel.MaxMin;
using InventoryApp.View.Historial;

namespace InventoryApp.View.MaxMin
{
    /// <summary>
    /// Lógica de interacción para ModifyMaxMinView.xaml
    /// </summary>
    public partial class ModifyMaxMinView : Window
    {
        public ModifyMaxMinView()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AddArticuloView altaView = new AddArticuloView();
            AddArticulosMaxMin addArticuloViewModel = new AddArticulosMaxMin((ModifyMaxMinViewModel)this.DataContext);
            altaView.DataContext = addArticuloViewModel;
            altaView.ShowDialog();
            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ModifyMaxMinViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyMaxMinViewModel ConvertDataContext(object dataSource)
        {
            ModifyMaxMinViewModel viewModel = dataSource as ModifyMaxMinViewModel;
            return viewModel;
        }
    }
}
