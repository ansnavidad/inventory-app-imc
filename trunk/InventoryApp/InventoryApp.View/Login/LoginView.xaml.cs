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
using InventoryApp.ViewModel;
using InventoryApp.ViewModel.Login;
using System.Windows.Threading;
using InventoryApp.DAL;
using System.Windows.Media.Animation;
using InventoryApp.View.Sync;


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
            recuperar.DataContext = this.DataContext;
            this.Hide();
            recuperar.ShowDialog();
        }

        private void registro_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RegisterUser registro = new RegisterUser();
            registro.Owner = this;
            registro.DataContext = this.DataContext;
            this.Hide();
            registro.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void checkBoxOpenMain_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (checkBoxOpenMain.IsEnabled)
            {

                MessageBoxResult result = MessageBox.Show("Bienvenido al sistema de inventarios Elara", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                MainWindow mw = new MainWindow();
                mw.Show();
                MainWindowViewModel viewModel = mw.DataContext as MainWindowViewModel;
                if (viewModel != null)
                {
                    viewModel.SetUser(txtUsuario.Text);
                }
                this.Close();
            }
            
        }

        public bool getcredentrials() {

            return true;
        }

        private void pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
                        
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DlgLogin ds = new  DlgLogin();
            LoginViewModel viewModel = this.DataContext  as LoginViewModel;
            if (viewModel != null)
            {
                viewModel.SetLoginViewModel();
                ds.DataContext = viewModel;
                ds.Owner = Application.Current.Windows[0];
                viewModel.start();
                ds.ShowDialog();
            }
        }
        
   
    }

}
