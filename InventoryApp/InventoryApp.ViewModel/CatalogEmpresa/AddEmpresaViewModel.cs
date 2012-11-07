using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogEmpresa
{
    public class AddEmpresaViewModel
    {
        #region Fields
        private EmpresaModel _addEmpresa;
        private RelayCommand _addEmpresaCommand;
        private CatalogEmpresaViewModel _catalogEmpresaViewModel;
        #endregion

        //Exponer la propiedad empresa
        #region Props
        public EmpresaModel AddEmpresa
        {
            get
            {
                return _addEmpresa;
            }
            set
            {
                _addEmpresa = value;
            }
        }
        public ICommand AddEmpresaCommand
        {
            get
            {
                if (_addEmpresaCommand == null)
                {
                    _addEmpresaCommand = new RelayCommand(p => this.AttempAddEmpresa(), p => this.CanAttempAddEmpresa());
                }
                return _addEmpresaCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddEmpresaViewModel(CatalogEmpresaViewModel catalogEmpresaViewModel)
        {
            this._addEmpresa = new EmpresaModel(new EmpresaDataMapper());
            this._catalogEmpresaViewModel = catalogEmpresaViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddEmpresa()
        {
            bool _canAddEmpresa = true;
            if (String.IsNullOrEmpty(this._addEmpresa.EmpresaName) ||
                String.IsNullOrEmpty(this._addEmpresa.Direccion) || 
                String.IsNullOrEmpty(this._addEmpresa.RazonSocial)||
                String.IsNullOrEmpty(this._addEmpresa.Rfc))
                _canAddEmpresa = false;

            return _canAddEmpresa;
        }

        public void AttempAddEmpresa()
        {
            this._addEmpresa.saveEmpresa();

            //Puede ser que para pruebas unitarias catalogEmpresaViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogEmpresaViewModel != null)
            {
                this._catalogEmpresaViewModel.loadItems();
            }
        }
        #endregion
    }
}
