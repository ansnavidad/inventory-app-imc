using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DepartamentoESModel : SOLICITANTE, INotifyPropertyChanged
    {        
        private DEPARTAMENTO _depa;

        public DEPARTAMENTO Depa
        {
            get
            {
                return _depa;
            }
            set
            {
                if (_depa != value)
                {
                    _depa = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Depa"));
                    }
                }
            }
        }        

        public DepartamentoESModel(SOLICITANTE Solicitante)
        {
            this._depa = Solicitante.DEPARTAMENTO;
            this.EMAIL = Solicitante.EMAIL;
            this.IS_ACTIVE = Solicitante.IS_ACTIVE;
            this.SOLICITANTE_NAME = Solicitante.SOLICITANTE_NAME;
            this.UNID_DEPARTAMENTO = Solicitante.UNID_DEPARTAMENTO;
            this.UNID_EMPRESA = Solicitante.UNID_EMPRESA;
            this.UNID_SOLICITANTE = Solicitante.UNID_SOLICITANTE;
            this.VALIDADOR = this.VALIDADOR;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
