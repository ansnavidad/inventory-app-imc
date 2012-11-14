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
        private SalidaDemoViewModel _salidaDemoViewModel;

        public CatalogItemViewModel(SalidaRentaViewModel _salidaRentaViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaRentaViewModel = _salidaRentaViewModel;

        }

        public CatalogItemViewModel(SalidaDemoViewModel _salidaDemoViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaDemoViewModel = _salidaDemoViewModel;

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

        public SalidaDemoViewModel SalidaDemoViewModel
        {
            get
            {
                return _salidaDemoViewModel;
            }
            set
            {
                _salidaDemoViewModel = value;
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
            if (this._salidaRentaViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaRentaViewModel.MovimientoModel.AlmacenProcedencia);
            }
            else if (this._salidaDemoViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaDemoViewModel.MovimientoModel.AlmacenProcedencia);
            }
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
            if (this._salidaRentaViewModel != null)
            {
                passItems(this._salidaRentaViewModel);
            }else if (this._salidaDemoViewModel != null){
                passItems(this._salidaDemoViewModel);
            }
        }

        public void passItems(SalidaRentaViewModel salida)
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

        public void passItems(SalidaDemoViewModel salida)
        {
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                {
                    bool aux = true;

                    for (int i = 0; i < this._salidaDemoViewModel.ItemModel.ItemModel.Count; i++)
                    {

                        if (this._salidaDemoViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                            aux = false;
                    }

                    if (aux)
                    {
                        item.IsChecked = false;
                        this._salidaDemoViewModel.ItemModel.ItemModel.Add(item);



                    }
                }

            }
            this.SalidaDemoViewModel.MovimientoModel.CantidadItems = this.SalidaDemoViewModel.ItemModel.ItemModel.Count();
        }

        #endregion

        
    }
}
