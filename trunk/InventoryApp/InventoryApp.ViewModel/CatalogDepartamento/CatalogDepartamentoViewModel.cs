using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogDepartamento
{
    public class CatalogDepartamentoViewModel
    {
        private RelayCommand _deleteDepartamentoCommand;

        public ICommand DeleteDepartamentoCommand
        {
            get
            {
                if (_deleteDepartamentoCommand == null)
                {
                    _deleteDepartamentoCommand = new RelayCommand(p => this.AttempDeleteDepartamento(), p => this.CanAttempDeleteDepartamento());
                }
                return _deleteDepartamentoCommand;
            }
        }

        private CatalogDepartamentoModel _catalogDepartamentoModel;

        public CatalogDepartamentoViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new DepartamentoDataMapper();
                this._catalogDepartamentoModel = new CatalogDepartamentoModel(dataMapper);   
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
            
        }
        public CatalogDepartamentoModel CatalogDepartamentoModel
        {
            get
            {
                return _catalogDepartamentoModel;
            }
            set
            {
                _catalogDepartamentoModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogDepartamentoModel.loadDepartamentos();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddDepartamentoViewModel CreateAddDepartamentoViewModel()
        {
            return new AddDepartamentoViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyDepartamentoViewModel CreateDepartamentoViewModel()
        {
            DepartamentoModel departamentoModel=new DepartamentoModel(new DepartamentoDataMapper());
            if (this._catalogDepartamentoModel != null && this._catalogDepartamentoModel.SelectedDepartamento != null)
            {
                departamentoModel.DepartamentoName = this._catalogDepartamentoModel.SelectedDepartamento.DEPARTAMENTO_NAME;
                departamentoModel.UnidDepartamento = this._catalogDepartamentoModel.SelectedDepartamento.UNID_DEPARTAMENTO;
            }
            return new ModifyDepartamentoViewModel(this,departamentoModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteDepartamento()
        {
            bool _canDeleteDepartamento = false;
            foreach (DeleteDepartamento d in this._catalogDepartamentoModel.Departamento)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteDepartamento = true;
                }
            }

            return _canDeleteDepartamento;
        }

        public void AttempDeleteDepartamento()
        {
            this._catalogDepartamentoModel.deleteDepartamentos();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogDepartamentoModel != null)
            {
                this._catalogDepartamentoModel.loadDepartamentos();
            }
        }
        #endregion
    }
}
