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
using System.Diagnostics;
using System.Configuration;
using InventoryApp.ViewModel.CargaItems;
using InventoryApp.View.Sync;
using System.Threading;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.View.CargaItems
{
    /// <summary>
    /// Lógica de interacción para CargaItemsView.xaml
    /// </summary>
    public partial class CargaItemsView : UserControl
    {
        public CargaItemsView()
        {
            InitializeComponent();
        }

        private void cbProceso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {

                if (cbProceso.SelectedItem != null)
                {
                    if (!CargaItemsViewModel.IsRunning)
                    {
                        DlgUpload ds = new DlgUpload();
                        CargaItemsViewModel viewModel = (this.DataContext as ObjectDataProvider).Data as CargaItemsViewModel;
                        if (viewModel != null)
                        {
                            viewModel.SetCargaItemsViewModel();
                            ds.DataContext = viewModel;
                            ds.Owner = Application.Current.Windows[0];
                            viewModel.start();
                            ds.ShowDialog();   
                        }
                                                
                    }

                }
            }
            
        }


    }
}
