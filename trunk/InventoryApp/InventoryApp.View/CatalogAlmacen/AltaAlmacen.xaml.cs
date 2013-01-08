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

namespace InventoryApp.View.CatalogAlmacen
{
    /// <summary>
    /// Lógica de interacción para AltaAlmacen.xaml
    /// </summary>
    public partial class AltaAlmacen : Window
    {
        public AltaAlmacen()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreAlmacen);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AltaTecnico view = new AltaTecnico();
            AddTecnicoViewModel viewModel = new AddTecnicoViewModel(new CatalogTecnicoViewModel(), this.txtUnid.Text.ToString(), (AddAlmacenViewModel)this.DataContext);
            view.DataContext = viewModel;
            view.ShowDialog();
        }
    }
}
