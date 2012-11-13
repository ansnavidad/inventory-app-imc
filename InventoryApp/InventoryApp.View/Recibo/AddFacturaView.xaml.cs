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
using InventoryApp.ViewModel.Recibo;

namespace InventoryApp.View.Recibo
{
    /// <summary>
    /// Lógica de interacción para AddFacturaView.xaml
    /// </summary>
    public partial class AddFacturaView : UserControl
    {
        public AddFacturaView()
        {
            InitializeComponent();
        }

        private void btnFacturaAdd_Click(object sender, RoutedEventArgs e)
        {
            AddFacturaViewModel addFacturaViewModel = this.DataContext as AddFacturaViewModel;
            if (addFacturaViewModel != null)
            {
                DlgAddFacturaArticuloView addFacturaArticuloView = new DlgAddFacturaArticuloView();
                addFacturaArticuloView.DataContext = addFacturaViewModel.CreateAddFacturaArticuloViewModel();
                addFacturaArticuloView.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }
    }
}
