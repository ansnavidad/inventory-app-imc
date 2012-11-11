using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogTipoPedimentoModel : INotifyPropertyChanged
    {
        private TIPO_PEDIMENTO _selectedTipoPedimento;
        private FixupCollection<DeleteTipoPedimento> _tipoPedimento;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteTipoPedimento> TipoPedimento
        {
            get { 
                return _tipoPedimento; 
                }
            set
            {
                if (_tipoPedimento !=value)
                {
                    _tipoPedimento = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("TipoPedimento"));
	                    }
                }
            }
        }
        public TIPO_PEDIMENTO SelectedTipoPedimento
        {
            get
            {
                return _selectedTipoPedimento;
            }
            set
            {
                if (_selectedTipoPedimento != value)
                {
                    _selectedTipoPedimento = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedTipoPedimento"));
                    }
                }
            }
        }

        public CatalogTipoPedimentoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TipoPedimentoDataMapper();
            this._tipoPedimento = new FixupCollection<DeleteTipoPedimento>();
            this._selectedTipoPedimento = new TIPO_PEDIMENTO();
            this.loadItems();
            
        }
        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<DeleteTipoPedimento> ic = new FixupCollection<DeleteTipoPedimento>();

            if (element != null)
            {
                if (((List<TIPO_PEDIMENTO>)element).Count > 0)
                {
                    foreach (TIPO_PEDIMENTO item in (List<TIPO_PEDIMENTO>)element)
                    {
                        DeleteTipoPedimento aux = new DeleteTipoPedimento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.TipoPedimento = ic;
        }

        public void deleteTipoPedimento()
        {
            foreach (DeleteTipoPedimento item in this._tipoPedimento)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
