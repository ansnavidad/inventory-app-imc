using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class CatalogBancoModel : INotifyPropertyChanged 
    {
        private FixupCollection<BANCO> _banco;
        private BANCO _selectedBanco;
        private IDataMapper _dataMapper;

        public FixupCollection<BANCO> Banco
        {
            get { return _banco; }
            set { 
                if(_banco != value){
                    _banco = value;
                    if(PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Banco"));
                    }
                }
            }
        }

        public BANCO SelectedBanco
        {
            get { return _selectedBanco; }
            set { 
                if(_selectedBanco != value){
                    _selectedBanco = value;
                    if(PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedBanco"));
                    }
                }
            }
        }

        public void loadBancos()
        {
            object element = this._dataMapper.getElements();

            
        }

        public CatalogBancoModel(IDataMapper dataMapper){
            this._dataMapper = new BancoDataMapper();
            this._selectedBanco = new BANCO();
            this.loadBancos();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
