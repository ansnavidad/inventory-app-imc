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
using InventoryApp.View.Historial;
using InventoryApp.ViewModel.CatalogMedioEnvio;

namespace InventoryApp.View.CatalogMedioEnvio
{
    /// <summary>
    /// Lógica de interacción para ModifyMedioEnvioView.xaml
    /// </summary>
    public partial class ModifyMedioEnvioView : Window
    {
        public ModifyMedioEnvioView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNombreMedioEnvio);
            this.txtNombreMedioEnvio.SelectAll();
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
            ModifyMedioEnvioViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyMedioEnvioViewModel ConvertDataContext(object dataSource)
        {
            ModifyMedioEnvioViewModel viewModel = dataSource as ModifyMedioEnvioViewModel;
            return viewModel;
        }
    }
}
