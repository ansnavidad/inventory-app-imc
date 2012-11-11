using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogTipoEmpresa
{
    public class CatalogTipoEmpresaViewModel
    {
        private RelayCommand _deleteTipoEmpresaCommand;
        private CatalogTipoEmpresaModel _catalogTipoEmpresaModel;

        public ICommand DeleteTipoEmpresaCommand
        {
            get
            {
                if (_deleteTipoEmpresaCommand == null)
                {
                    _deleteTipoEmpresaCommand = new RelayCommand(p => this.AttempDeleteTipoEmpresa(), p => this.CanAttempDeleteTipoEmpresa());
                }
                return _deleteTipoEmpresaCommand;
            }
        }

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

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteTipoEmpresa()
        {
            bool _canDeleteTipoEmpresa = false;
            foreach (DeleteTipoEmpresa d in this._catalogTipoEmpresaModel.TipoEmpresas)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteTipoEmpresa = true;
                }
            }

            return _canDeleteTipoEmpresa;
        }

        public void AttempDeleteTipoEmpresa()
        {
            this._catalogTipoEmpresaModel.deleteTipoEmpresa();

            if (this._catalogTipoEmpresaModel != null)
            {
                this._catalogTipoEmpresaModel.loadItems();
            }
        }
        #endregion
    }
}
