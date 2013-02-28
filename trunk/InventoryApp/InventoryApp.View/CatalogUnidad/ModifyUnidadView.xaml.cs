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
using InventoryApp.ViewModel.CatalogUnidad;

namespace InventoryApp.View.CatalogUnidad
{
    /// <summary>
    /// Lógica de interacción para ModifyUnidadView.xaml
    /// </summary>
    public partial class ModifyUnidadView : Window
    {
        public ModifyUnidadView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNombreUnidad);
            this.txtNombreUnidad.SelectAll();
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
            ModifyUnidadViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyUnidadViewModel ConvertDataContext(object dataSource)
        {
            ModifyUnidadViewModel viewModel = dataSource as ModifyUnidadViewModel;
            return viewModel;
        }
    }
}
