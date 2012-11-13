using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogCliente
{
    public class CatalogClienteViewModel
    {
        private RelayCommand _deleteClienteCommand;

        private CatalogClienteModel _catalogClienteModel;

        public ICommand DeleteClienteCommand
        {
            get
            {
                if (_deleteClienteCommand == null)
                {
                    _deleteClienteCommand = new RelayCommand(p => this.AttempDeleteCliente(), p => this.CanAttempDeleteCliente());
                }
                return _deleteClienteCommand;
            }
        }

        public CatalogClienteViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new ClienteDataMapper();
                this._catalogClienteModel = new CatalogClienteModel(dataMapper);   
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

        public CatalogClienteModel CatalogClienteModel
        {
            get
            {
                return _catalogClienteModel;
            }
            set
            {
                _catalogClienteModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogClienteModel.loadCliente();
        }

        /// <summary>
        /// Crea una nueva instancia de addcliente y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddClienteViewModel CreateAddClienteViewModel()
        {
            return new AddClienteViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de Modifycliente y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyClienteViewModel CreateModifyClienteViewModel()
        {
            ClienteModel clienteModel = new ClienteModel(new ClienteDataMapper());
            if (this._catalogClienteModel != null && this._catalogClienteModel.SelectedCliente != null)
            {
                clienteModel.ClienteName = this._catalogClienteModel.SelectedCliente.CLIENTE1;
                clienteModel.UnidCliente = this._catalogClienteModel.SelectedCliente.UNID_CLIENTE;
            }
            return new ModifyClienteViewModel(this, clienteModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteCliente()
        {
            bool _canDeleteCliente = false;
            foreach (DeleteCliente d in this._catalogClienteModel.Cliente)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteCliente = true;
                }
            }

            return _canDeleteCliente;
        }

        public void AttempDeleteCliente()
        {
            this._catalogClienteModel.deleteCliente();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogClienteModel != null)
            {
                this._catalogClienteModel.loadCliente();
            }
        }
        #endregion
    }
}
