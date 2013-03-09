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
using InventoryApp.ViewModel.CatalogRolSystem;

namespace InventoryApp.View.CatalogRolSystem
{
    /// <summary>
    /// Lógica de interacción para RolSystemUserControl.xaml
    /// </summary>
    public partial class RolSystemUserControl : UserControl
    {
        public RolSystemUserControl()
        {
            InitializeComponent();
        }

        private void dtGridRolSystem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ModifyRolSistemaView dlgModify = new ModifyRolSistemaView();
            CatalogRolSystemViewModel viewModel = this.ConvertDataContext(this.DataContext);
            ModifyRolSystemViewModel mfvm = viewModel.CraeteModifyRolSystemViewModel();
            if (mfvm != null)
            {
                dlgModify.DataContext = mfvm;
                dlgModify.ShowDialog();
            }
        }

        private CatalogRolSystemViewModel ConvertDataContext(object dataSource)
        {
            CatalogRolSystemViewModel viewModel = dataSource as CatalogRolSystemViewModel;
            return viewModel;
        }
    }
}
