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
using InventoryApp.ViewModel.CatalogPropiedad;

namespace InventoryApp.View.CatalogPropiedad
{
    /// <summary>
    /// Lógica de interacción para PropiedadUserControl.xaml
    /// </summary>
    public partial class PropiedadUserControl : UserControl
    {
        public PropiedadUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaPropiedad alta = new AltaPropiedad();
            try
            {
                CatalogPropiedadViewModel propiedadViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogPropiedadViewModel;
                alta.DataContext = propiedadViewModel.CreateAddPropiedadViewModel();
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

        private void dtGridPropiedad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyPropiedadView dlgModify = new ModifyPropiedadView();
                    try
                    {
                        CatalogPropiedadViewModel propiedadViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogPropiedadViewModel;
                        dlgModify.DataContext = propiedadViewModel.CreateModifyPropiedadViewModel();
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
