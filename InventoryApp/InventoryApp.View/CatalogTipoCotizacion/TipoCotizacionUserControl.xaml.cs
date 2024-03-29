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
using InventoryApp.ViewModel.CatalogTipoCotizacion;

namespace InventoryApp.View.CatalogTipoCotizacion
{
    /// <summary>
    /// Lógica de interacción para TipoCotizacionUserControl.xaml
    /// </summary>
    public partial class TipoCotizacionUserControl : UserControl
    {
        public TipoCotizacionUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaTipoCotizacion alta = new AltaTipoCotizacion();
            try
            {
                CatalogTipoCotizacionViewModel solicitanteViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTipoCotizacionViewModel;
                alta.DataContext = solicitanteViewModel.CreateAddTipoCotizacionViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridCotizacion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyTipoCotizacionView dlgModify = new ModifyTipoCotizacionView();
                    try
                    {
                        CatalogTipoCotizacionViewModel tipoCotizacionViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTipoCotizacionViewModel;
                        dlgModify.DataContext = tipoCotizacionViewModel.CreateModifyTipoCotizacionViewModel();
                        dlgModify.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
