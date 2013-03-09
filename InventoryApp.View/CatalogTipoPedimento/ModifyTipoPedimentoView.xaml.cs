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
using InventoryApp.ViewModel.CatalogTipoPedimento;

namespace InventoryApp.View.CatalogTipoPedimento
{
    /// <summary>
    /// Lógica de interacción para ModifyTipoPedimentoView.xaml
    /// </summary>
    public partial class ModifyTipoPedimentoView : Window
    {
        public ModifyTipoPedimentoView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.textBoxSignificado);
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
            ModifyTipoPedimentoViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyTipoPedimentoViewModel ConvertDataContext(object dataSource)
        {
            ModifyTipoPedimentoViewModel viewModel = dataSource as ModifyTipoPedimentoViewModel;
            return viewModel;
        }
    }
}
