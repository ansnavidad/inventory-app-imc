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
using System.Globalization;
using System.Threading;
using InventoryApp.ViewModel;
using System.Windows.Threading;
using InventoryApp.ViewModel.Sync;
using InventoryApp.View.Sync;
using InventoryApp.DAL;
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Web;
using System.Configuration;
using InventoryApp.ViewModel.Version;
using InventoryApp.ViewModel;
using System.Diagnostics;
using InventoryApp.ViewModel.Login;
using InventoryApp.View.Login;

namespace InventoryApp.View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer dTimerUploadProcess;
        SyncDataMapper sync = new SyncDataMapper();
        Storyboard _ImgSync;

        public DispatcherTimer DTimerUploadProcess
        {
            get { return dTimerUploadProcess; }
            set { dTimerUploadProcess = value; }
        }

         public MainWindow()
        {
            InitializeComponent();
            
            int SyncTime = Int32.Parse(ConfigurationManager.AppSettings["SyncTime"].ToString());

            //System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();
            //string nomPC = user.Name;
            //nomUser.Content = nomPC;
            
            this._ImgSync = (Storyboard)this.FindResource("rotateImg");

            DTimerUploadProcess = new DispatcherTimer();
            DTimerUploadProcess.Tick += new EventHandler(DTimerUploadProcess_Tick);
            DTimerUploadProcess.Interval = new TimeSpan(0, 0, SyncTime);
            DTimerUploadProcess.Start();
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

        public void ShowImgVersion()
        {
        }

        public void HideImgVersion()
        {
        }

        public void SetImgSyncMsg(string msg)
        {
            this.imgSyncFiles.ToolTip = msg;
            this.imgSyncFiles2.ToolTip = msg;
        }

        private void actualiceAPP()
        {
            try
            {
                string instalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"];
                string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreProcesoActualizacion"];

                if (!instalacion.EndsWith("\\"))
                    instalacion += "\\";

                System.Diagnostics.Process.Start(proceso);
                this.Close();
                Application.Current.Shutdown();
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex2)
            {
                System.Windows.MessageBox.Show("Error tratando de iniciar el proceso automático de actualización de la aplicación - " + ex2.Message);
            }
        }

        void DTimerUploadProcess_Tick(object sender, EventArgs e)
        {
            //Condicionar catsync
            if (!UploadProcessViewModel.IsRunning)
            {
                if (VersionViewModel.NewVersion())
                {
                    if (System.Windows.MessageBox.Show("Se detectó una nueva actualización de la aplicación, desea actualizar ahora?", "Actualización automática", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                    {
                        this.actualiceAPP();
                    }
                }
                UploadProcessViewModel vm = new UploadProcessViewModel();
                vm.PropertyChanged += delegate(object sndr, PropertyChangedEventArgs args)
                {
                    if (args.PropertyName.ToLower() == "jobdone")
                    {
                        if (!((UploadProcessViewModel)sndr).JobDone)
                        {
                            Action a = () => this.ShowImgSync();
                            this.Dispatcher.BeginInvoke(a);
                        }
                        else
                        {
                            Action a = () => this.HideImgSync();
                            this.Dispatcher.BeginInvoke(a);
                        }
                    }

                    if (args.PropertyName.ToLower() == "message")
                    {
                        Action a = () => this.SetImgSyncMsg(((UploadProcessViewModel)sndr).Message);
                        this.Dispatcher.BeginInvoke(a);
                    }
                };
                //DlgUpload ds = new DlgUpload();
                //ds.DataContext = vm;
                //ds.Owner = Application.Current.Windows[0];
                //ds.ShowDialog();
                vm.start();
            }
            else {

                VersionViewModel.IsRunning = false;
            }
        }

        private void imgSyncFiles_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DTimerUploadProcess_Tick(null, new EventArgs());
        }

        private void CerrarSesión_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Seguro que dese salir", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LoginView ds = new LoginView();
                LoginViewModel viewModel = new LoginViewModel();
                ds.DataContext = viewModel;
                ds.Show();
                this.Close();
            }
            
            
            
        }
    }
}
