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
using InventoryApp.View.CatalogSeguridad;
using InventoryApp.ViewModel.CatalogSeguridad;
using InventoryApp.ViewModel.CatalogUsuarios;

namespace InventoryApp.View.CatalogUsuarios
{
    /// <summary>
    /// Lógica de interacción para ModifyUsuarioView.xaml
    /// </summary>
    public partial class ModifyUsuarioView : Window
    {
        public ModifyUsuarioView()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AltaSeguridad altaView = new AltaSeguridad();
                AddSeguridadViewModel addArticuloViewModel = new AddSeguridadViewModel((ModifyUsuarioViewModel)this.DataContext);
                altaView.DataContext = addArticuloViewModel;
                altaView.ShowDialog();
            }
            catch (Exception ex)
            {
                
                ;
            }
            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
