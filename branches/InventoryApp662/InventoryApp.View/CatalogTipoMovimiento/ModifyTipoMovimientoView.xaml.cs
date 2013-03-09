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

namespace InventoryApp.View.CatalogTipoMovimiento
{
    /// <summary>
    /// Lógica de interacción para ModifyTipoMovimientoView.xaml
    /// </summary>
    public partial class ModifyTipoMovimientoView : Window
    {
        public ModifyTipoMovimientoView()
        {
            InitializeComponent();
             FocusManager.SetFocusedElement(this, this.txtTipoMovimiento);
             this.txtTipoMovimiento.SelectAll();
             this.txtSignoMovimiento.SelectAll();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
   }
}
