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
        private DeleteSolicitante _selectedSolicitante;
        private FixupCollection<DeleteSolicitante> _solicitante;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteSolicitante> Solicitante
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

        public DeleteSolicitante SelectedSolicitante
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
            this._solicitante = new FixupCollection<DeleteSolicitante>();
            //this._selectedSolicitante = new SOLICITANTE();
            this.loadSolicitante();
        }

        public void loadSolicitante()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<DeleteSolicitante> ic = new FixupCollection<DeleteSolicitante>();

            if (element != null)
            {
                if (((List<SOLICITANTE>)element).Count > 0)
                {
                    foreach (SOLICITANTE item in (List<SOLICITANTE>)element)
                    {
                        DeleteSolicitante aux = new DeleteSolicitante(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Solicitante = ic;
        }
        public void deleteSolicitante(USUARIO u)
        {
            foreach (DeleteSolicitante item in this._solicitante)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
