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
using InventoryApp.ViewModel.CatalogProyecto;
using InventoryApp.View.Historial;

namespace InventoryApp.View.CatalogProyecto
{
    /// <summary>
    /// Lógica de interacción para ModifyProyectoView.xaml
    /// </summary>
    public partial class ModifyProyectoView : Window
    {
        public ModifyProyectoView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtProyecto);
            this.txtProyecto.SelectAll();
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
            ModifyProyectoViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyProyectoViewModel ConvertDataContext(object dataSource)
        {
            ModifyProyectoViewModel viewModel = dataSource as ModifyProyectoViewModel;
            return viewModel;
        }
    }
}
