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

namespace InventoryApp.View.CatalogArticulo
{
    /// <summary>
    /// Lógica de interacción para ArticuloUserControl.xaml
    /// </summary>
    public partial class ArticuloUserControl : UserControl
    {
        public ArticuloUserControl()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AddArticuloView alta = new AddArticuloView();
            alta.ShowDialog();
        }

        private void dtGridItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    (new ModifyArticuloView()).ShowDialog();
                }
            }
        }
    }
}
