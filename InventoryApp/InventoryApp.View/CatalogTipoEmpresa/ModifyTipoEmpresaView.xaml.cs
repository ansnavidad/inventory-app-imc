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
using InventoryApp.ViewModel.CatalogTipoEmpresa;

namespace InventoryApp.View
{
    /// <summary>
    /// Lógica de interacción para ModifyTipoEmpresaView.xaml
    /// </summary>
    public partial class ModifyTipoEmpresaView : Window
    {
        public ModifyTipoEmpresaView()
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
            ModifyTipoEmpresaViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyTipoEmpresaViewModel ConvertDataContext(object dataSource)
        {
            ModifyTipoEmpresaViewModel viewModel = dataSource as ModifyTipoEmpresaViewModel;
            return viewModel;
        }
    }
}
