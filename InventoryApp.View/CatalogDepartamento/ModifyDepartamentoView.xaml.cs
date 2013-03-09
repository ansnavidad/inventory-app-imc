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
using InventoryApp.ViewModel.CatalogDepartamento;

namespace InventoryApp.View.CatalogDepartamento
{
    /// <summary>
    /// Lógica de interacción para ModifyDepartamentoView.xaml
    /// </summary>
    public partial class ModifyDepartamentoView : Window
    {
        public ModifyDepartamentoView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNombreDepartamento);
            this.txtNombreDepartamento.SelectAll();
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
            ModifyDepartamentoViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyDepartamentoViewModel ConvertDataContext(object dataSource)
        {
            ModifyDepartamentoViewModel viewModel = dataSource as ModifyDepartamentoViewModel;
            return viewModel;
        }
    }
}
