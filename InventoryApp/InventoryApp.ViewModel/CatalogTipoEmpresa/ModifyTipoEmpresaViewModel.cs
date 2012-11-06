using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
{
    public class ModifyTipoEmpresaViewModel
    {
        #region Fields
        private InsertTipoEmpresaModel _tipoEmpresa;
        private RelayCommand _modifyItemCommand;
        private CatalogTipoEmpresaViewModel _catalogTipoEmpresaViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public InsertTipoEmpresaModel TipoEmpresa
        {
            get
            {
                return _tipoEmpresa;
            }
            set
            {
                _tipoEmpresa = value;
            }
        }

        public ICommand ModifyItemCommand
        {
            get
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyTipoEmpresa(), p => this.CanAttempModifyTipoEmpresa());
                }
                return _modifyItemCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyTipoEmpresaViewModel(CatalogTipoEmpresaViewModel catalogTipoEmpresaViewModel, InsertTipoEmpresaModel selectedTipoEmpresaModel)
        {
            this._tipoEmpresa = new InsertTipoEmpresaModel(new TipoEmpresaDataMapper());
            this._catalogTipoEmpresaViewModel = catalogTipoEmpresaViewModel;
            this._tipoEmpresa.UnidTipoEmpresa = selectedTipoEmpresaModel.UnidTipoEmpresa;
            this._tipoEmpresa.TipoEmpresaName = selectedTipoEmpresaModel.TipoEmpresaName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyTipoEmpresa()
        {
            bool _canAddTipoEmpresa = true;
            if (String.IsNullOrEmpty(this._tipoEmpresa.TipoEmpresaName))
                _canAddTipoEmpresa = false;

            return _canAddTipoEmpresa;
        }

        public void AttempModifyTipoEmpresa()
        {
            this._tipoEmpresa.updateTipoEmpresa();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTipoEmpresaViewModel != null)
            {
                this._catalogTipoEmpresaViewModel.loadItems();
            }
        }
        #endregion
    }
}
