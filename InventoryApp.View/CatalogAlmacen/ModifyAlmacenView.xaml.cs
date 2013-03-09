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
using System.Windows.Shapes;
using InventoryApp.View.CatalogTecnico;
using InventoryApp.ViewModel.CatalogTecnico;
using InventoryApp.ViewModel.CatalogAlmacen;
using InventoryApp.View.Historial;

namespace InventoryApp.View.CatalogAlmacen
{
    /// <summary>
    /// Lógica de interacción para ModifyAlmacenView.xaml
    /// </summary>
    public partial class ModifyAlmacenView : Window
    {
        public ModifyAlmacenView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreAlmacen);
            this.txtNomreAlmacen.SelectAll();
            this.txtContacto.SelectAll();
            this.txtDireccion.SelectAll();
            this.txtTecnico.SelectAll();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AltaTecnico view = new AltaTecnico();
            AddTecnicoViewModel viewModel = new AddTecnicoViewModel(new CatalogTecnicoViewModel(), (ModifyAlmacenViewModel)this.DataContext);
            view.DataContext = viewModel;
            view.ShowDialog();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ModifyAlmacenViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyAlmacenViewModel ConvertDataContext(object dataSource)
        {
            ModifyAlmacenViewModel viewModel = dataSource as ModifyAlmacenViewModel;
            return viewModel;
        }
    }
}
