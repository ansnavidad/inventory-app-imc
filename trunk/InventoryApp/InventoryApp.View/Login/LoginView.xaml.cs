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


namespace InventoryApp.View.Login
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        DispatcherTimer dTimerUploadProcess;
        Storyboard _ImgSync;

        public DispatcherTimer DTimerUploadProcess
        {
            get { return dTimerUploadProcess; }
            set { dTimerUploadProcess = value; }
        }

        public LoginView()
        {
            InitializeComponent();
            
            FocusManager.SetFocusedElement(this, this.txtUsuario);
            dTimerUploadProcess = new DispatcherTimer();
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
        
        private void Imagen()
        {
            this._ImgSync = (Storyboard)this.FindResource("rotateImg");

            DTimerUploadProcess = new DispatcherTimer();

            if (true)
            {
                Action a = () => this.ShowImgSync();
                this.Dispatcher.BeginInvoke(a);
            }
            else
            {
                Action a = () => this.HideImgSync();
                this.Dispatcher.BeginInvoke(a);
            }
            if (true)
            {
                Action a = () => this.SetImgSyncMsg("Comprobando Credenciales...");
                this.Dispatcher.BeginInvoke(a);
            }

        }

        public void ShowImgSync()
        {
            this.cnvTmpRot.Visibility = Visibility.Visible;
            this.cnvTmpRot2.Visibility = Visibility.Collapsed;
            _ImgSync.Begin(this);
        }

        public void HideImgSync()
        {
            this.cnvTmpRot.Visibility = Visibility.Collapsed;
            this.cnvTmpRot2.Visibility = Visibility.Visible;
        }

        public void SetImgSyncMsg(string msg)
        {
            this.imgSyncLogin.ToolTip = msg;
            this.imgSyncLogin2.ToolTip = msg;
        }
   
    }

}
