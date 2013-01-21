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
using InventoryApp.ViewModel.Recibo;

namespace InventoryApp.View.Recibo
{
    /// <summary>
    /// Lógica de interacción para AddReciboView.xaml
    /// </summary>
    public partial class AddReciboView : UserControl
    {
        public AddReciboView()
        {
            InitializeComponent();
            this.ComboEmp.SelectedIndex = 1;
            this.ComboEmp.SelectedIndex = 0;
            this.ComboSol.SelectedIndex = 1;
            this.ComboSol.SelectedIndex = 0;
        }

        private void btnFacturaAdd_Click(object sender, RoutedEventArgs e)
        {
            DlgAddFacturaView addFactura = new DlgAddFacturaView();
            AddReciboViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateAddFacturaViewModel(); 
            addFactura.ShowDialog();
        }

        private void ConvertObjectSource()
        {

        }

        private AddReciboViewModel ConvertDataContext(object dataSource)
        {
            AddReciboViewModel viewModel = dataSource as AddReciboViewModel;
            return viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DlgAddMovimientoView addMovimientoView = new DlgAddMovimientoView();
            AddReciboViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addMovimientoView.DataContext = viewModel.CreateAddMovimientoViewModel();
            addMovimientoView.ShowDialog();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DlgModifyFacturaView dlgModifyFacturaView = new DlgModifyFacturaView();
            AddReciboViewModel viewModel = this.ConvertDataContext(this.DataContext);
            ModifyFacturaViewModel mfvm= viewModel.CraeteModifyFacturaViewModel();
            if (mfvm != null)
            {
                dlgModifyFacturaView.DataContext = mfvm;
                dlgModifyFacturaView.ShowDialog();
            }
        }

    }
}
