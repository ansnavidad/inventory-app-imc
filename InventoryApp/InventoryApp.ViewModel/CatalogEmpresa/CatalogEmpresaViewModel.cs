using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogEmpresa
{
    public class CatalogEmpresaViewModel : IPageViewModel
    {
        private RelayCommand _deleteEmpresaCommand;

        private CatalogEmpresaModel _catalogEmpresaModel;
        public USUARIO ActualUser;

        public ICommand DeleteEmpresaCommand
        {
            get
            {
                if (_deleteEmpresaCommand == null)
                {
                    _deleteEmpresaCommand = new RelayCommand(p => this.AttempDeleteEmpresa(), p => this.CanAttempDeleteEmpresa());
                }
                return _deleteEmpresaCommand;
            }
        }

        public CatalogEmpresaViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new EmpresaDataMapper();
                this._catalogEmpresaModel = new CatalogEmpresaModel(dataMapper);   
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

        public CatalogEmpresaModel CatalogEmpresaModel
        {
            get
            {
                return _catalogEmpresaModel;
            }
            set
            {
                _catalogEmpresaModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogEmpresaModel.loadEmpresa();
        }

        /// <summary>
        /// Crea una nueva instancia de AddEmpresa y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddEmpresaViewModel CreateAddEmpresaViewModel()
        {
            return new AddEmpresaViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyEmpresa y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyEmpresaViewModel CreateModifyEmpresaViewModel()
        {
            EmpresaModel empresaModel = new EmpresaModel(new EmpresaDataMapper());
            if (this._catalogEmpresaModel != null && this._catalogEmpresaModel.SelectedEmpresa != null)
            {
                empresaModel.UnidEmpresa = this._catalogEmpresaModel.SelectedEmpresa.UNID_EMPRESA;
                empresaModel.EmpresaName = this._catalogEmpresaModel.SelectedEmpresa.EMPRESA_NAME;
                empresaModel.Direccion = this._catalogEmpresaModel.SelectedEmpresa.DIRECCION;
                empresaModel.RazonSocial = this._catalogEmpresaModel.SelectedEmpresa.RAZON_SOCIAL;
                empresaModel.Rfc = this._catalogEmpresaModel.SelectedEmpresa.RFC;
            }
            return new ModifyEmpresaViewModel(this, empresaModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteEmpresa()
        {
            bool _canDeleteEmpresa = false;
            foreach (DeleteEmpresa d in this._catalogEmpresaModel.Empresa)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteEmpresa = true;
                }
            }

            return _canDeleteEmpresa;
        }

        public void AttempDeleteEmpresa()
        {
            this._catalogEmpresaModel.deleteEmpresa();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogEmpresaModel != null)
            {
                this._catalogEmpresaModel.loadEmpresa();
            }
        }
        #endregion


        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
