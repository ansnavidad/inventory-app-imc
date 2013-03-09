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
using InventoryApp.ViewModel.CatalogBanco;

namespace InventoryApp.View.CatalogBanco
{
    /// <summary>
    /// Lógica de interacción para ModifyBancoView.xaml
    /// </summary>
    public partial class ModifyBancoView : Window
    {
        public ModifyBancoView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNombreBanco);
            this.txtNombreBanco.SelectAll();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ModifyBancoViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyBancoViewModel ConvertDataContext(object dataSource)
        {
            ModifyBancoViewModel viewModel = dataSource as ModifyBancoViewModel;
            return viewModel;
        }
    }
}
