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
    /// Lógica de interacción para AddMovimientoView.xaml
    /// </summary>
    public partial class AddMovimientoView : UserControl
    {
        public AddMovimientoView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DlgAddMovimientoDetalleView view = new DlgAddMovimientoDetalleView();
            AddMovimientoViewModel viewModel = this.ConvertDataContext(this.DataContext);
            view.DataContext = viewModel.CreateAddMovimientoDetalleViewModel();
            view.ShowDialog();
        }

        private AddMovimientoViewModel ConvertDataContext(object dataSource)
        {
            AddMovimientoViewModel viewModel = dataSource as AddMovimientoViewModel;
            return viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DlgMovimientoSelectAritucloView SelectArticulo = new DlgMovimientoSelectAritucloView();
                AddMovimientoViewModel viewModel = this.ConvertDataContext(this.DataContext);
                SelectArticulo.DataContext = viewModel.CreateMovimientoSelectArticuloViewModel(e.AddedItems[0]);
                SelectArticulo.ShowDialog();
                BindingExpression be = this.cmbFactura.GetBindingExpression(ComboBox.SelectedItemProperty);
                be.UpdateSource();
            }
            catch (Exception ex)
            {                
                ;
            }
        }
    }
}
