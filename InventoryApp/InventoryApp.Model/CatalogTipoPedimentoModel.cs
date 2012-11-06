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
        private FixupCollection<TIPO_PEDIMENTO> _tipoPedimento;
        private IDataMapper _dataMapper;
        //private bool _isChecked;

        //public bool IsChecked
        //{
        //    get { return this._isChecked; }
        //    set
        //    {
        //        if (value != this._isChecked)
        //        {
        //            this._isChecked = value;
        //            if (this.PropertyChanged != null)
        //                this.PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
        //        }
        //    }
        //}

        public FixupCollection<TIPO_PEDIMENTO> TipoPedimento
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
            this._tipoPedimento = new  FixupCollection<TIPO_PEDIMENTO>();
            this._selectedTipoPedimento = new TIPO_PEDIMENTO();
            //this._isChecked = false;
            this.loadItems();
            
        }
        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<TIPO_PEDIMENTO> ic = element as FixupCollection<TIPO_PEDIMENTO>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                //this._itemStatus = ic;
                this.TipoPedimento = ic;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
