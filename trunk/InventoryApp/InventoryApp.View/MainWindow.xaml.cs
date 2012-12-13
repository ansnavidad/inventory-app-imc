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

namespace InventoryApp.View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer dTimerUploadProcess;
        public DispatcherTimer DTimerUploadProcess
        {
            get { return dTimerUploadProcess; }
            set { dTimerUploadProcess = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            //CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            //ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            //Thread.CurrentThread.CurrentCulture = ci;


            //DTimerUploadProcess = new DispatcherTimer();
            //DTimerUploadProcess.Tick += new EventHandler(DTimerUploadProcess_Tick);
            //DTimerUploadProcess.Interval = new TimeSpan(0, 0, 10);
            //DTimerUploadProcess.Start();
        }

        void DTimerUploadProcess_Tick(object sender, EventArgs e)
        {
            UploadProcessViewModel vm = new UploadProcessViewModel();
            DlgUpload ds = new DlgUpload();
            ds.DataContext = vm;
            ds.Owner = Application.Current.Windows[0];
            ds.ShowDialog();
            vm.start();
        }


    }
}
