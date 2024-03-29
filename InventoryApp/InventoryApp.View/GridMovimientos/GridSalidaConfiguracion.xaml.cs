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
    /// Lógica de interacción para GridSalidaConfiguracion.xaml
    /// </summary>
    public partial class GridSalidaConfiguracion : UserControl
    {
        public GridSalidaConfiguracion()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaConfiguracion dlg = new SalidaConfiguracion();
            MovimientoGridSalidaConfiguracionViewModel viewModel = this.DataContext as MovimientoGridSalidaConfiguracionViewModel;
            dlg.DataContext = new SalidaConfiguracionViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    SalidaConfiguracionSoloLectura readOnly = new SalidaConfiguracionSoloLectura();
                    try
                    {
                        MovimientoGridSalidaConfiguracionViewModel sololectura = new MovimientoGridSalidaConfiguracionViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridSalidaConfiguracionViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlySalidaConfiguracionViewModel();
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
