using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogDepartamento
{
    public class ModifyDepartamentoViewModel
    {
        #region Fields
        private DepartamentoModel _departamento;
        private RelayCommand _modifyDepartamentoCommand;
        private CatalogDepartamentoViewModel _catalogDepartamentoViewModel;
        #endregion

        #region Props
        public DepartamentoModel Departamento 
        {
            get
            {
                return _departamento;
            }
            set
            {
                _departamento = value;
            }
        }

        public ICommand ModifyDepartamentoCommand
        {
            get 
            {
                if (_modifyDepartamentoCommand == null)
                {
                    _modifyDepartamentoCommand = new RelayCommand(p => this.AttempModifyDepartamento(), p => this.CanAttempModifyDepartamento());
                }
                return _modifyDepartamentoCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyDepartamentoViewModel(CatalogDepartamentoViewModel catalogDepartamentoViewModel,DepartamentoModel selectedDepartamentoModel)
        {
            this._departamento = new DepartamentoModel(new DepartamentoDataMapper());
            this._catalogDepartamentoViewModel = catalogDepartamentoViewModel;
            this._departamento.UnidDepartamento = selectedDepartamentoModel.UnidDepartamento;
            this._departamento.DepartamentoName = selectedDepartamentoModel.DepartamentoName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyDepartamento()
        {
            bool _canAddDepartamento = true;
            if (String.IsNullOrEmpty(this._departamento.DepartamentoName))
                _canAddDepartamento = false;

            return _canAddDepartamento;
        }

         public void AttempModifyDepartamento()
        {
            this._departamento.updateDepartamento();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogDepartamentoViewModel != null)
            {
                this._catalogDepartamentoViewModel.loadItems();
            }
        }

        #endregion
    }
}
