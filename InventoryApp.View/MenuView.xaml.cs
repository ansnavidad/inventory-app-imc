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
using InventoryApp.View.Sync;
using InventoryApp.ViewModel;
using InventoryApp.ViewModel.CargaItems;

namespace InventoryApp.View
{
    /// <summary>
    /// Lógica de interacción para MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (Application.Current.Windows[0] != null)
            {
                DlgUpload ds = new DlgUpload();
                ds.Owner = Application.Current.Windows[0];

                try
                {
                    CargaItemsViewModel viewModel = (ds.Owner.DataContext as MainWindowViewModel).CurrentPageViewModel as CargaItemsViewModel;
                    if (viewModel != null)
                    {
                        viewModel.SetCargaItemsMenuViewModel();
                        ds.DataContext = viewModel;
                        viewModel.start();
                        ds.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    
                    
                }
                
                
            }
        }
    }
}
