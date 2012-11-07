using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
{
    public class CatalogTransporteViewModel
    {
        private CatalogTransporteModel _catalogTransporteModel;

        public CatalogTransporteViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TransporteDataMapper();
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper);
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

        public CatalogTransporteModel CatalogTransporteModel
        {
            get
            {
                return _catalogTransporteModel;
            }
            set
            {
                _catalogTransporteModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogTransporteModel.loadTransporte();
        }

        public InsertTransporteViewModel CreateInsertTransporteViewModel()
        {
            InsertTransporteViewModel p;
            try
            {
                p = new InsertTransporteViewModel(this);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            return p;
        }

        public ModifyTransporteViewModel CreateModifyTransporteViewModel()
        {
            InsertTransporteModel transporteModel = new InsertTransporteModel(new TransporteDataMapper());
            if (this._catalogTransporteModel != null && this._catalogTransporteModel.SelectedTransporte != null)
            {
                transporteModel.TransporteName = this._catalogTransporteModel.SelectedTransporte.TRANSPORTE_NAME;
                transporteModel.UnidTransporte = this._catalogTransporteModel.SelectedTransporte.UNID_TRANSPORTE;
                transporteModel.TipoEmpresa = this._catalogTransporteModel.SelectedTransporte.TIPO_EMPRESA;
                

            }
            return new ModifyTransporteViewModel(this, transporteModel);
        }
    }
}
