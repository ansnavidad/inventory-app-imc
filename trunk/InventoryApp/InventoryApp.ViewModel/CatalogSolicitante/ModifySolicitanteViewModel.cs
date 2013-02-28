using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogSolicitante
{
    public class ModifySolicitanteViewModel
    {
        #region Fields
        private SolicitanteModel _modiSolicitante;
        private RelayCommand _modifySolicitanteCommand;
        private CatalogSolicitanteViewModel _catalogSolicitanteViewModel;
        private CatalogEmpresaModel _catalogEmpresaModel;
        private CatalogDepartamentoModel _catalogDepartamentoModel;
        #endregion

        //Exponer la propiedad solicitante
        #region Props
        public SolicitanteModel ModiSolicitante
        {
            get
            {
                return _modiSolicitante;
            }
            set
            {
                _modiSolicitante = value;
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

        public ICommand ModifySolicitanteCommand
        {
            get
            {
                if (_modifySolicitanteCommand == null)
                {
                    _modifySolicitanteCommand = new RelayCommand(p => this.AttempModifySolicitante(), p => this.CanAttempModifySolicitante());
                }
                return _modifySolicitanteCommand;
            }
        }
        #endregion

         #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifySolicitanteViewModel(CatalogSolicitanteViewModel catalogSolicitanteViewModel, SolicitanteModel selectedSolicitanteModel)
        {
            this._modiSolicitante = new SolicitanteModel(new SolicitanteDataMapper());
            this._catalogSolicitanteViewModel = catalogSolicitanteViewModel;
            this._modiSolicitante.UnidSolicitante = selectedSolicitanteModel.UnidSolicitante;
            this._modiSolicitante.SolicitanteName = selectedSolicitanteModel.SolicitanteName;
            this._modiSolicitante.Departamento = selectedSolicitanteModel.Departamento;
            this._modiSolicitante.Empresa = selectedSolicitanteModel.Empresa;
            this._modiSolicitante.Email = selectedSolicitanteModel.Email;
            this._modiSolicitante.Validador = selectedSolicitanteModel.Validador;

            try
            {

                this._catalogEmpresaModel = new CatalogEmpresaModel(new EmpresaDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {

                this._catalogDepartamentoModel = new CatalogDepartamentoModel(new DepartamentoDataMapper());
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
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifySolicitante()
        {
            bool _canAddSolicitante = true;
            if (String.IsNullOrEmpty(this._modiSolicitante.SolicitanteName) ||
                String.IsNullOrEmpty(this._modiSolicitante.Email) ||
                String.IsNullOrEmpty(this._modiSolicitante.Validador))
                _canAddSolicitante = false;

            return _canAddSolicitante;
        }

        public void AttempModifySolicitante()
        {
            this._modiSolicitante.updateSolicitante();

            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogSolicitanteViewModel != null)
            {
                this._catalogSolicitanteViewModel.loadSolicitante();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.ModiSolicitante);
            return historialViewModel;
        }
        #endregion
    }
}
