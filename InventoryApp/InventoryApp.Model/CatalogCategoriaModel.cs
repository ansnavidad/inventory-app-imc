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
        private FixupCollection<DeleteCategoria> _itemCategoria;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteCategoria> Categoria
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
            this._itemCategoria = new FixupCollection<DeleteCategoria>();
            this._selectedCategoria = new CATEGORIA();
            this.loadItems();    
        }
        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<DeleteCategoria> ic = new FixupCollection<DeleteCategoria>();

            if (element != null)
            {
                if (((List<CATEGORIA>)element).Count > 0)
                {
                    foreach (CATEGORIA item in (List<CATEGORIA>)element)
                    {
                        DeleteCategoria aux = new DeleteCategoria(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Categoria = ic;
        }

        public void deleteCategoria()
        {
            foreach (DeleteCategoria item in this._itemCategoria)
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
