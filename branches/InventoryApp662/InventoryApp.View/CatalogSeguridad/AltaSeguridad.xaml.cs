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
using InventoryApp.ViewModel.CatalogSeguridad;
using InventoryApp.View.Historial;

namespace InventoryApp.View.CatalogSeguridad
{
    /// <summary>
    /// Lógica de interacción para AltaSeguridad.xaml
    /// </summary>
    public partial class AltaSeguridad : Window
    {
        public AltaSeguridad()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreRol);           
           
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
            try
            {
                HistorialView addFactura = new HistorialView();
                ModifySeguridadViewModel viewModel = this.ConvertDataContext(this.DataContext);
                addFactura.DataContext = viewModel.CreateHistorialViewModel();
                addFactura.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Este es un registro nuevo que no cuenta con Historial.");
            }
        }

        private ModifySeguridadViewModel ConvertDataContext(object dataSource)
        {
            ModifySeguridadViewModel viewModel = dataSource as ModifySeguridadViewModel;
            return viewModel;
        }
    }
}
