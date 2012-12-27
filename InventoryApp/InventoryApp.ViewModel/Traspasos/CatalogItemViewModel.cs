using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Traspasos
{
    public class CatalogItemViewModel
    {

        private CatalogItemModel _catalogItemModel;
        private RelayCommand _addItemCommand;
        private RelayCommand _addItemsCommand;
        private TraspasoStockViewModel _traspasoStockViewModel;


        public CatalogItemViewModel(TraspasoStockViewModel _traspasoStockViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._traspasoStockViewModel = _traspasoStockViewModel;

        }

        public CatalogItemModel CatalogItemModel
        {
            get
            {
                return _catalogItemModel;
            }
            set
            {
                _catalogItemModel = value;
            }
        }

        public TraspasoStockViewModel TraspasoStockViewModel
        {
            get
            {
                return _traspasoStockViewModel;
            }
            set
            {
                _traspasoStockViewModel = value;
            }
        }

        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempArticulo(), p => this.CanAttempArticulo());
                }
                return _addItemCommand;
            }
        }

        public ICommand AddItemsCommand
        {
            get
            {
                if (_addItemsCommand == null)
                {
                    _addItemsCommand = new RelayCommand(p => this.AttempItems(), p => this.CanAttempItems());
                }
                return _addItemsCommand;
            }
        }


        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempArticulo()
        {
            bool _canInsertArticulo = false;
            if (!String.IsNullOrEmpty(this._catalogItemModel.Serie) || !String.IsNullOrEmpty(this._catalogItemModel.SKU))
                _canInsertArticulo = true;

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            this.CatalogItemModel.loadItems(_traspasoStockViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí xD");
        }

        public bool CanAttempItems()
        {
            bool _canInsertArticulo = false;
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                    _canInsertArticulo = true;
            }

            return _canInsertArticulo;
        }

        public void AttempItems()
        {
            if (_traspasoStockViewModel != null)
            {
                passItems(_traspasoStockViewModel);
            }
        }

        public void passItems(TraspasoStockViewModel traspaso)
        {
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                {
                    bool aux = true;

                    for (int i = 0; i < this._traspasoStockViewModel.ItemModel.ItemModel.Count; i++)
                    {
                        if (this._traspasoStockViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                            aux = false;
                    }

                    if (aux)
                    {
                        item.IsChecked = false;
                        this._traspasoStockViewModel.ItemModel.ItemModel.Add(item);
                    }
                }
            }
            this.TraspasoStockViewModel.MovimientoModel.CantidadItems = this.TraspasoStockViewModel.ItemModel.ItemModel.Count();
        }

        #endregion
    }
}
