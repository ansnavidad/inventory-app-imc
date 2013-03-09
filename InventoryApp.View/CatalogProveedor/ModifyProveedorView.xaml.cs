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
using InventoryApp.View.CatalogProveedorCuenta;
using InventoryApp.ViewModel.CatalogProveedorCuenta;
using InventoryApp.ViewModel.CatalogProveedor;
using InventoryApp.View.Historial;

namespace InventoryApp.View.CatalogProveedor
{
    /// <summary>
    /// Lógica de interacción para ModifyProveedorView.xaml
    /// </summary>
    public partial class ModifyProveedorView : Window
    {
        public ModifyProveedorView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.textBoxNombre);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNuevaCuentaProveedor_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarProveedorCuenta view = new AltaModificarProveedorCuenta();
            AddProveedorCuentaViewModel viewModel = new AddProveedorCuentaViewModel(new CatalogProveedorCuentaViewModel(), (ModifyProveedorViewModel)this.DataContext);
            view.DataContext = viewModel;
            view.ShowDialog();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ModifyProveedorViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }
        private ModifyProveedorViewModel ConvertDataContext(object dataSource)
        {
            ModifyProveedorViewModel viewModel = dataSource as ModifyProveedorViewModel;
            return viewModel;
        }
    }
}
