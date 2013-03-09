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
using InventoryApp.ViewModel.CatalogCliente;

namespace InventoryApp.View.CatalogCliente
{
    /// <summary>
    /// Lógica de interacción para ModifyClienteView.xaml
    /// </summary>
    public partial class ModifyClienteView : Window
    {
        public ModifyClienteView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreCliente);
            this.txtNomreCliente.SelectAll();
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
            ModifyClienteViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyClienteViewModel ConvertDataContext(object dataSource)
        {
            ModifyClienteViewModel viewModel = dataSource as ModifyClienteViewModel;
            return viewModel;
        }
    }
}
