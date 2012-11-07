using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogSolicitanteModel : INotifyPropertyChanged
    {
        private SOLICITANTE _selectedSolicitante;
        private FixupCollection<SOLICITANTE> _solicitante;
        private IDataMapper _dataMapper;

        public FixupCollection<SOLICITANTE> Solicitante
        {
            get
            {
                return _solicitante;
            }
            set
            {
                if (_solicitante != value)
                {
                    _solicitante = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Solicitante"));
                    }
                }
            }
        }

        public SOLICITANTE SelectedSolicitante
        {
            get
            {
                return _selectedSolicitante;
            }
            set
            {
                if (_selectedSolicitante != value)
                {
                    _selectedSolicitante = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedSolicitante"));
                    }
                }
            }
        }

        public CatalogSolicitanteModel(IDataMapper dataMapper)
        {
            this._dataMapper = new SolicitanteDataMapper();
            this._solicitante = new FixupCollection<SOLICITANTE>();
            this._selectedSolicitante = new SOLICITANTE();
            //this._isChecked = false;
            this.loadSolicitante();
        }
        public void loadSolicitante()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<SOLICITANTE> ic = new FixupCollection<SOLICITANTE>();

            foreach (SOLICITANTE elemento in (List<SOLICITANTE>)element)
            {
                ic.Add((SOLICITANTE)elemento);
            }
            if (ic != null)
            {
                this.Solicitante = ic;
            }
            //FixupCollection<SOLICITANTE> ic = element as FixupCollection<SOLICITANTE>;
            //if (ic != null)
            //{
            //    this.Solicitante = ic;
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
