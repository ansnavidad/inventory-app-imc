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
using InventoryApp.View.Salidas;
using InventoryApp.ViewModel.GridMovimientos;
using InventoryApp.ViewModel.Salidas;

namespace InventoryApp.View.GridMovimientos
{
    /// <summary>
    /// Lógica de interacción para GridSalidaOffice.xaml
    /// </summary>
    public partial class GridSalidaOffice : UserControl
    {
        public GridSalidaOffice()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaOffice dlg = new SalidaOffice();
            MovimientoGridSalidaOfficeViewModel viewModel = this.DataContext as MovimientoGridSalidaOfficeViewModel;
            dlg.DataContext = new SalidaOfficeViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    SalidaOfficeSoloLectura readOnly = new SalidaOfficeSoloLectura();
                    try
                    {
                        MovimientoGridSalidaOfficeViewModel sololectura = new MovimientoGridSalidaOfficeViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridSalidaOfficeViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlySalidaOfficeViewModel();
                        readOnly.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

        }
    }
}
