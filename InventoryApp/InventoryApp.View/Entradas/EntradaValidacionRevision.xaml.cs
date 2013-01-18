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
using InventoryApp.ViewModel.Entradas;

namespace InventoryApp.View.Entradas
{
    /// <summary>
    /// Lógica de interacción para EntradaValidacionRevision.xaml
    /// </summary>
    public partial class EntradaValidacionRevision : Window
    {
        public EntradaValidacionRevision()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.textBoxTT);
            comboBoxAlmacenOrigen.SelectedIndex = 1;
            comboBoxAlmacenOrigen.SelectedIndex = 0;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.comboBoxInfraestructura.IsEnabled = false;
            
            AddItem it = new AddItem();
            EntradaPorValidacionViewModel entrada = this.DataContext as EntradaPorValidacionViewModel;
            it.DataContext = entrada.CreateCatalogItemViewModel();
            it.ShowDialog();
        }

       
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Estas dos lineas son para que ejecute el Relay Command
            EntradaPorValidacionViewModel entrada = this.DataContext as EntradaPorValidacionViewModel;
            
            MessageBox.Show("La Entrada con el folio " + this.textBlockFolio.Text + "\n se ha registrado exitosamente.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se ha cancelado la entrada.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Content = null;
            this.Content = new GridMovimientos.GridMovimientos();
            this.DataContext = new ViewModel.GridMovimientos.MovimientosGridViewModel();

            this.Close();
        }


    }
}
