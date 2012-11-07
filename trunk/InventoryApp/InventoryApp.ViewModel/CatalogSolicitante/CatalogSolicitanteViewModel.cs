using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogSolicitante
{
    public class CatalogSolicitanteViewModel
    {
        private CatalogSolicitanteModel _catalogSolicitanteModel;

        public CatalogSolicitanteViewModel()
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            
        }

        public CatalogSolicitanteModel CatalogSolicitanteModel
        {
            get
            {
                return _catalogSolicitanteModel;
            }
            set
            {
                _catalogSolicitanteModel = value;
            }
        }

        public void loadSolicitante()
        {
            this._catalogSolicitanteModel.loadSolicitante();
        }

        /// <summary>
        /// Crea una nueva instancia de de AddSolicitanteViewModel y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddSolicitanteViewModel CreateAddSolicitanteViewModel()
        {
            return new AddSolicitanteViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifySolicitanteViewModel y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifySolicitanteViewModel CreateModifySolicitanteViewModel()
        {
            SolicitanteModel solicitanteModel = new SolicitanteModel(new SolicitanteDataMapper());
            if (this._catalogSolicitanteModel != null && this._catalogSolicitanteModel.SelectedSolicitante != null)
            {
                solicitanteModel.UnidSolicitante = this._catalogSolicitanteModel.SelectedSolicitante.UNID_SOLICITANTE;
                solicitanteModel.SolicitanteName = this._catalogSolicitanteModel.SelectedSolicitante.SOLICITANTE_NAME;
                solicitanteModel.Empresa = this._catalogSolicitanteModel.SelectedSolicitante.EMPRESA;
                solicitanteModel.Departamento = this._catalogSolicitanteModel.SelectedSolicitante.DEPARTAMENTO;
                solicitanteModel.Email = this._catalogSolicitanteModel.SelectedSolicitante.EMAIL;
                solicitanteModel.Validador = this._catalogSolicitanteModel.SelectedSolicitante.VALIDADOR;
            }
            return new ModifySolicitanteViewModel(this, solicitanteModel);
        }
    }
}
