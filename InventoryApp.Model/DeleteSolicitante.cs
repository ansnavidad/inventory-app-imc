using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteSolicitante : SOLICITANTE, INotifyPropertyChanged
    {
        private bool _isChecked;
        private DEPARTAMENTO _departamento;
        private EMPRESA _empresa;

        public DEPARTAMENTO Departamento
        {
            get { return this._departamento; }
            set
            {
                if (value != this._departamento)
                {
                    this._departamento = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Departamento"));
                }
            }
        }

        public EMPRESA Empresa
        {
            get { return this._empresa; }
            set
            {
                if (value != this._empresa)
                {
                    this._empresa = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Empresa"));
                }
            }
        }

        public bool IsChecked
        {
            get { return this._isChecked; }
            set
            {
                if (value != this._isChecked)
                {
                    this._isChecked = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        public DeleteSolicitante(SOLICITANTE solicitante)
        {
            this.UNID_SOLICITANTE = solicitante.UNID_SOLICITANTE;
            this.SOLICITANTE_NAME = solicitante.SOLICITANTE_NAME;
            this.EMAIL = solicitante.EMAIL;
            this.VALIDADOR = solicitante.VALIDADOR;
            this._empresa = solicitante.EMPRESA;
            this._departamento = solicitante.DEPARTAMENTO;
            this.IS_ACTIVE = solicitante.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
