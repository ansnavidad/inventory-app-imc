﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.ViewModel.CatalogArticulo;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.MaxMin
{
    public class AddMaxMinViewModel
    {
        #region Fields
        private MaxMinModel _addMaxMin;
        private RelayCommand _addMaxMinCommand;
        private MaxMinViewModel _maxMinViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        private CatalogArticuloModel _catalogArticuloModel;
        #endregion

        //Exponer la propiedad maxMin articulo y almacen
        #region Props
        public MaxMinModel AddMaxMin
        {
            get
            {
                return _addMaxMin;
            }
            set
            {
                _addMaxMin = value;
            }
        }

        public CatalogAlmacenModel CatalogAlmacenModel
        {
            get
            {
                return _catalogAlmacenModel;
            }
            set
            {
                _catalogAlmacenModel = value;
            }
        }

        public CatalogArticuloModel CatalogArticuloModel
        {
            get
            {
                return _catalogArticuloModel;
            }
            set
            {
                _catalogArticuloModel = value;
            }
        }

        public ICommand AddMaxMinCommand
        {
            get
            {
                if (_addMaxMinCommand == null)
                {
                    _addMaxMinCommand = new RelayCommand(p => this.AttempAddMaxMin(), p => this.CanAttempAddMaxMin());
                }
                return _addMaxMinCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddMaxMinViewModel(MaxMinViewModel maxMinViewModel)
        {
            this._addMaxMin = new MaxMinModel(new MaxMinDataMapper());
            this._maxMinViewModel = maxMinViewModel;
            try
            {

                this._catalogAlmacenModel = new CatalogAlmacenModel(new AlmacenDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {

                this._catalogArticuloModel = new CatalogArticuloModel(new ArticuloDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddMaxMin()
        {
            bool _canAddMaxMin = true;
            if (this.CatalogArticuloModel.Articulos.Count != 0)
                _canAddMaxMin = false;

            return _canAddMaxMin;
        }

        public void AttempAddMaxMin()
        {
            this._addMaxMin.saveMaxMin();

            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._maxMinViewModel != null)
            {
                this._maxMinViewModel.loadMaxMin();
            }
        }
        #endregion
    }
}