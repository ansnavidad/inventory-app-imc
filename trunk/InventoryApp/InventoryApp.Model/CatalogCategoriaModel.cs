using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogCategoriaModel : INotifyPropertyChanged
    {
        private CATEGORIA _selectedCategoria;
        private FixupCollection<CATEGORIA> _itemCategoria;
        private IDataMapper _dataMapper;

        public FixupCollection<CATEGORIA> Categoria
        {
            get { 
                return _itemCategoria; 
                }
            set
            {
                if (_itemCategoria !=value)
                {
                    _itemCategoria = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("Categoria"));
	                    }
                }
            }
        }
        public CATEGORIA SelectedCategoria
        {
            get
            {
                return _selectedCategoria;
            }
            set
            {
                if (_selectedCategoria != value)
                {
                    _selectedCategoria = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedCategoria"));
                    }
                }
            }
        }

        public CatalogCategoriaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new CategoriaDataMapper();
            this._itemCategoria = new FixupCollection<CATEGORIA>();
            this._selectedCategoria = new CATEGORIA();
            //this._isChecked = false;
            this.loadItems();
            
        }
        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<CATEGORIA> ic = element as FixupCollection<CATEGORIA>;
            if (ic != null)
            {
                this.Categoria = ic;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
