using System;
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
using InventoryApp.ViewModel.MaxMin;

namespace InventoryApp.View.MaxMin
{
    /// <summary>
    /// Lógica de interacción para MaxMinView.xaml
    /// </summary>
    public partial class MaxMinView : UserControl
    {
        public MaxMinView()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AddMaxMinView alta = new AddMaxMinView();
            try
            {
                MaxMinViewModel maxMinViewModel = new MaxMinViewModel();
                maxMinViewModel = this.DataContext as MaxMinViewModel;
                alta.DataContext = maxMinViewModel.CreateAddMaxMinViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtGridItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyMaxMinView modify = new ModifyMaxMinView();
                    try
                    {
                        MaxMinViewModel maxMinViewModel = new MaxMinViewModel();
                        maxMinViewModel = this.DataContext as MaxMinViewModel;
                        modify.DataContext = maxMinViewModel.CreateModifyMaxMinViewModel();
                        modify.ShowDialog();
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
