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
using InventoryApp.ViewModel.Traspasos;

namespace InventoryApp.View.Traspasos
{
    /// <summary>
    /// Lógica de interacción para TraspasoStock.xaml
    /// </summary>
    public partial class TraspasoStock : Window
    {
        public TraspasoStock()
        {
            InitializeComponent();
            comboBoxAlmacenDestino.SelectedIndex = 0;
            comboBoxAlmacenDestino.SelectedIndex = 1;
            comboBoxSolicitante.SelectedIndex = 0;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.comboBoxAlmacenOrigen.IsEnabled = false;            
            AddItem it = new AddItem();
            TraspasoStockViewModel entrada = this.DataContext as TraspasoStockViewModel;
            it.DataContext = entrada.CreateCatalogItemViewModel();
            it.ShowDialog();
        }        

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Estas dos lineas son para que ejecute el Relay Command
            TraspasoStockViewModel traspaso = this.DataContext as TraspasoStockViewModel;
            traspaso.AttempArticulo();   
            MessageBox.Show("El traspaso con el folio " + this.textBlockFolio.Text + "\n se ha registrado exitosamente.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Content = null;
            this.Content = new GridMovimientos.GridMovimientos();
            this.DataContext = new ViewModel.GridMovimientos.MovimientosGridViewModel();

            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se ha cancelado el traspaso.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Content = null;
            this.Content = new GridMovimientos.GridMovimientos();
            this.DataContext = new ViewModel.GridMovimientos.MovimientosGridViewModel();

            this.Close();
        }

        private void comboBoxAlmacenOrigen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
