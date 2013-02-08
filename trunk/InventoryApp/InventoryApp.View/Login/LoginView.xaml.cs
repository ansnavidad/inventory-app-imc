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
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtUsuario);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void recuperar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RecoverPassword recuperar = new RecoverPassword();
            recuperar.Owner = this;
            this.Hide();
            recuperar.ShowDialog();
        }

        private void registro_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RegisterUser registro = new RegisterUser();
            registro.Owner = this;
            this.Hide();
            registro.ShowDialog();
        }
    }
}
