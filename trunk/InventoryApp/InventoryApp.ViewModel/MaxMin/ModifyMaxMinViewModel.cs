using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.MaxMin
{
    public class ModifyMaxMinViewModel
    {
        #region Fields
        private MaxMinModel _modiMaxMin;
        private RelayCommand _modifyMaxMinCommand;
        private MaxMinViewModel _maxMinViewModel;
        private CatalogArticuloModel _catalogArticuloModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        #endregion

        //Exponer la propiedad MaxMin almacen y articulo
        #region Props
        public MaxMinModel ModiMaxMin
        {
            get
            {
                return _modiMaxMin;
            }
            set
            {
                _modiMaxMin = value;
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

        public ICommand ModifyMaxMinCommand
        {
            get
            {
                if (_modifyMaxMinCommand == null)
                {
                    _modifyMaxMinCommand = new RelayCommand(p => this.AttempModifyMaxMin(), p => this.CanAttempModifyMaxMin());
                }
                return _modifyMaxMinCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyMaxMinViewModel(MaxMinViewModel maxMinViewModel, MaxMinModel selectedMaxMinModel)
        {
            this._modiMaxMin = new MaxMinModel(new MaxMinDataMapper());
            this._maxMinViewModel = maxMinViewModel;
            this._modiMaxMin.UnidMaxMin = selectedMaxMinModel.UnidMaxMin;
            this._modiMaxMin.Max = selectedMaxMinModel.Max;
            this._modiMaxMin.Min = selectedMaxMinModel.Min;
            this._modiMaxMin.Almacen = selectedMaxMinModel.Almacen;
            this._modiMaxMin.Articulo = selectedMaxMinModel.Articulo;
            try
            {

                this._catalogArticuloModel = new CatalogArticuloModel (new ArticuloDataMapper());
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
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyMaxMin()
        {
            bool _canAddMaxMin = true;
            if (this.CatalogArticuloModel.Articulos.Count != 0)
                _canAddMaxMin = false;

            return _canAddMaxMin;
        }

        public void AttempModifyMaxMin()
        {
            this._modiMaxMin.updateMaxMin();

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
