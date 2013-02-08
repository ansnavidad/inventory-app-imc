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

namespace InventoryApp.View.Login
{
    /// <summary>
    /// Lógica de interacción para RecoverPassword.xaml
    /// </summary>
    public partial class RecoverPassword : Window
    {
        public RecoverPassword()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtUsuario);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Owner.ShowDialog();
        }
    }
}
