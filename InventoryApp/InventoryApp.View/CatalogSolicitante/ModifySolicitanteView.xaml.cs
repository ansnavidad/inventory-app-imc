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

namespace InventoryApp.View.CatalogSolicitante
{
    /// <summary>
    /// Lógica de interacción para ModifySolicitanteView.xaml
    /// </summary>
    public partial class ModifySolicitanteView : Window
    {
        public ModifySolicitanteView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreSolicitante);
            this.txtNomreSolicitante.SelectAll();
            this.txtEmail.SelectAll();
            this.txtValidador.SelectAll();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
