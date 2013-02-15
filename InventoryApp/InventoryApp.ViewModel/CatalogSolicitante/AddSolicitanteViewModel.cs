using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using System.Windows;

namespace InventoryApp.ViewModel.CatalogSolicitante
{
    public class AddSolicitanteViewModel
    {
        #region Fields
        private SolicitanteModel _addSolicitante;
        private RelayCommand _addSolicitanteCommand;
        private CatalogSolicitanteViewModel _catalogSolicitanteViewModel;
        private CatalogEmpresaModel _catalogEmpresaModel;
        private CatalogDepartamentoModel _catalogDepartamentoModel;
        #endregion

        //Exponer la propiedad solicitante departamento y empresa
        #region Props
        public SolicitanteModel AddSolicitante
        {
            get
            {
                return _addSolicitante;
            }
            set
            {
                _addSolicitante = value;
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


        public ICommand AddSolicitanteCommand
        {
            get
            {
                if (_addSolicitanteCommand == null)
                {
                    _addSolicitanteCommand = new RelayCommand(p => this.AttempAddSolicitante(), p => this.CanAttempAddSolicitante());
                }
                return _addSolicitanteCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddSolicitanteViewModel(CatalogSolicitanteViewModel catalogSolicitanteViewModel)
        {
            this._addSolicitante = new SolicitanteModel(new SolicitanteDataMapper());
            this._catalogSolicitanteViewModel = catalogSolicitanteViewModel;
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
        public bool CanAttempAddSolicitante()
        {
            bool _canAddSolicitante = true;
            if (String.IsNullOrEmpty(this._addSolicitante.SolicitanteName) ||
                String.IsNullOrEmpty(this._addSolicitante.Email) ||
                String.IsNullOrEmpty(this._addSolicitante.Validador) ||
                this._addSolicitante.Departamento == null ||
                this._addSolicitante.Empresa == null)
                _canAddSolicitante = false;

            if (!String.IsNullOrEmpty(this._addSolicitante.SolicitanteName))
            {
                if (!this._addSolicitante.ValidateBusiness())
                {
                    this._addSolicitante.Error = "El solicitante que ingreso ya existe en esta empesa ¡cambie la empresa!";
                    _canAddSolicitante = false;
                }
                else
                {
                    this._addSolicitante.Error = "";
                }
            }
            

            return _canAddSolicitante;
        }

        public void AttempAddSolicitante()
        {
            this._addSolicitante.saveSolicitante();

            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogSolicitanteViewModel != null)
            {
                this._catalogSolicitanteViewModel.loadSolicitante();
            }
        }
        #endregion
    }
}
