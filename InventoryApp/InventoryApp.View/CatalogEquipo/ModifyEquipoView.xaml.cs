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
using InventoryApp.ViewModel.CatalogEquipo;

namespace InventoryApp.View.CatalogEquipo
{
    /// <summary>
    /// Lógica de interacción para ModifyEquipoView.xaml
    /// </summary>
    public partial class ModifyEquipoView : Window
    {
        public ModifyEquipoView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreStatus);
            this.txtNomreStatus.SelectAll();
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
            HistorialView addFactura = new HistorialView();
            ModifyEquipoViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyEquipoViewModel ConvertDataContext(object dataSource)
        {
            ModifyEquipoViewModel viewModel = dataSource as ModifyEquipoViewModel;
            return viewModel;
        }
    }
}
