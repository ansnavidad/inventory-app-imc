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
using System.Windows.Shapes;
using InventoryApp.ViewModel.Salidas;
using InventoryApp.View.Historial;

namespace InventoryApp.View.Salidas
{
    /// <summary>
    /// Lógica de interacción para SalidaCorrectivoSoloLectura.xaml
    /// </summary>
    public partial class SalidaCorrectivoSoloLectura : Window
    {
        public SalidaCorrectivoSoloLectura()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ReadOnlySalidaCorrectivoViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ReadOnlySalidaCorrectivoViewModel ConvertDataContext(object dataSource)
        {
            ReadOnlySalidaCorrectivoViewModel viewModel = dataSource as ReadOnlySalidaCorrectivoViewModel;
            return viewModel;
        }
    }
}
