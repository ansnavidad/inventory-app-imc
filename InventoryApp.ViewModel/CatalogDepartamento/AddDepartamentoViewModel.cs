using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogDepartamento
{
    public class AddDepartamentoViewModel
    {
        #region Fields
        private DepartamentoModel _departamento;
        private RelayCommand _addDepartamentoCommand;
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

        public ICommand AddDepartamentoCommand
        {
            get 
            {
                if (_addDepartamentoCommand == null)
                {
                    _addDepartamentoCommand = new RelayCommand(p => this.AttempAddDepartamento(), p => this.CanAttempAddDepartamento());
                }
                return _addDepartamentoCommand; 
            }
        }
        #endregion

        #region Constructors

        public AddDepartamentoViewModel(CatalogDepartamentoViewModel catalogDepartamentoViewModel)
        {
            this._departamento = new DepartamentoModel(new DepartamentoDataMapper(), catalogDepartamentoViewModel.ActualUser);
            this._catalogDepartamentoViewModel = catalogDepartamentoViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddDepartamento()
        {
            bool _canAddDepartamento = true;
            if (String.IsNullOrEmpty(this._departamento.DepartamentoName))
                _canAddDepartamento = false;

            return _canAddDepartamento;
        }

        public void AttempAddDepartamento()
        {
            this._departamento.saveDeparatamento();

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
