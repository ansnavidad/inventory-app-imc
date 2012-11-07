using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogEmpresa
{
    public class ModifyEmpresaViewModel
    {
        #region Fields
        private EmpresaModel _modiEmpresa;
        private RelayCommand _modifyEmpresaCommand;
        private CatalogEmpresaViewModel _catalogEmpresaViewModel;
        #endregion

        //Exponer la propiedad empresa
        #region Props
        public EmpresaModel ModiEmpresa
        {
            get
            {
                return _modiEmpresa;
            }
            set
            {
                _modiEmpresa = value;
            }
        }
        public ICommand ModifyEmpresaCommand
        {
            get
            {
                if (_modifyEmpresaCommand == null)
                {
                    _modifyEmpresaCommand = new RelayCommand(p => this.AttempModifyEmpresa(), p => this.CanAttempModifyEmpresa());
                }
                return _modifyEmpresaCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyEmpresaViewModel(CatalogEmpresaViewModel catalogEmpresaViewModel, EmpresaModel selectedEmpresaModel)
        {
            this._modiEmpresa = new EmpresaModel(new EmpresaDataMapper());
            this._catalogEmpresaViewModel = catalogEmpresaViewModel;
            this._modiEmpresa.UnidEmpresa = selectedEmpresaModel.UnidEmpresa;
            this._modiEmpresa.EmpresaName = selectedEmpresaModel.EmpresaName;
            this._modiEmpresa.Direccion = selectedEmpresaModel.Direccion;
            this._modiEmpresa.RazonSocial = selectedEmpresaModel.RazonSocial;
            this._modiEmpresa.Rfc = selectedEmpresaModel.Rfc;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyEmpresa()
        {
            bool _canAddEmpresa = true;
            if (String.IsNullOrEmpty(this._modiEmpresa.EmpresaName) ||
                String.IsNullOrEmpty(this._modiEmpresa.Direccion) ||
                String.IsNullOrEmpty(this._modiEmpresa.RazonSocial) ||
                String.IsNullOrEmpty(this._modiEmpresa.Rfc))
                _canAddEmpresa = false;

            return _canAddEmpresa;
        }

        public void AttempModifyEmpresa()
        {
            this._modiEmpresa.updateEmpresa();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogEmpresaViewModel != null)
            {
                this._catalogEmpresaViewModel.loadItems();
            }
        }
        #endregion
    }
}
