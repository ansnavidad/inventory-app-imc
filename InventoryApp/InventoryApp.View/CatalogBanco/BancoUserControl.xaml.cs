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
using InventoryApp.ViewModel.CatalogBanco;

namespace InventoryApp.View.CatalogBanco
{
    /// <summary>
    /// Lógica de interacción para BancoUserControl.xaml
    /// </summary>
    public partial class BancoUserControl : UserControl
    {
        public BancoUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AddBancoView add = new AddBancoView();
            try
            {
                CatalogBancoViewModel bancoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogBancoViewModel;
                add.DataContext = bancoViewModel.CreateAddBancoViewModel();
                add.ShowDialog();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void dtGridBanco_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    //(new ModifyBancoView()).ShowDialog();
                    ModifyBancoView dlgModify = new ModifyBancoView();
                    try
                    {
                        CatalogBancoViewModel bancoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogBancoViewModel;
                        dlgModify.DataContext = bancoViewModel.CreateBancoViewModel();
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
