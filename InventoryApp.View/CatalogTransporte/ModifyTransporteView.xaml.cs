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
using InventoryApp.ViewModel.CatalogTransporte;

namespace InventoryApp.View
{
    /// <summary>
    /// Lógica de interacción para ModifyTransporteView.xaml
    /// </summary>
    public partial class ModifyTransporteView : Window
    {
        public ModifyTransporteView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreStatus);
            
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
            ModifyTransporteViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyTransporteViewModel ConvertDataContext(object dataSource)
        {
            ModifyTransporteViewModel viewModel = dataSource as ModifyTransporteViewModel;
            return viewModel;
        }
    }
}
