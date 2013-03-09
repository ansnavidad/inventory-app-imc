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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventoryApp.ViewModel.Salidas;

namespace InventoryApp.View.Salidas
{
    /// <summary>
    /// Lógica de interacción para SalidaRevision.xaml
    /// </summary>
    public partial class SalidaRevision : Window
    {
        public SalidaRevision()
        {
            InitializeComponent();
            comboBoxSolicitante.SelectedIndex = 0;
        }

        private void buttonAgregarItems_Click(object sender, RoutedEventArgs e)
        {
                        
            AddItem it = new AddItem();
            SalidaRevisionViewModel salida = this.DataContext as SalidaRevisionViewModel;
            it.DataContext = salida.CreateCatalogItemViewModel();
            it.ShowDialog();
        }

        
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Estas dos lineas son para que ejecute el Relay Command
            SalidaRevisionViewModel salida = this.DataContext as SalidaRevisionViewModel;
            salida.AttempArticulo();
            MessageBox.Show("La Salida con el folio " + this.textBlockFolio.Text + "\n se ha registrado exitosamente.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Content = null;
            this.Content = new GridMovimientos.GridMovimientos();
            this.DataContext = new ViewModel.GridMovimientos.MovimientosGridViewModel();

            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se ha cancelado la salida.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Content = null;
            this.Content = new GridMovimientos.GridMovimientos();
            this.DataContext = new ViewModel.GridMovimientos.MovimientosGridViewModel();

            this.Close();
        }
    }
}
