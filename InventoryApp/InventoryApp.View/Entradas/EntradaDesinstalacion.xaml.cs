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
    /// Lógica de interacción para EntradaDevolucion.xaml
    /// </summary>
    public partial class EntradaDesinstalacion : UserControl
    {
        public EntradaDesinstalacion()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtRecibe);
            comboCliente.SelectedIndex = 0;
            comboBoxAlmacenOrigen.SelectedIndex = 0; //EL ORIGEN ES EN REALIDAD EL DESTINO
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddItem it = new AddItem();
            EntradaDesinstalacionViewModel entrada = this.DataContext as EntradaDesinstalacionViewModel;
            it.DataContext = entrada.CreateCatalogItemViewModel();
            it.ShowDialog();
        }

        private void radioButtonAlmacén_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxAlmacenDestino.SelectedIndex = 0;

            comboBoxAlmacenDestino.IsEnabled = true;
            comboBoxProveedorDestino.IsEnabled = false;
            comboBoxClienteDestino.IsEnabled = false;
            comboBoxProveedorDestino.SelectedItem = null;
            comboBoxClienteDestino.SelectedItem = null;
        }

        private void radioButtonCliente_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxClienteDestino.SelectedIndex = 0;

            comboBoxAlmacenDestino.IsEnabled = false;
            comboBoxProveedorDestino.IsEnabled = false;
            comboBoxClienteDestino.IsEnabled = true;
            comboBoxAlmacenDestino.SelectedItem = null;
            comboBoxProveedorDestino.SelectedItem = null;
        }

        private void radioButtonProveedor_Checked(object sender, RoutedEventArgs e)
        {
            if (comboBoxProveedorDestino != null)
            {
                comboBoxProveedorDestino.SelectedIndex = 0;

                comboBoxAlmacenDestino.IsEnabled = false;
                comboBoxProveedorDestino.IsEnabled = true;
                comboBoxClienteDestino.IsEnabled = false;
                comboBoxAlmacenDestino.SelectedItem = null;
                comboBoxClienteDestino.SelectedItem = null;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("La Entrada con el folio " + this.textBlockFolio.Text + "\n se ha registrado exitosamente.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Content = null;
            this.Content = new EntradaDesinstalacion();
            this.DataContext = new EntradaDesinstalacionViewModel();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se ha cancelado la entrada.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Content = null;
            this.Content = new EntradaDesinstalacion();
            this.DataContext = new EntradaDesinstalacionViewModel();
        }
    }
}
