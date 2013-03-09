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
using InventoryApp.ViewModel.CatalogEmpresa;

namespace InventoryApp.View.CatalogEmpresa
{
    /// <summary>
    /// Lógica de interacción para ModifyEmpresaView.xaml
    /// </summary>
    public partial class ModifyEmpresaView : Window
    {
        public ModifyEmpresaView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreEmpesa);
            this.txtNomreEmpesa.SelectAll();
            this.txtDireccion.SelectAll();
            this.txtRazonSocial.SelectAll();
            this.txtRfc.SelectAll();
        }

        private void btnMoificar_Click(object sender, RoutedEventArgs e)
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
            ModifyEmpresaViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyEmpresaViewModel ConvertDataContext(object dataSource)
        {
            ModifyEmpresaViewModel viewModel = dataSource as ModifyEmpresaViewModel;
            return viewModel;
        }
    }
}
