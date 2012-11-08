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

namespace InventoryApp.View.CatalogMarca
{
    /// <summary>
    /// Lógica de interacción para MarcaUserControl.xaml
    /// </summary>
    public partial class MarcaUserControl : UserControl
    {
        public MarcaUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AddMarca alta = new AddMarca();
            alta.ShowDialog();
        }

        private void dtGridMarca_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    (new ModifyMarcaView()).ShowDialog();
                }
            }
        }
    }
}
