using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogEmpresa
{
    public class CatalogEmpresaViewModel
    {
        private CatalogEmpresaModel _catalogEmpresaModel;

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

    }
}
