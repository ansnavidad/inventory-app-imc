﻿using System;
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
    /// Lógica de interacción para SalidaRenta.xaml
    /// </summary>
    public partial class SalidaRenta : Window
    {
        public SalidaRenta()
        {
            comboBoxClienteDestino = new ComboBox();
            InitializeComponent();
            comboBoxSolicitante.SelectedIndex = 0;
        }

        private void buttonAgregarItems_Click(object sender, RoutedEventArgs e)
        {
            
            AddItem it = new AddItem();
            SalidaRentaViewModel salida = this.DataContext as SalidaRentaViewModel;
            it.DataContext = salida.CreateCatalogItemViewModel();
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

        private void radioButtonProveedor_Checked(object sender, RoutedEventArgs e)
        {
            if (comboBoxProveedorDestino != null)
            {
                comboBoxProveedorDestino.SelectedIndex = 0;

                
                comboBoxProveedorDestino.IsEnabled = true;
                comboBoxClienteDestino.IsEnabled = false;
                comboBoxClienteDestino.SelectedItem = null;
            }
        }

        private void radioButtonCliente_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxClienteDestino.SelectedIndex = 0;

            
            comboBoxProveedorDestino.IsEnabled = false;
            comboBoxClienteDestino.IsEnabled = true;
            comboBoxProveedorDestino.SelectedItem = null;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Estas dos lineas son para que ejecute el Relay Command
            SalidaRentaViewModel salida = this.DataContext as SalidaRentaViewModel;
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
