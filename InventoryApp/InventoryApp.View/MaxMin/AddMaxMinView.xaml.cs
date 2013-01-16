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
using InventoryApp.ViewModel.MaxMin;

namespace InventoryApp.View.MaxMin
{
    /// <summary>
    /// Lógica de interacción para AddMaxMinView.xaml
    /// </summary>
    public partial class AddMaxMinView : Window
    {
        public AddMaxMinView()
        {
            InitializeComponent();
        }

        private void btnAgregarArticulo_Click(object sender, RoutedEventArgs e)
        {
            AddArticuloView altaView = new AddArticuloView();
            AddArticulosMaxMin addArticuloViewModel = new AddArticulosMaxMin((AddMaxMinViewModel)this.DataContext);
            altaView.DataContext = addArticuloViewModel;
            altaView.ShowDialog();
            
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
