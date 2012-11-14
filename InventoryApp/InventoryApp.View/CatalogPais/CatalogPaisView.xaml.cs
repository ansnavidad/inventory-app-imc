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
using InventoryApp.View.CatalogPais;

namespace InventoryApp.View.CatalogPais
{
    /// <summary>
    /// Lógica de interacción para CatalogPaisView.xaml
    /// </summary>
    public partial class CatalogPaisView : Window
    {
        public CatalogPaisView()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarPais alta = new AltaModificarPais();
            alta.ShowDialog();
        }

        private void dtGridItemStatus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    (new ModifyPaisView()).ShowDialog();
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}