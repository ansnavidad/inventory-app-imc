using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogCliente
{
    public class ModifyClienteViewModel
    {
        #region Fields
        private ClienteModel _modiClienteModel;
        private RelayCommand _modifyClienteCommand;
        private CatalogClienteViewModel _catalogClienteViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public ClienteModel ModiClienteModel 
        {
            get
            {
                return _modiClienteModel;
            }
            set
            {
                _modiClienteModel = value;
            }
        }

        public ICommand ModifyClienteCommand
        {
            get 
            {
                if (_modifyClienteCommand == null)
                {
                    _modifyClienteCommand = new RelayCommand(p => this.AttempModifyCliente(), p => this.CanAttempModifyCliente());
                }
                return _modifyClienteCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyClienteViewModel(CatalogClienteViewModel catalogClienteViewModel, ClienteModel selectedClienteModel)
        {
            this._modiClienteModel = new ClienteModel(new ClienteDataMapper(), catalogClienteViewModel.ActualUser);
            this._catalogClienteViewModel = catalogClienteViewModel;
            this._modiClienteModel.ClienteName = selectedClienteModel.ClienteName;
            this._modiClienteModel.UnidCliente = selectedClienteModel.UnidCliente;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyCliente()
        {
            bool _canAddCliente = true;
            if (String.IsNullOrEmpty(this._modiClienteModel.ClienteName))
                _canAddCliente = false;

            return _canAddCliente;
        }

        public void AttempModifyCliente()
        {
            this._modiClienteModel.updateCliente();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogClienteViewModel != null)
            {
                this._catalogClienteViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.ModiClienteModel);
            return historialViewModel;
        }

        #endregion
    }
}
