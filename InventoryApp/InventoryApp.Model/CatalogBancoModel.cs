using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogBancoModel : INotifyPropertyChanged
    {
        private BANCO _selectedBanco;
        private FixupCollection<DeleteBanco> _banco;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteBanco> Banco
        {
            get { 
                return _banco; 
                }
            set
            {
                if (_banco !=value)
                {
                    _banco = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("Banco"));
	                    }
                }
            }
        }
        public BANCO SelectedBanco
        {
            get
            {
                return _selectedBanco;
            }
            set
            {
                if (_selectedBanco != value)
                {
                    _selectedBanco = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedBanco"));
                    }
                }
            }
        }

        public void loadBancos()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteBanco> ic = new FixupCollection<DeleteBanco>();

            if (element != null)
            {
                if (((List<BANCO>)element).Count > 0)
                {
                    foreach (BANCO item in (List<BANCO>)element)
                    {
                        DeleteBanco aux = new DeleteBanco(item);
                        ic.Add(aux);
                    }
                }
             }
            this.Banco = ic;
        }

        public void deleteBancos()
        {
            foreach (DeleteBanco item in this._banco)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogBancoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new BancoDataMapper();
            this._banco = new FixupCollection<DeleteBanco>();
            this._selectedBanco = new BANCO();
            this.loadBancos();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
