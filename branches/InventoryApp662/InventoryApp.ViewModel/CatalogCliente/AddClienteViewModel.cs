using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogCliente
{
    public class AddClienteViewModel
    {
        #region Fields
        private ClienteModel _addclienteModel;
        private RelayCommand _addClienteCommand;
        private CatalogClienteViewModel _catalogClienteViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public ClienteModel AddClienteModel 
        {
            get
            {
                return _addclienteModel;
            }
            set
            {
                _addclienteModel = value;
            }
        }

        public ICommand AddClienteCommand
        {
            get 
            {
                if (_addClienteCommand == null)
                {
                    _addClienteCommand = new RelayCommand(p => this.AttempAddCliente(), p => this.CanAttempAddCliente());
                }
                return _addClienteCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddClienteViewModel(CatalogClienteViewModel catalogClienteViewModel)
        {
            this._addclienteModel = new ClienteModel(new ClienteDataMapper(), catalogClienteViewModel.ActualUser);
            this._catalogClienteViewModel = catalogClienteViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddCliente()
        {
            bool _canAddCliente = true;
            if (String.IsNullOrEmpty(this._addclienteModel.ClienteName))
                _canAddCliente = false;

            return _canAddCliente;
        }

        public void AttempAddCliente()
        {
            this._addclienteModel.saveCliente();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogClienteViewModel != null)
            {
                this._catalogClienteViewModel.loadItems();
            }
        }
        #endregion
    }
}
