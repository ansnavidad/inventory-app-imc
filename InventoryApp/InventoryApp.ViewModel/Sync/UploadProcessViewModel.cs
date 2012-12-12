using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.ViewModel.Sync
{
    public class UploadProcessViewModel : ViewModelBase
    {
        System.Timers.Timer t;
        string _message;
        bool _jobDone;


        public string Message
        {
            get { return _message; }
            set
            {
                if (value != _message)
                {
                    this._message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public bool JobDone
        {
            get { return _jobDone; }
            set
            {
                if (value != _jobDone)
                {
                    this._jobDone = value;
                    OnPropertyChanged("JobDone");
                }
            }
        }

        public UploadProcessViewModel()
        {
            this.Message = "Test";
            this._jobDone = false;
            t = new System.Timers.Timer(100);
            t.Enabled = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(UploadData);
        }

        public void start()
        {
            t.Start();
        }

        public void UploadData(Object sender, System.Timers.ElapsedEventArgs args)
        {
            //No borrar! Esto debe ir en la primera sentencia
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();

            //Poner lógica de consumo de servicios para enviar los datos
            this.Message = "Enviando almacen ...";


            //Poner lógica de consumo de servicios para enviar los datos
            this.Message = "Enviando almacen tecnico ...";
            
            //Esta instrucción cierra la ventana
            this.JobDone = true;
        }


    }
}
