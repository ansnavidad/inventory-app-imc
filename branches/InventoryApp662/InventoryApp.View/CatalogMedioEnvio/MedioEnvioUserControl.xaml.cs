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
using InventoryApp.ViewModel.CatalogMedioEnvio;

namespace InventoryApp.View.CatalogMedioEnvio
{
    /// <summary>
    /// Lógica de interacción para MedioEnvioUserControl.xaml
    /// </summary>
    public partial class MedioEnvioUserControl : UserControl
    {
        public MedioEnvioUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarMedioEnvio alta = new AltaModificarMedioEnvio();
            try
            {
                CatalogMedioEnvioViewModel medioEnvioViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogMedioEnvioViewModel;
                alta.DataContext = medioEnvioViewModel.CreateAddMedioEnvioViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridItemStatus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyMedioEnvioView dlgModify = new ModifyMedioEnvioView();
                    try
                    {
                        CatalogMedioEnvioViewModel medioEnvioViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogMedioEnvioViewModel;
                        dlgModify.DataContext = medioEnvioViewModel.CreateModifyMedioEnvioViewModel();
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
