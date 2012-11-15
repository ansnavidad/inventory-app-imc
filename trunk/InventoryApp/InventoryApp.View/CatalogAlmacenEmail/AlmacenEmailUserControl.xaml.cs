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
using InventoryApp.ViewModel.CatalogAlmacenEmail;

namespace InventoryApp.View.CatalogAlmacenEmail
{
    /// <summary>
    /// Lógica de interacción para AlmacenEmailUserControl.xaml
    /// </summary>
    public partial class AlmacenEmailUserControl : UserControl
    {
        public AlmacenEmailUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaAlmacenEmail alta = new AltaAlmacenEmail();
            try
            {
                CatalogAlmacenEmailViewModel almacenEmailViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogAlmacenEmailViewModel;
                alta.DataContext = almacenEmailViewModel.CreateAddAlmacenEmailViewModel();
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

        private void dtGridAlmacenEmail_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyAlmacenEmailView dlgModify = new ModifyAlmacenEmailView();
                    try
                    {
                        CatalogAlmacenEmailViewModel almacenEmailViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogAlmacenEmailViewModel;
                        dlgModify.DataContext = almacenEmailViewModel.CreateModifyAlmacenEmailViewModel();
                        dlgModify.ShowDialog();
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
