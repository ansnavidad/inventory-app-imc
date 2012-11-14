using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Salidas
{
    public class CatalogItemViewModel
    {
        private CatalogItemModel _catalogItemModel;
        private RelayCommand _addItemCommand;
        private RelayCommand _addItemsCommand;
        private SalidaRentaViewModel _salidaRentaViewModel;

        public CatalogItemViewModel(SalidaRentaViewModel _salidaRentaViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaRentaViewModel = _salidaRentaViewModel;

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

        public SalidaRentaViewModel SalidaRentaViewModel
        {
            get
            {
                return _salidaRentaViewModel;
            }
            set
            {
                _salidaRentaViewModel = value;
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
            if (!String.IsNullOrEmpty(this._catalogItemModel.Serie))
                _canInsertArticulo = true;

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            this.CatalogItemModel.loadItems();
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
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                {
                    bool aux = true;

                    for (int i = 0; i < this._salidaRentaViewModel.ItemModel.ItemModel.Count; i++)
                    {

                        if (this._salidaRentaViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                            aux = false;
                    }

                    if (aux)
                    {
                        item.IsChecked = false;
                        this._salidaRentaViewModel.ItemModel.ItemModel.Add(item);



                    }
                }

            }
            this.SalidaRentaViewModel.MovimientoModel.CantidadItems = this.SalidaRentaViewModel.ItemModel.ItemModel.Count();

        }
        #endregion
    }
}
