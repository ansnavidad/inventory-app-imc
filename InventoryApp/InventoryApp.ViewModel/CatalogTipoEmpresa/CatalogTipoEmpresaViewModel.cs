using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;

namespace InventoryApp.ViewModel
{
    public class CatalogTipoEmpresaViewModel
    {
        private CatalogTipoEmpresaModel _catalogTipoEmpresaModel;

        public CatalogTipoEmpresaViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TipoEmpresaDataMapper();
                this._catalogTipoEmpresaModel = new CatalogTipoEmpresaModel(dataMapper);
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

        public CatalogTipoEmpresaModel CatalogTipoEmpresaModel
        {
            get
            {
                return _catalogTipoEmpresaModel;
            }
            set
            {
                _catalogTipoEmpresaModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogTipoEmpresaModel.loadItems();
        }

        public InsertTipoEmpresaViewModel CreateInsertTipoEmpresaViewModel()
        {
            return new InsertTipoEmpresaViewModel(this);
        }

        public ModifyTipoEmpresaViewModel CreateModifyTipoEmpresaViewModel()
        {
            InsertTipoEmpresaModel tipoEmpresaModel = new InsertTipoEmpresaModel(new TipoEmpresaDataMapper());
            if (this._catalogTipoEmpresaModel != null && this._catalogTipoEmpresaModel.SelectedEmpresa != null)
            {
                tipoEmpresaModel.TipoEmpresaName = this._catalogTipoEmpresaModel.SelectedEmpresa.TIPO_EMPRESA_NAME;
                tipoEmpresaModel.UnidTipoEmpresa = this._catalogTipoEmpresaModel.SelectedEmpresa.UNID_TIPO_EMPRESA;

            }
            return new ModifyTipoEmpresaViewModel(this, tipoEmpresaModel);
        }
    }
}
